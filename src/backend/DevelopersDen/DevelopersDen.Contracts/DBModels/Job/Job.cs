using DevelopersDen.Contracts.DBModels.Recruiter;
using System.ComponentModel.DataAnnotations;

namespace DevelopersDen.Contracts.DBModels.Job
{
    public class Job : AuditableEntity
    {
        [Key]
        public Guid JobId { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public string JobDescription { get; set; }
        public Int32 MinExperience { get; set; }
        [Required]
        public List<string> KeySkills { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string City { get; set; }
        public DateTime PostedDate { get; set; }
        public Guid RecruiterAccountId { get; set; }
        public RecruiterAccount RecruiterAccount { get; set; } = null!;
        public ICollection<JobApplication> Applications { get; set; } = new List<JobApplication>();
    }
}
