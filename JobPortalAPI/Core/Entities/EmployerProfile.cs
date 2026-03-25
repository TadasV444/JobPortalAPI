using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobPortalAPI.Core.Entities;

[Table("EmployerProfiles")]
public class EmployerProfile
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Required]
    [Column("user_id")]
    public int UserId { get; set; }
    [Required]
    [Column("company_name")]
    public string CompanyName { get; set; } = string.Empty;
    [Required]
    [Column("contact_email")]
    public string ContactEmail { get; set; } = string.Empty;
    [Column("location")]
    public string Location { get; set; } = string.Empty;
    [Required]
    [Column("user")]
    // Navigation Property
    public User User { get; set; } = null!;
    
    public virtual ICollection<JobPosting> JobPostings { get; set; } = new List<JobPosting>();
}