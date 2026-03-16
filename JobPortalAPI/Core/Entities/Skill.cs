using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobPortalAPI.Core.Entities;
[Table("Skills")]
public class Skill
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Required]
    [Column("name")]
    public string Name { get; set; } = string.Empty;
    
    public ICollection<CandidateSkill> CandidateSkills { get; set; } = new List<CandidateSkill>();
}