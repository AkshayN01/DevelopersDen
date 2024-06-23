using System.ComponentModel.DataAnnotations;

namespace DevelopersDen.Contracts.DBModels.Job
{
    public class JobApplication
    {
        [Key]
        public Guid JobApplicationId { get; set; }
        public string Comments { get; set; }
        public Int32 ApplicationStatusId { get; set; }
        public Guid JobId { get; set; }
        public Guid JobSeekerId { get; set; }
        public Job Job { get; set; } = null!;
        public JobSeeker.JobSeeker JobSeeker { get; set; } = null!; 
    }
}
