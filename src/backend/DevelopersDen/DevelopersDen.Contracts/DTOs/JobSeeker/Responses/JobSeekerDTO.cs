﻿using DevelopersDen.Contracts.DBModels.JobSeeker;
using System.ComponentModel.DataAnnotations;

namespace DevelopersDen.Contracts.DTOs.JobSeeker.Responses
{
    public class JobSeekerDTO
    {
        public Guid Guid { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public JobSeekerProfileDTO JobSeekerProfile { get; set; }
    }

    public class JobSeekerProfileDTO
    {
        public Guid JobSeekerProfileId { get; set; }
        public String Summary { get; set; }
        public byte[] Resume { get; set; }
        public List<WorkExperienceDTO> WorkExperience { get; set; } = new List<WorkExperienceDTO>();
        public List<string> KeySkills { get; set; } = new List<string>();
    }
    public class WorkExperienceDTO
    {
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public string WorkDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int IsCurrent { get; set; }
    }
}
