namespace JobPortalAPI.Api.Models.Dtos;

public class CandidateDetailsDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Email { get; set; }
    public string Summary { get ; set; }
    public string Location { get; set; }
    public int YearsOfExperience { get; set; }
    // Will add skills laters
}