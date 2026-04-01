using JobPortalAPI.Api.Extentions;
using JobPortalAPI.Api.Models.Dtos;
using JobPortalAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace JobPortalAPI.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CandidatesController(ICandidatesService candidatesService) : ControllerBase
{
    [HttpGet("retrieve-candidates-list")]
    public async Task<ActionResult<ApiResponse<List<CandidateListDto>>>> GetCandidatesList()
    {
        var candidates = await candidatesService.GetCandidatesAsync();

        if (candidates.Count == 0)
        {
            return this.BadRequestResponse<List<CandidateListDto>>(
                message: "Candidate list is empty",
                errors: ["Candidate list is empty or does not exist"]
            );
        }

        return Ok(ApiResponse<List<CandidateListDto>>.CreateSuccess(candidates, "Candidate list retrieved"));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<CandidateDetailsDto>>> GetCandidateDetails(int id)
    {
        var candidates = await candidatesService.GetCandidateDetailsAsync(id);

        if (candidates == null)
        {
            return this.NotFoundResponse<CandidateDetailsDto>(
                message: "Candidate does not exist",
                errors: ["Candidate does not exist, details not found"]
            );
        }
        return Ok(ApiResponse<CandidateDetailsDto>.CreateSuccess(candidates, "Candidate details retrieved"));
    }
        
}