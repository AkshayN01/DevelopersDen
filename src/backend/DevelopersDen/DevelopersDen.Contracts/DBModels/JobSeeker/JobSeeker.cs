using System.ComponentModel.DataAnnotations;

namespace DevelopersDen.Contracts.DBModels.JobSeeker
{
    public class JobSeeker : AuditableEntity
    {
        [Key]
        public long JobSeekerId { get; set; }
        public Guid JobSeekerGuid { get; set; }

        [Required(ErrorMessage = "User first name is required")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "User middle name is required")]
        public string MiddleName { get; set; } = string.Empty;
        [Required(ErrorMessage = "User last name is required")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "EmailId is required")]
        public string EmailId { get; set; } = string.Empty;
        [Required(ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; } = string.Empty;
        public int IsEmailVerified { get; set; }
        public Int64 JobSeekerProfileId { get; set; }
        public JobSeekerProfile JobSeekerProfile { get; set; }
        public int StakeholderId { get; set; }
        public Stakeholder Stakeholder { get; set; } = new Stakeholder();
    }
}
