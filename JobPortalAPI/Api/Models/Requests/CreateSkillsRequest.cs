using System.ComponentModel.DataAnnotations;

namespace JobPortalAPI.Api.Models.Requests;

public class CreateSkillsRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
}