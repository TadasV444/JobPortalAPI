using JobPortalAPI.Api.Models.Requests;
using JobPortalAPI.Api.Models.Responses;
using JobPortalAPI.Core.Entities;
using JobPortalAPI.Core.Enums;
using JobPortalAPI.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobPortalAPI.Infractructure.Services;

public class ApplicationService(JobPortalContext context) : IApplicationService
{
    public async Task<ApplicationResponse?> CreateApplicationAsync(int userId, ApplicationRequest request)
    {
        var candidateProfile = await context.CandidateProfiles
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (candidateProfile == null)
        {
            return null;
        }

        var jobPosting = await context.JobPostings
            .FirstOrDefaultAsync(j => j.Id == request.JobPostingId);

        if (jobPosting == null)
        {
            return null;
        }

        var duplicatesApplication = await context.Applications
            .AnyAsync(a => a.CandidateProfileId == candidateProfile.Id && a.JobPostingId == jobPosting.Id);

        if (duplicatesApplication)
        {
            return null;
        }

        var application = new Application()
        {
            CandidateProfileId = candidateProfile.Id,
            JobPostingId = jobPosting.Id,
            AppliedDate = DateTime.UtcNow,
            Status = nameof(ApplicationStatus.Pending)
        };

        context.Applications.Add(application);
        await context.SaveChangesAsync();

        return await context.Applications
            .Where(a => a.Id == application.Id)
            .Select(c => new ApplicationResponse
            {
                CandidateProfileId = c.CandidateProfileId,
                CandidateEmail = c.CandidateProfile.User.Email,
                CandidateLocation = c.CandidateProfile.Location,
                CandidateSummary = c.CandidateProfile.Summary,
                YearsOfExperience = c.CandidateProfile.YearsOfExperience,

                JobPostingId = c.JobPostingId,
                EmployerProfileId = c.JobPosting.EmployerProfileId,
                JobPostingDescription = c.JobPosting.JobDescription,
                JobPostingTitle = c.JobPosting.Title,
                JobPostingLocation = c.JobPosting.Location,
                SalaryFrom = c.JobPosting.SalaryFrom,
                SalaryTo = c.JobPosting.SalaryTo,
                EmployerCompanyName = c.JobPosting.EmployerProfile.CompanyName,
                Status = c.Status,
                AppliedDate = c.AppliedDate,
                CoverLetter = c.CoverLetter,
                Skills = c.JobPosting.JobSkills.Select(js => js.Skill.Name).ToList()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<List<ApplicationResponse>> GetApplicationsForCandidateAsync(int userId)
    {
        return await context.Applications
            .Where(a => a.CandidateProfile.UserId == userId)
            .Select(c => new ApplicationResponse
            {
                CandidateProfileId = c.CandidateProfileId,
                CandidateEmail = c.CandidateProfile.User.Email,
                CandidateLocation = c.CandidateProfile.Location,
                CandidateSummary = c.CandidateProfile.Summary,
                YearsOfExperience = c.CandidateProfile.YearsOfExperience,

                JobPostingId = c.JobPostingId,
                EmployerProfileId = c.JobPosting.EmployerProfileId,
                JobPostingDescription = c.JobPosting.JobDescription,
                JobPostingTitle = c.JobPosting.Title,
                JobPostingLocation = c.JobPosting.Location,
                SalaryFrom = c.JobPosting.SalaryFrom,
                SalaryTo = c.JobPosting.SalaryTo,
                EmployerCompanyName = c.JobPosting.EmployerProfile.CompanyName,
                Status = c.Status,
                AppliedDate = c.AppliedDate,
                CoverLetter = c.CoverLetter,
                Skills = c.JobPosting.JobSkills.Select(js => js.Skill.Name).ToList()
            })
            .ToListAsync();
    }

    public async Task<List<ApplicationResponse>> GetApplicationsForEmployerAsync(int userId)
    {
        return await context.Applications
            .Where(a => a.JobPosting.EmployerProfile.UserId == userId)
            .Select(c => new ApplicationResponse
            {
                CandidateProfileId = c.CandidateProfileId,
                CandidateEmail = c.CandidateProfile.User.Email,
                CandidateLocation = c.CandidateProfile.Location,
                CandidateSummary = c.CandidateProfile.Summary,
                YearsOfExperience = c.CandidateProfile.YearsOfExperience,

                JobPostingId = c.JobPostingId,
                EmployerProfileId = c.JobPosting.EmployerProfileId,
                JobPostingDescription = c.JobPosting.JobDescription,
                JobPostingTitle = c.JobPosting.Title,
                JobPostingLocation = c.JobPosting.Location,
                SalaryFrom = c.JobPosting.SalaryFrom,
                SalaryTo = c.JobPosting.SalaryTo,
                EmployerCompanyName = c.JobPosting.EmployerProfile.CompanyName,
                Status = c.Status,
                AppliedDate = c.AppliedDate,
                CoverLetter = c.CoverLetter,
                Skills = c.JobPosting.JobSkills.Select(js => js.Skill.Name).ToList()
            })
            .ToListAsync();
    }

    public async Task<ApplicationResponse?> GetApplicationByIdAsync(int applicationId, int userId)
    {
        return await context.Applications
            .Where(a => a.Id == applicationId &&
                        (a.CandidateProfile.UserId == userId || a.JobPosting.EmployerProfile.UserId == userId))
            .Select(a => new ApplicationResponse
            {
                CandidateProfileId = a.CandidateProfileId,
                CandidateEmail = a.CandidateProfile.User.Email,
                CandidateLocation = a.CandidateProfile.Location,
                CandidateSummary = a.CandidateProfile.Summary,
                YearsOfExperience = a.CandidateProfile.YearsOfExperience,

                JobPostingId = a.JobPostingId,
                EmployerProfileId = a.JobPosting.EmployerProfileId,
                JobPostingDescription = a.JobPosting.JobDescription,
                JobPostingTitle = a.JobPosting.Title,
                JobPostingLocation = a.JobPosting.Location,
                SalaryFrom = a.JobPosting.SalaryFrom,
                SalaryTo = a.JobPosting.SalaryTo,
                EmployerCompanyName = a.JobPosting.EmployerProfile.CompanyName,
                Status = a.Status,
                AppliedDate = a.AppliedDate,
                CoverLetter = a.CoverLetter,
                Skills = a.JobPosting.JobSkills.Select(js => js.Skill.Name).ToList()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Boolean> WithdrawApplicationAsync(int applicationId, int userId)
    {
        var application = await context.Applications
            .FirstOrDefaultAsync(a => a.Id == applicationId && a.CandidateProfile.UserId == userId);

        if (application == null)
        {
            return false;
        }

        if (application.Status == nameof(ApplicationStatus.Approved)
            || application.Status == nameof(ApplicationStatus.Rejected))
        {
            return false;
        }

        application.Status = nameof(ApplicationStatus.Withdrawn);
        await context.SaveChangesAsync();

        return true;
    }
}