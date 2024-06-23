using DevelopersDen.Contracts.DBModels.Recruiter;

namespace DevelopersDen.Interfaces.Repository
{
    public interface IRecruiterRepository
    {
        Task<RecruiterAccount> GetRecruiterAccount(string emailId, string password);
    }
}
