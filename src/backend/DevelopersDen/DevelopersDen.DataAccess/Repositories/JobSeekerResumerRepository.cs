using DevelopersDen.Contracts.DBModels.Job;
using DevelopersDen.Contracts.DBModels.JobSeeker;
using DevelopersDen.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersDen.DataAccess.Repositories
{
    public class JobSeekerResumerRepository : GenericRepository<JobSeekerResume>, IJobSeekerResumerRepository
    {
        public JobSeekerResumerRepository(ApplicationDbContext context) : base(context)
        {
        }
         
        public async Task<JobSeekerResume?> GetBySeekerProfileId(Guid seekerProfileId)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.JobSeekerProfileId == seekerProfileId);
        }
    }
}
