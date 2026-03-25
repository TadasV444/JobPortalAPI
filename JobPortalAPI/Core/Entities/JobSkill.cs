using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortalAPI.Core.Entities;

[Table("JobSkills")]
public class JobSkill
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Required]
    [Column("job_posting_id")]
    public int JobPostingId { get; set; }
    [Required]
    [Column("skill_id")]
    public int SkillId { get; set; }
    // Navigation Property
    public JobPosting JobPosting { get; set; } = null!;
    
    public Skill Skill { get; set; } = null!;
}