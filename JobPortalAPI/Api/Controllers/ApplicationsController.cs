using JobPortalAPI.Api.Extentions;
using JobPortalAPI.Api.Models.Requests;
using JobPortalAPI.Api.Models.Responses;
using JobPortalAPI.Core.Entities;
using JobPortalAPI.Core.Enums;
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
    [Authorize(Roles = "Candidate")]
    [HttpPost("create-job-application")]
    public async Task<ActionResult<ApiResponse<ApplicationResponse>>> CreateApplication(ApplicationRequest request)
    {
        var userId = this.GetUserId();
        var result = await applicationService.CreateApplicationAsync(userId, request);

        if (!result.IsSuccess)
        {
            return result.ErrorType switch
            {
                ServiceErrorType.NotFound => this.NotFoundResponse<ApplicationResponse>(result.ErrorMessage!),
                ServiceErrorType.Conflict => this.ConflictResponse<ApplicationResponse>(result.ErrorMessage!),
                _ => this.BadRequestResponse<ApplicationResponse>(result.ErrorMessage!)
            };
        }

        return Ok(ApiResponse<ApplicationResponse>.CreateSuccess(result.Data!, "Application created"));
    }

    [Authorize(Roles = "Candidate")]
    [HttpGet("get-candidate-applications")]
    public async Task<ActionResult<ApiResponse<List<ApplicationResponse>>>> GetCandidateApplications()
    {
        var userId = this.GetUserId();
        var candidateApplications = await applicationService.GetApplicationsForCandidateAsync(userId);

        return Ok(ApiResponse<List<ApplicationResponse>>.CreateSuccess(candidateApplications,
            "Candidate application retrieved"));
    }

    [Authorize(Roles = "Employer")]
    [HttpGet("get-employer-applications")]
    public async Task<ActionResult<ApiResponse<List<ApplicationResponse>>>> GetEmployerApplications()
    {
        var userId = this.GetUserId();
        var employerApplications = await applicationService.GetApplicationsForEmployerAsync(userId);

        return Ok(ApiResponse<List<ApplicationResponse>>.CreateSuccess(employerApplications,
            "Employer application retrieved"));
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

    [Authorize(Roles = "Candidate")]
    [HttpPut("withdraw-from-application")]
    public async Task<ActionResult<ApiResponse<bool>>> WithdrawFromApplicationByUser(int applicationId)
    {
        var userId = this.GetUserId();

        var result = await applicationService.WithdrawApplicationAsync(applicationId, userId);

        if (!result.IsSuccess)
        {
            return result.ErrorType switch
            {
                ServiceErrorType.NotFound => this.NotFoundResponse<bool>(result.ErrorMessage!),
                ServiceErrorType.Conflict => this.ConflictResponse<bool>(result.ErrorMessage!),
                _ => this.BadRequestResponse<bool>(result.ErrorMessage!)
            };
        }

        return Ok(ApiResponse<bool>.CreateSuccess(true, "Successfully withdrawn from application"));
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("delete-application")]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteApplication(int applicationId)
    {
        var applicationDeleted = await applicationService.DeleteApplicationAsync(applicationId);

        if (!applicationDeleted)
        {
            return this.NotFoundResponse<bool>(
                message: "Application not found",
                errors: new List<string> { $"Application with application id {applicationId} was not found." }
            );
        }

        return Ok(ApiResponse<bool>.CreateSuccess(applicationDeleted, "Application deleted successfully"));
    }
}