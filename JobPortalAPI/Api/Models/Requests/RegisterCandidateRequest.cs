namespace JobPortalAPI.Api.Models.Requests;

public class RegisterCandidateRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int YearsOfExperience { get; set; }
}