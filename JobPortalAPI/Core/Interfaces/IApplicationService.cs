using JobPortalAPI.Api.Models.Requests;
using JobPortalAPI.Api.Models.Responses;

namespace JobPortalAPI.Core.Interfaces;

public interface IApplicationService
{
    Task<ApplicationResponse?> CreateApplicationAsync(int userId, ApplicationRequest request);
    Task<List<ApplicationResponse>> GetApplicationsForCandidateAsync(int userId);
    Task<List<ApplicationResponse>> GetApplicationsForEmployerAsync(int userId);
    Task<ApplicationResponse?> GetApplicationByIdAsync(int applicationId, int userId);
    Task<bool> WithdrawApplicationAsync(int applicationId, int  userId);
}