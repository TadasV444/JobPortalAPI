using JobPortalAPI.Api.Extentions;
using JobPortalAPI.Api.Models.Requests;
using JobPortalAPI.Api.Models.Responses;
using JobPortalAPI.Core.Entities;
using JobPortalAPI.Core.Interfaces;
using JobPortalAPI.Infractructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobPortalAPI.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register-candidate")]
    public async Task<ActionResult<ApiResponse<AuthResponse>>> RegisterCandidate(RegisterCandidateRequest request)
    {
        var authResponse = await authService.RegisterCandidateAsync(request);

        if (authResponse is null)
        {
            return this.BadRequestResponse<AuthResponse>(
                message: "Candidate already taken",
                errors: ["Candidate already exists"]
            );
        }
        
        return Ok(ApiResponse<AuthResponse>.CreateSuccess(authResponse,"Candidate registration successful"));
    }

    [HttpPost("register-employer")]
    public async Task<ActionResult<ApiResponse<AuthResponse>>> RegisterEmployer(RegisterEmployerRequest request)
    {
        var authResponse = await authService.RegisterEmployerAsync(request);

        if (authResponse is null)
        {
            return this.BadRequestResponse<AuthResponse>(
                message: "Employer already taken",
                errors: ["Employer already exists"]
            );
        }

        return Ok(ApiResponse<AuthResponse>.CreateSuccess(authResponse, "Employer registration successful"));

    }

    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<TokenResponse>>> Login(LoginRequest request)
    {
        var tokenResponse = await authService.LoginAsync(request);

        if (tokenResponse is null)
        {
            return this.UnauthorizedResponse<TokenResponse>(
                message: " User already logged in ",
                errors: ["User already exists"]
            );
        }
        return Ok(ApiResponse<TokenResponse>.CreateSuccess(tokenResponse, "User logged in successfully"));
    }
    [HttpPost("refresh-token")]
    public async Task<ActionResult<ApiResponse<TokenResponse>>> RefreshToken(RefreshTokenRequest request)
    {
        var result = await authService.RefreshTokenAsync(request);
        if (result is null)
        {
            return this.UnauthorizedResponse<TokenResponse>(
                message: "Invalid refresh token",
                errors: ["The provided refresh token is invalid or expired"]
            );
        }

        return Ok(ApiResponse<TokenResponse>.CreateSuccess(
            data: result,
            message: "Token refreshed successfully"
        ));
    }

    [Authorize]
    [HttpGet("authenticated")]
    public ActionResult<ApiResponse<string>> AuthenticatedOnlyEndpoint()
    {
        return Ok(ApiResponse<string>.CreateSuccess(
            data: "authenticated",
            message: "Successfully accessed authenticated endpoint"
        ));
    }
    
    
}