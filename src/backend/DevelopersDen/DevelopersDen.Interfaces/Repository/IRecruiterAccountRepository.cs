using DevelopersDen.Contracts.DBModels.Recruiter;

namespace DevelopersDen.Interfaces.Repository
{
    public interface IRecruiterAccountRepository : IGenericRepository<RecruiterAccount>
    {
        Task<RecruiterAccount> GetRecruiterAccount(string emailId, string password);
    }
}
