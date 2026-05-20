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
    [StringLength(250)]
    public string JobDescription { get; set; } = string.Empty;
    [StringLength(120)]
    [Required]
    [Column("title")]
    public string Title { get; set; } = string.Empty;
    [StringLength(120)]
    [Required]
    [Column("location")]
    public string Location { get; set; } = string.Empty;
    [Required]
    [Column("salary_from")]
    public decimal? SalaryFrom { get; set; }
    [Required]
    [Column("salary_to")]
    public decimal? SalaryTo { get; set; }
    // Navigation Property
    public EmployerProfile EmployerProfile { get; set; } = null!;
    // Collection Navigation Property
    public virtual ICollection<JobSkill> JobSkills { get; set; } = new List<JobSkill>();
    
    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
    
}