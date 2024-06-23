using System.ComponentModel.DataAnnotations;

namespace DevelopersDen.Contracts.DBModels.Recruiter
{
    public class RecruiterAccount : AuditableEntity
    {
        public Int64 Id { get; set; }
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string Password { get; set; }
        public string VerificationToken { get; set; }
        public Int32 IsVerified { get; set; }
        public Int32 IsPrimary { get; set; }
        public Int64 RecruiterId { get; set; }
        public Recruiter Recruiter { get; set; }
    }
}
