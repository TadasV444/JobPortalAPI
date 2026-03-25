using JobPortalAPI.Api.Models.Requests;
using JobPortalAPI.Api.Models.Responses;
using JobPortalAPI.Core.Entities;
using JobPortalAPI.Core.Enums;
using JobPortalAPI.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace JobPortalAPI.Infractructure.Services;

/*public class AuthService(JobPortalContext context, IConfiguration configuration) : IAuthService 
{
    public async Task<AuthResponse?> RegisterCandidateAsync(RegisterCandidateRequest request)
    {
        var email =  request.Email?.Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
        {
            return null;
        }

        var user = new User()
        {
            Email = request.Email,
            PasswordHash = request.Password,
            Role = Role.Candidate,
            CreatedAt = DateTime.UtcNow
        };
        
        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, request.Password);
        context.Add(user);
    }
    
    
}*/