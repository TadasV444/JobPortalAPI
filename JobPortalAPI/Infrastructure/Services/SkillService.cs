using System.Reflection.Metadata.Ecma335;
using JobPortalAPI.Api.Models.Requests;
using JobPortalAPI.Api.Models.Responses;
using JobPortalAPI.Core.Entities;
using JobPortalAPI.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobPortalAPI.Infractructure.Services;

public class SkillService(JobPortalContext context) : ISkillService
{
    public async Task<SkillResponse?> CreateSkillAsync(CreateSkillsRequest request)
    {
        var name = request.Name?.Trim();

        if (string.IsNullOrWhiteSpace(name))
        {
            return null;
        }

        var exists = await context.Skills
            .AnyAsync(s => s.Name.ToLower() == name.ToLower());

        if (exists)
        {
            return null;
        }

        var skill = new Skill
        {
            Name = name
        };

        context.Skills.Add(skill);
        await context.SaveChangesAsync();

        return new SkillResponse()
        {
            Id = skill.Id,
            Name = skill.Name
        };
    }

    public async Task<List<SkillResponse>> GetSkillsAsync()
    {
        return await context.Skills
            .Select(s => new SkillResponse
            {
                Id = s.Id,
                Name = s.Name
            })
            .ToListAsync();
    }

    public async Task<bool> DeleteSkillByIdAsync(int id)
    {
        var skill = await context.Skills.FirstOrDefaultAsync(s => s.Id == id);

        if (skill is null)
        {
            return false;
        }
        context.Skills.Remove(skill);
        await context.SaveChangesAsync();
        return true;
    }
        
}