using System.ComponentModel.DataAnnotations;

namespace DevelopersDen.Contracts.DBModels.Job
{
    public class SavedJob
    {
        [Key]
        public Guid Guid { get; set; }
        public Guid JobId { get; set; }
        public Guid JobSeekerId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
