using JobPortalAPI.Api.Extentions;
using JobPortalAPI.Api.Models.Dtos;
using JobPortalAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace JobPortalAPI.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployersController(IEmployersService employersService) : ControllerBase
{
    [HttpGet("retrieve-employers-list")]
    public async Task<ActionResult<ApiResponse<List<EmployerListDto>>>> GetEmployersList()
    {
        var employers = await employersService.GetEmployersAsync();

        if (employers.Count == 0)
        {
            return this.BadRequestResponse<List<EmployerListDto>>(
                message: "Employers list is empty",
                errors: ["Employers list is empty or does not exist"]
            );
        }

        return Ok(ApiResponse<List<EmployerListDto>>.CreateSuccess(employers, "Candidate list retrieved"));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<EmployerDetailsDto>>> GetEmployerDetails(int id)
    {
        var employers = await employersService.GetEmployerDetailsAsync(id);

        if (employers == null)
        {
            return this.NotFoundResponse<EmployerDetailsDto>(
                message: "Employee does not exist",
                errors: ["Employee does not exist, details not found"]
            );
        }
        return Ok(ApiResponse<EmployerDetailsDto>.CreateSuccess(employers, "Employer details retrieved"));
    }
        
}