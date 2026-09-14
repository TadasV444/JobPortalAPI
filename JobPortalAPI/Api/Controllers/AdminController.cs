using JobPortalAPI.Api.Extentions;
using JobPortalAPI.Api.Models.Requests;
using JobPortalAPI.Api.Models.Responses;
using JobPortalAPI.Core.Enums;
using JobPortalAPI.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPortalAPI.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class AdminController(IAdminService adminService) : ControllerBase
{
    [HttpGet("users")]
    public async Task<ActionResult<ApiResponse<AdminUserResponse>>> GetAllUsers()
    {
        var users = await adminService.GetAllUsersAsync();
        return Ok(ApiResponse<List<AdminUserResponse>>.CreateSuccess(users, "Users retrieved successfully"));

    }

    [HttpDelete("users/{id:int}")]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteUser(int id)
    {
        var result = await adminService.DeleteUserAsync(id, this.GetUserId());

        if (!result.IsSuccess)
        {
            return result.ErrorType switch
            {
                ServiceErrorType.NotFound => this.NotFoundResponse<bool>(result.ErrorMessage!),
                ServiceErrorType.Conflict => this.ConflictResponse<bool>(result.ErrorMessage!),
                _ => this.BadRequestResponse<bool>(result.ErrorMessage!)
            };
        }
        
        return Ok(ApiResponse<bool>.CreateSuccess(true, "User deleted successfully"));
    }
}