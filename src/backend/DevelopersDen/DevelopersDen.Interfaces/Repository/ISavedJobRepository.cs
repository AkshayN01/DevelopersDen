using DevelopersDen.Contracts.DBModels.Job;

namespace DevelopersDen.Interfaces.Repository
{
    public interface ISavedJobRepository : IGenericRepository<SavedJob>
    {
        Task<IEnumerable<SavedJob>> GetAll(Guid SeekerId);
    }
}
