using DevelopersDen.Contracts.DBModels.Job;
using System.Linq.Expressions;

namespace DevelopersDen.Interfaces.Repository
{
    public interface IJobRepository : IGenericRepository<Job>
    {
        Task<IEnumerable<Job>> FilterJobs(Expression<Func<Job, bool>> condition);
        Task<IEnumerable<Job>> GetAllJobs(Guid Id, bool IsRecruiter);
        Task DeactivateJob(Guid id);
    }
}
