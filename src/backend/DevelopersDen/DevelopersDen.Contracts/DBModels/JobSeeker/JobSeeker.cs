using System.ComponentModel.DataAnnotations;

namespace DevelopersDen.Contracts.DBModels.JobSeeker
{
    public class JobSeeker : AuditableEntity
    {
        [Key]
        public Guid JobSeekerId { get; set; }

        [Required(ErrorMessage = "User name is required")]
        public string Name { get; set; } = string.Empty;
        public string PasswordHash { get; set; } //can be null if google user
        [Required(ErrorMessage = "EmailId is required")]
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string GoogleId { get; set; } //can be null for non-google users
        public string ProfilePictureUrl { get; set; }
        public DateTime? LastLogin { get; set; }
        public int IsEmailVerified { get; set; }
        public JobSeekerProfile? JobSeekerProfile { get; set; }
        public ICollection<Job.JobApplication> JobApplications { get; set; } = new List<Job.JobApplication>();
        public Int32 StakeholderId { get; set; }
    }
}
