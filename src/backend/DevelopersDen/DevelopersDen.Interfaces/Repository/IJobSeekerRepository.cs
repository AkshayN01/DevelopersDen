using DevelopersDen.Contracts.DBModels.Job;
using DevelopersDen.Contracts.DBModels.JobSeeker;

namespace DevelopersDen.Interfaces.Repository
{
    public interface IJobSeekerRepository
    {
        Task<JobSeeker> Login(string emailId, string password, string googleId);
    }
}
