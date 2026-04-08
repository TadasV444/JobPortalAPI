using JobPortalAPI.Api.Models.Requests;
using JobPortalAPI.Api.Models.Responses;

namespace JobPortalAPI.Core.Interfaces;

public interface IJobService
{
    Task<CreateJobRequest> CreateJobAsync(CreateJobRequest request);
    Task<UpdateJobRequest> UpdateJobAsync(int id, UpdateJobRequest request);
    Task<List<JobResponse>> GetJobsAsync();
    Task<JobResponse> GetJobByIdAsync(int id);
}