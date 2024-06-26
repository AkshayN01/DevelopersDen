using DevelopersDen.Contracts.DBModels;
using DevelopersDen.Contracts.DBModels.Job;
using DevelopersDen.Contracts.DBModels.Recruiter;
using DevelopersDen.Contracts.DTOs.Recruiter.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersDen.Contracts.DTOs.JobSeeker.Responses
{
    public class JobApplicationDTO
    {
        public Guid ApplicationId { get; set; }
        public string Comments { get; set; }
        public Int32 ApplicationStatusId { get; set; }
        public JobDTO Job { get; set; } = new JobDTO();
    }

    public class JobDTO
    {
        public Guid JobId { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public Int32 MinExperience { get; set; }
        public List<string> KeySkills { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public int JobType { get; set; }
        public DateTime PostedDate { get; set; }
        public RecruiterDTO Recruiter { get; set; } = new RecruiterDTO();
    }
}
