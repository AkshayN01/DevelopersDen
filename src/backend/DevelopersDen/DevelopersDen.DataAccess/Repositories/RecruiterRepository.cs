using DevelopersDen.Contracts.DBModels.Recruiter;
using DevelopersDen.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace DevelopersDen.DataAccess.Repositories
{
    public class RecruiterRepository : GenericRepository<Recruiter>, IRecruiterRepository
    {
        public RecruiterRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Guid>> GetRecruiterIdByName(string name)
        {
            return await _dbSet
                .Where(x => x.CompanyName.StartsWith(name) || x.CompanyName.Contains(name))
                .Select(x => x.RecruiterId).ToListAsync();
        }
    }
}
