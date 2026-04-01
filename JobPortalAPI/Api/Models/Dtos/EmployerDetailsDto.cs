namespace JobPortalAPI.Api.Models.Dtos;

public class EmployerDetailsDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Email { get; set; }
    public string CompanyName { get; set; }
    public string Location { get; set; }
    // will add jobpostings later
}