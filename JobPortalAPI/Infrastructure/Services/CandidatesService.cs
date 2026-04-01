using JobPortalAPI.Api.Models.Dtos;
using JobPortalAPI.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobPortalAPI.Infractructure.Services;

public class CandidatesService(JobPortalContext context) : ICandidatesService
{
    public async Task<List<CandidateListDto>> GetCandidatesAsync()
    {
        return await context.CandidateProfiles
            .Select(c => new CandidateListDto
            {
                Id = c.Id,
                Email = c.User.Email,
                Location = c.Location,
                YearsOfExperience = c.YearsOfExperience
            })
            .ToListAsync();
    }

    public async Task<CandidateDetailsDto?> GetCandidateDetailsAsync(int id)
    {
        return await context.CandidateProfiles
            .Where(c => c.Id == id)
            .Select(c => new CandidateDetailsDto()
            {
                Id = c.Id,
                Email = c.User.Email,
                UserId = c.User.Id,
                Summary = c.Summary,
                Location = c.Location,
                YearsOfExperience = c.YearsOfExperience
            })
            .FirstOrDefaultAsync();
    }
}