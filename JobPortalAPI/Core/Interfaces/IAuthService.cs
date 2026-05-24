using JobPortalAPI.Api.Models.Requests;
using JobPortalAPI.Api.Models.Responses;

namespace JobPortalAPI.Core.Interfaces;

public interface IAuthService
{
    Task<AuthResponse?> RegisterCandidateAsync(RegisterCandidateRequest request);
    Task<AuthResponse?> RegisterEmployerAsync(RegisterEmployerRequest request);
    Task<TokenResponse?> LoginAsync(LoginRequest request);
    Task<TokenResponse?> RefreshTokenAsync(RefreshTokenRequest request);
}