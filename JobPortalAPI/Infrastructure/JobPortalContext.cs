using JobPortalAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobPortalAPI.Infractructure;

public class JobPortalContext(DbContextOptions <JobPortalContext> options) :DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<CandidateProfile> CandidateProfiles { get; set; }
    public DbSet<CandidateSkill> CandidateSkills { get; set; }
    public DbSet<EmployerProfile> EmployerProfiles { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Application> Applications { get; set; }
    public DbSet<JobPosting> JobPostings { get; set; }
    public DbSet<JobSkill> JobSkills { get; set; }

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
        //Skill -> JobSkill
            modelBuilder.Entity<Skill>()
                .HasMany(s => s.JobSkills)
                .WithOne(js => js.Skill)
                .HasForeignKey(js => js.SkillId)
                .OnDelete(DeleteBehavior.Cascade);
        //JobPosting -> JobSkill
            modelBuilder.Entity<JobPosting>()
                .HasMany(js => js.JobSkills)
                .WithOne(js => js.JobPosting)
                .HasForeignKey(js => js.JobPostingId)
                .OnDelete(DeleteBehavior.Cascade);
        //JobPosting -> Application
            modelBuilder.Entity<JobPosting>()
                .HasMany(j => j.Applications)
                .WithOne(a => a.JobPosting)
                .HasForeignKey(a => a.JobPostingId)
                .OnDelete(DeleteBehavior.Cascade);
        //EmployerProfile -> JobPosting
            modelBuilder.Entity<EmployerProfile>()
                .HasMany(j => j.JobPostings)
                .WithOne(jp => jp.EmployerProfile)
                .HasForeignKey(jp => jp.EmployerProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        //CandidateProfile -> Application
            modelBuilder.Entity<CandidateProfile>()
                .HasMany(a => a.Applications)
                .WithOne(a => a.CandidateProfile)
                .HasForeignKey(a => a.CandidateProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        //User -> CandidateProfile
            modelBuilder.Entity<User>()
                .HasOne(u  => u.CandidateProfile)
                .WithOne(c => c.User)
                .HasForeignKey<CandidateProfile>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        //User -> EmployerProfile
            modelBuilder.Entity<User>()
                .HasOne(u => u.EmployerProfile)
                .WithOne(e => e.User)
                .HasForeignKey<EmployerProfile>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                
        #endregion
        
        #region Indexes

        modelBuilder.Entity<CandidateProfile>()
            .HasIndex(c => c.UserId)
            .IsUnique(); // Because one user can have exactly one profile
        modelBuilder.Entity<CandidateSkill>()
            .HasIndex(cs => cs.CandidateProfileId);
        modelBuilder.Entity<CandidateSkill>()
            .HasIndex(cs => cs.SkillId); // Because a candidate can have multiple skills
        modelBuilder.Entity<Skill>()
            .HasIndex(s => s.Name)
            .IsUnique(); // Skill names should be unique without duplicates 
        modelBuilder.Entity<JobPosting>()
            .HasIndex(js => js.EmployerProfileId);
        modelBuilder.Entity<Application>()
            .HasIndex(a => a.CandidateProfileId);
        modelBuilder.Entity<Application>()
            .HasIndex(a => a.JobPostingId);
        modelBuilder.Entity<JobSkill>()
            .HasIndex(js => js.JobPostingId);
        modelBuilder.Entity<JobSkill>()
            .HasIndex(js => js.SkillId);
        modelBuilder.Entity<EmployerProfile>()
            .HasIndex(e => e.UserId);
        modelBuilder.Entity<CandidateSkill>()
            .HasIndex(cs => new { cs.CandidateProfileId, cs.SkillId })
            .IsUnique();
        modelBuilder.Entity<Application>()
            .HasIndex(a => new { a.CandidateProfileId, a.JobPostingId })
            .IsUnique();
        modelBuilder.Entity<JobSkill>()
            .HasIndex(js => new { js.JobPostingId, js.SkillId })
            .IsUnique();
        
        #endregion
    }
}