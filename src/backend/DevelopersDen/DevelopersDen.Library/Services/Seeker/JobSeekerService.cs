using DevelopersDen.Contracts.DBModels.JobSeeker;
using DevelopersDen.Interfaces.Repository;

namespace DevelopersDen.Library.Services.Seeker
{
    public class JobSeekerService
    {
        private readonly IJobSeekerRepository _jobSeekerRepository;
        public JobSeekerService(IJobSeekerRepository jobSeekerRepository) 
        { 
            _jobSeekerRepository = jobSeekerRepository;
        }

        public async Task<JobSeeker?> GetJobSeekerDetails(Guid seekerId, bool includeProfile = false)
        {
            JobSeeker? jobSeeker = null;
            if (includeProfile)
                jobSeeker = await _jobSeekerRepository.GetSeekerDetailsAsync(seekerId);
            else
                jobSeeker = await _jobSeekerRepository.GetByGuidAsync(seekerId);

            return jobSeeker;
        }

        public async Task<bool> VerifyEmailIsUnique(string emailId)
        {
            return false;
        }
    }
}
