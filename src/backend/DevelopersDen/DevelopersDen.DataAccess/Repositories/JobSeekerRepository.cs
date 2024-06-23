using DevelopersDen.Contracts.DBModels.JobSeeker;
using DevelopersDen.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace DevelopersDen.DataAccess.Repositories
{
    public class JobSeekerRepository : GenericRepository<JobSeeker>, IJobSeekerRepository
    {
        public JobSeekerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<JobSeeker?> Login(string emailId, string password, string googleId)
        {
            if (googleId == null)
                return await _dbSet.FirstOrDefaultAsync(x => x.Email == emailId && x.PasswordHash == password);
            else
                return await _dbSet.FirstOrDefaultAsync(x => x.Email == emailId && x.GoogleId == googleId);

        }
    }
}
