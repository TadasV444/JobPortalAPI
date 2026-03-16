using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobPortalAPI.Core.Entities;

[Table("CandidateSkills")]

public class CandidateSkill
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Required]
    [Column("candidate_profile_id")]
    public int CandidateProfileId { get; set; }
    
    public CandidateProfile CandidateProfile { get; set; } = null!;
    [Required]
    [Column("skill_id")]
    public int SkillId { get; set; }

    public Skill Skill { get; set; } = null!;


}