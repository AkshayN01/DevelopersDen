namespace DevelopersDen.Contracts.DBModels.Job
{
    public class SavedJob
    {
        public Guid JobId { get; set; }
        public Guid JobSeekerId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
