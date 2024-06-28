using System.ComponentModel.DataAnnotations;

namespace DevelopersDen.Contracts.DBModels.Recruiter
{
    public class RecruiterAccount : AuditableEntity
    {
        public Guid Id { get; set; }
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string Password { get; set; }
        public string? VerificationToken { get; set; }
        public Int32 IsVerified { get; set; }
        public Int32 IsPrimary { get; set; }
        public Guid RecruiterId { get; set; } //Required Foriegn Key
        public Recruiter Recruiter { get; set; } = null!; //Required
        public ICollection<Job.Job> Jobs { get; set; } = new List<Job.Job>();
    }
}
