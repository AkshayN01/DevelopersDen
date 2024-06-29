using DevelopersDen.Contracts.DBModels;
using DevelopersDen.Contracts.DBModels.Job;
using DevelopersDen.Contracts.DBModels.JobSeeker;
using DevelopersDen.Contracts.DBModels.Recruiter;
using DevelopersDen.DataAccess.Repositories;
using DevelopersDen.Interfaces.Repository;

namespace DevelopersDen.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IJobSeekerRepository? jobSeekerRepository;
        private IGenericRepository<JobSeekerProfile>? jobSeekerProfileRepository;
        private IJobSeekerResumerRepository? jobSeekerResumeRepository;
        private IJobApplicationRepository? jobApplicationRepository;
        private ISavedJobRepository? savedJobRepository;

        private IRecruiterRepository? recruiterRepository;
        private IJobRepository? jobRepository;
        private IRecruiterAccountRepository? recruiterAccountRepository;
        private IGenericRepository<Subscription>? subscriptionRepository;

        private IGenericRepository<ApplicationStatus>? applicationStatusRepository;
        private IGenericRepository<Stakeholder>? stakeholderepository;

        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public IJobSeekerRepository _JobSeekerRepository => jobSeekerRepository ??= new JobSeekerRepository(_context);

        public IGenericRepository<JobSeekerProfile> _JobSeekerProfileRepository => jobSeekerProfileRepository ??= new GenericRepository<JobSeekerProfile>(_context);
        public IJobSeekerResumerRepository _JobSeekerResumeRepository => jobSeekerResumeRepository ??= new JobSeekerResumerRepository(_context);

        public IJobApplicationRepository _JobApplicationRepository => jobApplicationRepository ??= new JobApplicationRepository(_context);

        public IGenericRepository<ApplicationStatus> _ApplicationStatusRepository => applicationStatusRepository ??= new GenericRepository<ApplicationStatus>(_context);

        public ISavedJobRepository _SavedJobRepository => savedJobRepository ??= new SavedJobRepository(_context);

        public IRecruiterRepository _RecruiterRepository => recruiterRepository ??= new RecruiterRepository(_context);

        public IJobRepository _JobRepository => jobRepository ??= new JobRepository(_context);

        public IRecruiterAccountRepository _RecruiterAccountRepository => recruiterAccountRepository ??= new RecruiterAccountRepository(_context);

        public IGenericRepository<Subscription> _SubscriptionRepository => subscriptionRepository ??= new GenericRepository<Subscription>(_context);
        public IGenericRepository<Stakeholder> _StakeholderRepository => stakeholderepository ??= new GenericRepository<Stakeholder>(_context);

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }
    }
}
