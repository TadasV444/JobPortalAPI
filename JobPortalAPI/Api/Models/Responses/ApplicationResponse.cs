namespace JobPortalAPI.Api.Models.Responses;

public class ApplicationResponse
{
    public int Id { get; set; }
    
    public int CandidateProfileId { get; set; }
    public string CandidateEmail { get; set; } = string.Empty;
    public string CandidateSummary { get; set; } = string.Empty;
    public string CandidateLocation { get; set; } = string.Empty;
    public int YearsOfExperience { get; set; }
    
    public int JobPostingId { get; set; }
    public int EmployerProfileId { get; set; }
    public string JobPostingDescription { get; set; } = string.Empty;
    public string JobPostingTitle { get; set; } = string.Empty;
    public string JobPostingLocation { get; set; } = string.Empty;
    public decimal? SalaryFrom { get; set; }
    public decimal? SalaryTo { get; set; }
    
    public string EmployerCompanyName  { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime AppliedDate { get; set; }
    public string CoverLetter { get; set; } = string.Empty;

    public List<string> Skills { get; set; } = [];


}