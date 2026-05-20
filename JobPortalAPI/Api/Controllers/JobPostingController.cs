using JobPortalAPI.Api.Extentions;
using JobPortalAPI.Api.Models.Requests;
using JobPortalAPI.Api.Models.Responses;
using JobPortalAPI.Core.Entities;
using JobPortalAPI.Core.Interfaces;
using JobPortalAPI.Infractructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace JobPortalAPI.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobPostingController(IJobService jobService) : ControllerBase
{
    [HttpPost("create-job-posting")]
    public async Task<ActionResult<ApiResponse<JobResponse>>> CreateJob(CreateJobRequest request)
    {
        var jobResponse = await jobService.CreateJobAsync(request);

        if (jobResponse is null)
        {
            return this.BadRequestResponse<JobResponse>(
                message: "This job posting is already created ",
                errors: ["Job posting is taken"]
            );
        }
        
        return Ok(ApiResponse<JobResponse>.CreateSuccess(jobResponse, "Job posting was successfully created"));
    }

    [HttpPut("update-job-posting")]
    public async Task<ActionResult<ApiResponse<JobResponse>>> UpdateJob(int id, UpdateJobRequest request)
    {
        var jobResponse = await jobService.UpdateJobAsync(id,  request);
        
        if (jobResponse is null)
        {
            return this.BadRequestResponse<JobResponse>(
                message: "This job posting is already updated ",
                errors: ["Job posting is already updated"]
            );
        }
        
        return Ok(ApiResponse<JobResponse>.CreateSuccess(jobResponse, "Job posting was successfully updated"));
        
    }

    [HttpGet("get-job-posting-list")]
    public async Task<ActionResult<ApiResponse<JobResponse>>> GetJob(int id)
    {
        var jobResponse = await jobService.GetJobByIdAsync(id);
        
        if (jobResponse is null)
        {
            return this.BadRequestResponse<JobResponse>(
                message: "This job posting list is already retrieved ",
                errors: ["Job posting list is already returned"]
            );
        }
        
        return Ok(ApiResponse<JobResponse>.CreateSuccess(jobResponse, "Job posting list successfully retrieved"));
            
    }
    
    [HttpGet("get-job-posting-by-id")]
    public async Task<ActionResult<ApiResponse<List<JobResponse>>>> GetJobs()
    {
        var jobResponse = await jobService.GetJobsAsync();
        
        return Ok(ApiResponse<List<JobResponse>>.CreateSuccess(jobResponse, "Job posting successfully retrieved"));
            
    }
    
}