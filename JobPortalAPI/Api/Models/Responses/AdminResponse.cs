namespace JobPortalAPI.Api.Models.Responses;

public class AdminResponse
{
    public int Id { get; set; } 
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}