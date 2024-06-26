using DevelopersDen.Contracts.DBModels.Job;
using DevelopersDen.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace DevelopersDen.DataAccess.Repositories
{
    public class JobApplicationRepository : GenericRepository<JobApplication>, IJobApplicationRepository
    {
        public JobApplicationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<JobApplication> GetAllByJobSeekerId(Guid jobSeekerId, bool includeJobDetails = false)
        {
            if(includeJobDetails)
                return _dbSet.Where(x => x.JobSeekerId == jobSeekerId).Include(x => x.Job).AsEnumerable();
            else
                return _dbSet.Where(x => x.JobSeekerId == jobSeekerId).AsEnumerable();
        }

        public Task<JobApplication?> GetByJobAndSeekerId(Guid seekerId, Guid jobId)
        {
            return _dbSet.FirstOrDefaultAsync(x => x.JobId == jobId && x.JobSeekerId == seekerId);
        }
    }
}
