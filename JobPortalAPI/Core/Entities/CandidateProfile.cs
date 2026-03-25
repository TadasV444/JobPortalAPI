using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobPortalAPI.Core.Entities;

[Table("CandidateProfiles")]
public class CandidateProfile
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Required]
    [Column("user_id")]
    public int UserId { get; set; }
    [Required]
    [Column("summary")]
    public string Summary { get; set; } = string.Empty;
    [Required]
    [Column("location")]
    public string Location { get; set; } = string.Empty;
    [Required]
    [Column("years_of_experience")]
    public int YearsOfExperience { get; set; }
    [Required]
    [Column("user")]
    // Navigation Property
    public User User { get; set; } = null!;
    
    public virtual ICollection<CandidateSkill> CandidateSkills { get; set; } = new List<CandidateSkill>();
    
    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
}