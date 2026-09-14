using JobPortalAPI.Api.Models.Responses;
using JobPortalAPI.Core.Interfaces;
using JobPortalAPI.Core.Entities;
using JobPortalAPI.Core.Enums;
using JobPortalAPI.Core.Helpers;
using Microsoft.EntityFrameworkCore;

namespace JobPortalAPI.Infractructure.Services;

public class AdminService(JobPortalContext context) : IAdminService
{
    public async Task<List<AdminUserResponse>> GetAllUsersAsync()
    {
        return await context.Users
            .Select(u => new AdminUserResponse
            {
                Id = u.Id,
                Email = u.Email,
                Role = u.Role.ToString()
            })
            .ToListAsync();
    }

    public async Task<ServiceResult<bool>> DeleteUserAsync(int userId, int currentAdminId)
    {
        if (userId == currentAdminId)
            return ServiceResult<bool>.Fail(ServiceErrorType.Conflict, "You cannot delete your own admin account");

        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user is null)
            return ServiceResult<bool>.Fail(ServiceErrorType.NotFound, "User not found");

        context.Users.Remove(user);
        await context.SaveChangesAsync();
        return ServiceResult<bool>.Ok(true);
    }
}