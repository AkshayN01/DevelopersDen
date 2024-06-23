using DevelopersDen.Contracts.DBModels;
using DevelopersDen.Contracts.DBModels.Job;
using DevelopersDen.Contracts.DBModels.JobSeeker;
using DevelopersDen.Contracts.DBModels.Recruiter;
using Microsoft.EntityFrameworkCore;

namespace DevelopersDen.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Stakeholder> Stakeholders { get; set; }
        public DbSet<JobSeeker> JobSeekers { get; set; }
        public DbSet<JobSeekerProfile> JobSeekersProfiles { get; set; }
        public DbSet<Recruiter> Recruiters { get; set; }
        public DbSet<RecruiterAccount> RecruiterAccounts { get; set; }
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<ApplicationStatus> ApplicationStatuses { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<SavedJob> SavedJobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobSeeker>()
            .HasIndex(u => u.Email)
            .IsUnique();

            modelBuilder.Entity<Recruiter>()
            .HasIndex(u => u.EmailId)
            .IsUnique();

            modelBuilder.Entity<RecruiterAccount>()
            .HasIndex(u => u.EmailId)
            .IsUnique();

            modelBuilder.Entity<Subscription>()
                .HasOne(e => e.Plan)
                .WithMany(e => e.Subscriptions)
                .HasForeignKey(e => e.SubscriptionPlanId)
                .IsRequired();

            modelBuilder.Entity<RecruiterAccount>()
                .HasOne(e => e.Recruiter)
                .WithMany(e => e.RecruiterAccounts)
                .HasForeignKey(e => e.RecruiterId)
                .IsRequired();

            modelBuilder.Entity<Subscription>()
                .HasOne(e => e.Recruiter)
                .WithMany(e => e.Subscriptions)
                .HasForeignKey(e => e.RecruiterId)
                .IsRequired();

            modelBuilder.Entity<Job>()
                .HasOne(e => e.RecruiterAccount)
                .WithMany(e => e.Jobs)
                .HasForeignKey(e => e.RecruiterAccountId)
                .IsRequired();


            modelBuilder.Entity<JobSeeker>()
                .HasOne(e => e.JobSeekerProfile)
                .WithOne(e => e.JobSeeker)
                .HasForeignKey<JobSeekerProfile>(e => e.JobSeekerId)
                .IsRequired();

            modelBuilder.Entity<JobApplication>()
                .HasOne(e => e.JobSeeker)
                .WithMany(e => e.JobApplications)
                .HasForeignKey(e => e.JobSeekerId)
                .IsRequired();

            modelBuilder.Entity<JobApplication>()
                .HasOne(e => e.Job)
                .WithMany(e => e.Applications)
                .HasForeignKey(e => e.JobId)
                .IsRequired();

        }

    }
}
