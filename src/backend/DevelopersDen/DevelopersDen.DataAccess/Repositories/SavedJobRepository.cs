using DevelopersDen.Contracts.DBModels.Job;
using DevelopersDen.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace DevelopersDen.DataAccess.Repositories
{
    public class SavedJobRepository : GenericRepository<SavedJob>, ISavedJobRepository
    {
        public SavedJobRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<SavedJob>> GetAll(Guid SeekerId)
        {
            return await _dbSet.Where(x => x.JobSeekerId == SeekerId).ToListAsync();
        }
    }
}
