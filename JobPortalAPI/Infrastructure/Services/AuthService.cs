using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using JobPortalAPI.Api.Models.Requests;
using JobPortalAPI.Api.Models.Responses;
using JobPortalAPI.Core.Entities;
using JobPortalAPI.Core.Enums;
using JobPortalAPI.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace JobPortalAPI.Infractructure.Services;

public class AuthService(JobPortalContext context, IConfiguration configuration) : IAuthService
{
    public async Task<AuthResponse?> RegisterCandidateAsync(RegisterCandidateRequest request)
    {
        var email = request.Email?.Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
        {
            return null;
        }

        var existingUser = await context.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (existingUser != null)
        {
            return null;
        }

        var user = new User()
        {
            Email = email!,
            Role = Role.Candidate,
            CreatedAt = DateTime.UtcNow
        };

        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, request.Password);

        var candidateProfile = new CandidateProfile
        {
            User = user,
            Summary = request.Summary,
            Location = request.Location,
            YearsOfExperience = request.YearsOfExperience
        };

        context.CandidateProfiles.Add(candidateProfile);
        await context.SaveChangesAsync();

        return new AuthResponse
        {
            Email = email!,
            Role = user.Role.ToString(),
            Token = CreateToken(user)
        };
    }

    public async Task<AuthResponse?> RegisterEmployerAsync(RegisterEmployerRequest request)
    {
        var email = request.Email?.Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
        {
            return null;
        }

        var existingUser = await context.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (existingUser != null)
        {
            return null;
        }

        var user = new User
        {
            Email = email!,
            Role = Role.Employer,
            CreatedAt = DateTime.UtcNow
        };

        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, request.Password);

        var employerProfile = new EmployerProfile
        {
            User = user,
            CompanyName = request.CompanyName,
            ContactEmail = email!,
            Location = request.Location
        };

        context.EmployerProfiles.Add(employerProfile);
        await context.SaveChangesAsync();

        return new AuthResponse
        {
            Email = email!,
            Role = user.Role.ToString(),
            Token = CreateToken(user)
        };
    }

    public async Task<TokenResponse?> LoginAsync(LoginRequest request)
    {
        var email = request.Email?.Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
        {
            return null;
        }

        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
        {
            return null;
        }

        var hasher = new PasswordHasher<User>();
        var verificationResult = hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

        if (verificationResult == PasswordVerificationResult.Failed)
        {
            return null;
        }


        user.LastLogin = DateTime.UtcNow;
        await context.SaveChangesAsync();

        return await CreateTokenResponse(user);
    }

    public async Task<TokenResponse?> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var user = await ValidateRefreshToken(request.UserId, request.RefreshToken);
        if (user is null)
        {
            return null;
        }

        return await CreateTokenResponse(user);
    }

    private async Task<TokenResponse> CreateTokenResponse(User user)
    {
        return new TokenResponse
        {
            AccessToken = CreateToken(user),
            RefreshToken = await GenerateAndSaveRefreshTokenAsync(user),
        };
    }

    private async Task<User?> ValidateRefreshToken(int userId, string refreshToken)
    {
        var user = await context.Users.FindAsync(userId);
        if (user == null ||
            user.RefreshToken != refreshToken ||
            user.RefreshTokenExpirationDate <= DateTime.UtcNow)
        {
            return null;
        }

        return user;
    }

    private async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
    {
        var refreshToken = GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpirationDate = DateTime.UtcNow.AddDays(1);
        await context.SaveChangesAsync();
        return refreshToken;
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Role, user.Role.ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
        var tokenDescriptor = new JwtSecurityToken(
            issuer: configuration.GetValue<string>("AppSettings:Issuer"),
            audience: configuration.GetValue<string>("AppSettings:Audience"),
            claims: claims,
            expires: DateTime.Now.AddHours(4),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}