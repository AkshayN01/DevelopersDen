using DevelopersDen.Contracts.DBModels.Job;
using DevelopersDen.Contracts.DBModels.JobSeeker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersDen.Interfaces.Repository
{
    public interface IJobSeekerResumerRepository : IGenericRepository<JobSeekerResume>
    {
        Task<JobSeekerResume?> GetBySeekerProfileId(Guid seekerProfileId);
    }
}
