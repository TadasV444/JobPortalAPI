using System.ComponentModel.DataAnnotations;

namespace JobPortalAPI.Api.Models.Requests;

public class ApplicationRequest
{
    [Required]
    public int JobPostingId  { get; set; }
    public string CoverLetter { get; set; } = string.Empty;
    
}