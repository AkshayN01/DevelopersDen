using AutoMapper;
using DevelopersDen.Contracts.DBModels.JobSeeker;
using DevelopersDen.Contracts.DTOs.JobSeeker.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersDen.Library.Profiles
{
    public class JobSeekerAMProfile : Profile
    {
        public JobSeekerAMProfile() 
        {
            CreateMap<JobSeeker, JobSeekerDTO>();
        }
    }
}
