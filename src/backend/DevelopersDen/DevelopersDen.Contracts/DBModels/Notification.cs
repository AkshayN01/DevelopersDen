using System.ComponentModel.DataAnnotations;

namespace DevelopersDen.Contracts.DBModels
{
    public class Notification
    {
        public Int64 Id { get; set; }
        public EmailNoti Noti { get; set; }
        public Int32 IsSent { get; set; }
        public Int32 StakeholderId { get; set; }
        public Stakeholder Stakeholder { get; set; }
    }
    public class EmailNoti
    {
        [Required]
        public string ToEmailId { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
    }
}
