using System.ComponentModel.DataAnnotations;

namespace JobPortalAPI.Api.Models.Requests;

public class CreateJobRequest
{
    public int EmployerProfileId { get; set; }
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public string Description{ get; set; }  = string.Empty;
    [Required]
    public string Location { get; set; }  = string.Empty;
    [Required]
    public decimal SalaryFrom { get; set; }
    [Required]
    public decimal SalaryTo { get; set; }
    
    public List<int> SkillIds { get; set; } = new();
}