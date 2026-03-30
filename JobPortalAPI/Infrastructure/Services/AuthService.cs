using JobPortalAPI.Api.Models.Requests;
using JobPortalAPI.Api.Models.Responses;
using JobPortalAPI.Core.Entities;
using JobPortalAPI.Core.Enums;
using JobPortalAPI.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JobPortalAPI.Infractructure.Services;

public class AuthService(JobPortalContext context, IConfiguration configuration) : IAuthService 
{
    public async Task<AuthResponse?> RegisterCandidateAsync(RegisterCandidateRequest request)
    {
        var email =  request.Email?.Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
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
            Token = string.Empty
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
            Token = string.Empty
        };
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest request)
    {
        var email = request.Email?.Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
        {
            return null;
        }
        
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

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
        
        return new AuthResponse
        {
            Email = email!,
            Role = user.Role.ToString(),
            Token = string.Empty
        };
        
    }
}