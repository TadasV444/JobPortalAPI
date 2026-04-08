namespace JobPortalAPI.Api.Models.Requests;

public class CreateJobRequest
{
    public int EmployerProfileId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public decimal SalaryFrom { get; set; }
    public decimal SalaryTo { get; set; }
    
}