using System.Runtime.InteropServices;
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
public class SkillsController(ISkillService skillService) : ControllerBase
{
    [HttpPost("create-skill")]
    public async Task<ActionResult<ApiResponse<SkillResponse>>> CreateSkill(CreateSkillsRequest request)
    {
        var skillResponse = await skillService.CreateSkillAsync(request);

        if (skillResponse is null)
        {
            return this.BadRequestResponse<SkillResponse>(
                message: "Skill creation failed",
                errors: ["Skill already exists or input is invalid "]
                );
        }

        return Ok(ApiResponse<SkillResponse>.CreateSuccess(skillResponse, "Skill created successfully"));
    }

    [HttpGet("get-all-skills")]
    public async Task<ActionResult<ApiResponse<List<SkillResponse>>>> GetAllSkills()
    {
        var skillResponse = await skillService.GetSkillsAsync();
        
        return Ok(ApiResponse<List<SkillResponse>>.CreateSuccess(skillResponse,  "All skills retrieved successfully"));
    }

    [HttpDelete("delete-skill")]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteSkill(int id)
    {
        var skillDeleted = await skillService.DeleteSkillByIdAsync(id);

        if (!skillDeleted)
        {
            return this.NotFoundResponse<bool>(
                message: "Skill not found",
                errors: new List<string> { $"Skill with id {id} was not found." }
            );
        }
        
        return Ok(ApiResponse<bool>.CreateSuccess(skillDeleted, "Skill deleted successfully"));
    }
    
}