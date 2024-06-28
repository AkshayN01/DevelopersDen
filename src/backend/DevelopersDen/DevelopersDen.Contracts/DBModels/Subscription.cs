using System.ComponentModel.DataAnnotations;

namespace DevelopersDen.Contracts.DBModels
{
    public class Subscription
    {
        [Key]
        public Int64 SubscriptionId { get; set; }
        [Required]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        [Required]
        public DateTime EndDate { get; set; }
        public Int32 IsActive { get; set; }
        public Guid RecruiterId { get; set; }
        public Recruiter.Recruiter Recruiter { get; set; } = null!;
        public Int32 SubscriptionPlanId { get; set; }
        public SubscriptionPlan Plan { get; set; } = null!;
    }
}
