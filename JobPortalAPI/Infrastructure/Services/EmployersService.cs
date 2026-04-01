using JobPortalAPI.Api.Models.Dtos;
using JobPortalAPI.Core.Interfaces;
using JobPortalAPI.Api.Models.Dtos;
using JobPortalAPI.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobPortalAPI.Infractructure.Services;

public class EmployersService(JobPortalContext context) : IEmployersService
{
    public async Task<List<EmployerListDto>> GetEmployersAsync()
    {
        return await context.EmployerProfiles
            .Select(e => new EmployerListDto()
            {
                Id = e.Id,
                Email = e.User.Email,
                CompanyName = e.Location,
                Location = e.Location
            })
            .ToListAsync();
    }

    public async Task<EmployerDetailsDto?> GetEmployerDetailsAsync(int id)
    {
        return await context.EmployerProfiles
            .Where(e => e.Id == id)
            .Select(e => new EmployerDetailsDto()
            {
                Id = e.Id,
                Email = e.User.Email,
                UserId = e.User.Id,
                CompanyName = e.CompanyName,
                Location = e.Location
            })
            .FirstOrDefaultAsync();
    }
}