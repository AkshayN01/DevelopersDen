using DevelopersDen.Contracts.DBModels.Job;
using System.Linq.Expressions;

namespace DevelopersDen.Interfaces.Repository
{
    public interface IJobRepository : IGenericRepository<Job>
    {
        Task<List<Job>> FilterJobs(Expression<Func<Job, bool>> condition, int pageNumber, int pageSize, bool applyPagination = true);
        Task<IEnumerable<Job>> GetAllJobs(Guid Id, bool IsRecruiter);
        Task DeactivateJob(Guid id);
    }
}
