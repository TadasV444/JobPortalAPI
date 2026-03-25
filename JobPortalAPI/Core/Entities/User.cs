using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JobPortalAPI.Core.Enums;

namespace JobPortalAPI.Core.Entities;
[Table("Users")]
public class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Required]
    [Column("email")]
    public string Email { get; set; } = string.Empty;
    [Required]
    [Column("password_hash")]
    public string PasswordHash { get; set; } = string.Empty;
    [Required]
    [Column("role")]
    public Role Role { get; set; }
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    //Reverse Navigation 
    public CandidateProfile? CandidateProfile { get; set; }
    
    public EmployerProfile? EmployerProfile { get; set; }
    
}