using JobPortalAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobPortalAPI.Infractructure;

public class JobPortalContext(DbContextOptions <JobPortalContext> options) :DbContext(options)
{
    public DbSet<CandidateProfile> CandidateProfiles { get; set; }
    public DbSet<CandidateSkill> CandidateSkills { get; set; }
    public DbSet<EmployerProfile> EmployerProfiles { get; set; }
    public DbSet<Skill> Skills { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Relationships
        //CandidateProfile -> CandidateSkill
            modelBuilder.Entity<CandidateProfile>()
                .HasMany(c => c.CandidateSkills)
                .WithOne(cs => cs.CandidateProfile)
                .HasForeignKey(cs => cs.CandidateProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        //Skill -> CandidateSkill
            modelBuilder.Entity<Skill>()
                .HasMany(s => s.CandidateSkills)
                .WithOne(cs => cs.Skill)
                .HasForeignKey(cs => cs.SkillId)
                .OnDelete(DeleteBehavior.Cascade);
        #endregion
        
        #region Indexes

        modelBuilder.Entity<CandidateProfile>()
            .HasIndex(c => c.UserId)
            .IsUnique(); // Because one user can have exactly one profile
        modelBuilder.Entity<CandidateSkill>()
            .HasIndex(cs => cs.CandidateProfileId);
        modelBuilder.Entity<Skill>()
            .HasIndex(s => s.Name)
            .IsUnique(); // Because the same skill can apply only once to a candidate profile to prevent duplicates

        #endregion
    }
}