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
public class ApplicationsController(IApplicationService applicationService) : ControllerBase
{   
    [Authorize(Roles = "Employer")]
    [HttpPost("create-job-application")]
    public async Task<ActionResult<ApiResponse<ApplicationResponse>>> CreateApplication(ApplicationRequest request)
    {
        var userId = this.GetUserId();
        var applicationResponse = await applicationService.CreateApplicationAsync(userId, request);

        if (applicationResponse is null)
        {
            return this.BadRequestResponse<ApplicationResponse>(
                message: "Application not found",
                errors: ["Application is not created"]
            );
        }

        return Ok(ApiResponse<ApplicationResponse>.CreateSuccess(applicationResponse, "Application created"));
    }

    [Authorize(Roles = "Candidate")]
    [HttpGet("get-candidate-applications")]
    public async Task<ActionResult<ApiResponse<List<ApplicationResponse>>>> GetCandidateApplications()
    {
        var userId = this.GetUserId();
        var candidateApplications = await applicationService.GetApplicationsForCandidateAsync(userId);

        return Ok(ApiResponse<List<ApplicationResponse>>.CreateSuccess(candidateApplications,"Candidate application retrieved"));
    }

    [Authorize(Roles = "Employer")]
    [HttpGet("get-employer-applications")]
    public async Task<ActionResult<ApiResponse<List<ApplicationResponse>>>> GetEmployerApplications()
    {
        var userId = this.GetUserId();
        var employerApplications = await applicationService.GetApplicationsForEmployerAsync(userId);

        return Ok(ApiResponse<List<ApplicationResponse>>.CreateSuccess(employerApplications, "Employer application retrieved"));
    }


    [Authorize(Roles = "Candidate,Employer")]
    [HttpGet("get-applications-by-id")]
    public async Task<ActionResult<ApiResponse<ApplicationResponse>>> GetApplicationsById(int applicationId)
    {
        var userId = this.GetUserId();
        var getApplications = await applicationService.GetApplicationByIdAsync(applicationId, userId);
        
        if (getApplications is null)
        {
            return this.BadRequestResponse<ApplicationResponse>(
                message: "Application by this ID not found",
                errors: ["Unable to find application"]
            );
        }
        
        return Ok(ApiResponse<ApplicationResponse>.CreateSuccess(getApplications, "Application by Id is retrieved"));
        
    }
}