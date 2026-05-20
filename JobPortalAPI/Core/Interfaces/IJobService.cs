using JobPortalAPI.Api.Models.Requests;
using JobPortalAPI.Api.Models.Responses;

namespace JobPortalAPI.Core.Interfaces;

public interface IJobService
{
    Task<JobResponse?> CreateJobAsync(CreateJobRequest request);
    Task<JobResponse?> UpdateJobAsync(int id, UpdateJobRequest request);
    Task<List<JobResponse>> GetJobsAsync();
    Task<JobResponse?> GetJobByIdAsync(int id);
}