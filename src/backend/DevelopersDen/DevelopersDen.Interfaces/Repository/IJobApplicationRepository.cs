using DevelopersDen.Contracts.DBModels.Job;

namespace DevelopersDen.Interfaces.Repository
{
    public interface IJobApplicationRepository : IGenericRepository<JobApplication>
    {
        IEnumerable<JobApplication> GetAllByJobSeekerId(Guid jobSeekerId, bool includeJobDetails = false);
        Task<JobApplication?> GetByJobAndSeekerId(Guid seekerId, Guid jobId);
    }
}
