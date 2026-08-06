using JobPortalAPI.Api.Models.Requests;
using JobPortalAPI.Api.Models.Responses;
using JobPortalAPI.Core.Helpers;

namespace JobPortalAPI.Core.Interfaces;

public interface IApplicationService
{
    Task<ServiceResult<ApplicationResponse>> CreateApplicationAsync(int userId, ApplicationRequest request);
    Task<List<ApplicationResponse>> GetApplicationsForCandidateAsync(int userId);
    Task<List<ApplicationResponse>> GetApplicationsForEmployerAsync(int userId);
    Task<ApplicationResponse?> GetApplicationByIdAsync(int applicationId, int userId);
    Task<ServiceResult<bool>> WithdrawApplicationAsync(int applicationId, int  userId);
    Task<bool> DeleteApplicationAsync(int applicationId);
}