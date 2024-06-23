using System.ComponentModel.DataAnnotations;

namespace DevelopersDen.Contracts.DBModels.Recruiter
{
    public class Recruiter : AuditableEntity
    {
        [Key]
        public Int64 RecruiterId { get; set; }
        public Guid RecruiterGuid { get; set; }
        [Required(ErrorMessage = "Company name is required")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Company description is required")]
        public string CompanyDescription { get; set; }
        [Required(ErrorMessage = "Company address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Company city is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "Company emailId is required")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "Company website url is required")]
        public string WebsiteUrl { get; set; }
        public Int32 StakeholderId { get; set; }
        public Stakeholder Stakeholder { get; set; }
    }
}
