using DevelopersDen.Contracts.DBModels.Job;
using DevelopersDen.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace DevelopersDen.DataAccess.Repositories
{
    public class JobRepository : GenericRepository<Job>, IJobRepository
    {
        public JobRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task DeactivateJob(Guid id)
        {
            var job = await _dbSet.FindAsync(id);
            if(job == null)
                throw new Exception("No such job found");

            job.IsActive = 0;
            await UpdateAsync(job);
        }

        public async Task<List<Job>> FilterJobs(Expression<Func<Job, bool>> condition, int pageNumber, int pageSize, bool applyPagination = true, bool tracked = false)
        {
            IQueryable<Job> query = _dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            query = query.Where(condition);
            if (applyPagination)
                query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetAllJobs(Guid Id, bool IsRecruiter)
        {
            if(IsRecruiter)
                return await _dbSet.Where(x => x.RecruiterId == Id).ToListAsync();
            else
                return await _dbSet.Where(x => x.RecruiterAccountId == Id).ToListAsync();
        }
    }
}
