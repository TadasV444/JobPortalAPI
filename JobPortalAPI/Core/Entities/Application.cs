using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortalAPI.Core.Entities;

[Table("Applications")]
public class Application
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Required]
    [Column("")]
    public int CandidateProfileId { get; set; }
    [Required]
    [Column("job_posting_id")]
    public int JobPostingId { get; set; }
    [Required]
    [Column("applied_date")]
    public DateTime AppliedDate { get; set; }
    [Required]
    [Column("status")]
    public string Status { get; set; } = string.Empty;
    
    public CandidateProfile CandidateProfile { get; set; } = null!;
    public JobPosting JobPosting { get; set; } = null!;
}