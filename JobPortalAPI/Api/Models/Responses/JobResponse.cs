namespace JobPortalAPI.Api.Models.Responses;

public class JobResponse
{
    public int Id { get; set; }
    public int EmployerProfileId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public decimal SalaryFrom { get; set; } 
    public decimal SalaryTo { get; set; }
    public DateTime CreatedAt { get; set; }

}