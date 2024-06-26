using DevelopersDen.Contracts.DBModels.Job;
using DevelopersDen.Contracts.DBModels.JobSeeker;

namespace DevelopersDen.Interfaces.Repository
{
    public interface IJobSeekerRepository : IGenericRepository<JobSeeker>
    {
        Task<JobSeeker> Login(string emailId, string googleId);
        Task<JobSeeker?> GetSeekerDetailsAsync(Guid seekerId);
    }
}
