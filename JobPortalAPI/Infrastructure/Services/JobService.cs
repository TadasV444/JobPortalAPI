using JobPortalAPI.Api.Models.Requests;
using JobPortalAPI.Api.Models.Responses;
using JobPortalAPI.Core.Entities;
using JobPortalAPI.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobPortalAPI.Infractructure.Services;

public class JobService(JobPortalContext context) : IJobService
{
    public async Task<JobResponse?> CreateJobAsync(CreateJobRequest request)
    {
        if (request.EmployerProfileId <= 0)
        {
            return null;
        }

        if (string.IsNullOrWhiteSpace(request.Title))
        {
            return null;
        }

        if (string.IsNullOrWhiteSpace(request.Description))
        {
            return null;
        }

        if (request.SalaryFrom < 0 || request.SalaryTo < request.SalaryFrom)
        {
            return null;
        }

        var employer = await context.EmployerProfiles
            .FirstOrDefaultAsync(e => e.Id == request.EmployerProfileId);

        if (employer == null)
        {
            return null;
        }

        var jobPosting = new JobPosting
        {
            EmployerProfileId = employer.Id,
            Title = request.Title,
            JobDescription = request.Description,
            Location = request.Location,
            SalaryFrom = request.SalaryFrom,
            SalaryTo = request.SalaryTo
        };

        context.JobPostings.Add(jobPosting);
        await context.SaveChangesAsync();

        if (request.SkillIds is { Count: > 0 })
        {
            var jobSkills = request.SkillIds
                .Distinct()
                .Select(skillId => new JobSkill
                {
                    JobPostingId = jobPosting.Id,
                    SkillId = skillId
                })
                .ToList();

            context.JobSkills.AddRange(jobSkills);
            await context.SaveChangesAsync();
        }

        return new JobResponse()
        {
            Id = jobPosting.Id,
            EmployerProfileId = jobPosting.EmployerProfileId,
            Title = jobPosting.Title,
            Description = jobPosting.JobDescription,
            Location = jobPosting.Location,
            SalaryFrom = request.SalaryFrom,
            SalaryTo = request.SalaryTo
        };
    }

    public async Task<JobResponse?> UpdateJobAsync(int id, UpdateJobRequest request)
    {
        var jobPosting = await context.JobPostings
            .FirstOrDefaultAsync(j => j.Id == id);

        if (jobPosting is null)
        {
            return null;
        }

        if (request.Title is not null)
        {
            jobPosting.Title = request.Title;
        }

        if (request.Description is not null)
        {
            jobPosting.JobDescription = request.Description;
        }

        if (request.Location is not null)
        {
            jobPosting.Location = request.Location;
        }

        if (request.SalaryFrom.HasValue) jobPosting.SalaryFrom = request.SalaryFrom.Value;
        if (request.SalaryTo.HasValue) jobPosting.SalaryTo = request.SalaryTo.Value;

        if (request.SkillIds is not null)
        {
            var distinctSkillIds = request.SkillIds
                .Distinct()
                .ToList();

            var existingJobSkills = await context.JobSkills
                .Where(js => js.JobPostingId == jobPosting.Id)
                .ToListAsync();

            context.JobSkills.RemoveRange(existingJobSkills);

            if (distinctSkillIds.Count > 0)
            {
                var newJobSkills = distinctSkillIds
                    .Select(skillId => new JobSkill
                    {
                        JobPostingId = jobPosting.Id,
                        SkillId = skillId
                    })
                    .ToList();

                context.JobSkills.AddRange(newJobSkills);
            }
        }

        await context.SaveChangesAsync();

        var skillIds = await context.JobSkills
            .Where(js => js.JobPostingId == jobPosting.Id)
            .Select(js => js.SkillId)
            .ToListAsync();

        return new JobResponse
        {
            Id = jobPosting.Id,
            EmployerProfileId = jobPosting.EmployerProfileId,
            Title = jobPosting.Title,
            Description = jobPosting.JobDescription,
            Location = jobPosting.Location,
            SalaryFrom = jobPosting.SalaryFrom,
            SalaryTo = jobPosting.SalaryTo,
            SkillIds = skillIds
        };
    }

    public async Task<List<JobResponse>> GetJobsAsync()
    {
        return await context.JobPostings
            .Select(j => new JobResponse
            {
                Id = j.Id,
                EmployerProfileId = j.EmployerProfileId,
                Title = j.Title,
                Description = j.JobDescription,
                Location = j.Location,
                SalaryFrom = j.SalaryFrom,
                SalaryTo = j.SalaryTo,
                SkillIds = j.JobSkills.Select(sk => sk.SkillId).ToList()
            })
            .ToListAsync();
    }

    public async Task<JobResponse?> GetJobByIdAsync(int id)
    {
        return await context.JobPostings
            .Where(j => j.Id == id)
            .Select(j => new JobResponse
            {
                Id = j.Id,
                EmployerProfileId = j.EmployerProfileId,
                Title = j.Title,
                Description = j.JobDescription,
                Location = j.Location,
                SalaryFrom = j.SalaryFrom,
                SalaryTo = j.SalaryTo,
                SkillIds = j.JobSkills.Select(sk => sk.SkillId).ToList()
            })
            .FirstOrDefaultAsync();
    }
}