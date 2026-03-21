using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortalAPI.Core.Entities;

[Table("JobPostings")]
public class JobPosting
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Required]
    [Column("employer_profile_id")]
    public int EmployerProfileId { get; set; }
    [Required]
    [Column("job_description")]
    public string JobDescription { get; set; } = string.Empty;
    [Required]
    [Column("title")]
    public string Title { get; set; } = string.Empty;

    public EmployerProfile EmployerProfile { get; set; } = null!;
    
    public virtual ICollection<JobSkill> JobSkills { get; set; } = new List<JobSkill>();
    
    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
    
}