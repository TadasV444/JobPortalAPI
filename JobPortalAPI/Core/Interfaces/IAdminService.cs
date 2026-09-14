using JobPortalAPI.Api.Models.Responses;
using JobPortalAPI.Core.Helpers;

namespace JobPortalAPI.Core.Interfaces;

public interface IAdminService
{
    Task<List<AdminUserResponse>> GetAllUsersAsync();
    Task<ServiceResult<bool>> DeleteUserAsync(int userId, int currentAdminId);
}