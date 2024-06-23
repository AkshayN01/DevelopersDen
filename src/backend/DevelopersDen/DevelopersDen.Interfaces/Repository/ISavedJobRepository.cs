using DevelopersDen.Contracts.DBModels.Job;

namespace DevelopersDen.Interfaces.Repository
{
    public interface ISavedJobRepository
    {
        Task<IEnumerable<SavedJob>> GetAll(Guid SeekerId);
    }
}
