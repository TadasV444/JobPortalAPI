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
    [Column("location")]
    public string Location { get; set; } = string.Empty;
}