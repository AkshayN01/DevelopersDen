using DevelopersDen.Contracts.DBModels;
using DevelopersDen.Contracts.DBModels.Job;
using DevelopersDen.Contracts.DBModels.JobSeeker;
using DevelopersDen.Contracts.DBModels.Recruiter;

namespace DevelopersDen.Interfaces.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        #region "Job Seeker"

        IJobSeekerRepository _JobSeekerRepository { get; }
        IGenericRepository<JobSeekerProfile> _JobSeekerProfileRepository { get; }
        IJobApplicationRepository _JobApplicationRepository { get; }
        IGenericRepository<ApplicationStatus> _ApplicationStatusRepository { get; }
        ISavedJobRepository _SavedJobRepository { get; }

        #endregion

        #region "Recruiter"

        IRecruiterRepository _RecruiterRepository { get; }
        IJobRepository _JobRepository { get; }
        IRecruiterAccountRepository _RecruiterAccountRepository { get; }
        IGenericRepository<Subscription> _SubscriptionRepository { get; }

        #endregion

        IGenericRepository<Stakeholder> _StakeholderRepository { get; }

        void Commit();
        void Rollback();
    }
}
