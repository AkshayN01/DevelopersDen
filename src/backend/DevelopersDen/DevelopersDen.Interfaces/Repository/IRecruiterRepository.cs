using DevelopersDen.Contracts.DBModels.Recruiter;

namespace DevelopersDen.Interfaces.Repository
{
    public interface IRecruiterRepository : IGenericRepository<Recruiter>
    {
        Task<List<Guid>> GetRecruiterIdByName(string name);
    }
}
