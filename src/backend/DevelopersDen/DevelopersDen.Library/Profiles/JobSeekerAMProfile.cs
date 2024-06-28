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
            CreateMap<JobSeeker, JobSeekerDTO>()
                .ForMember(dest => dest.Guid, src => src.MapFrom(x => x.JobSeekerId))
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.JobSeekerId))
                .ForMember(dest => dest.EmailId, src => src.MapFrom(x => x.JobSeekerId))
                .ForMember(dest => dest.PhoneNumber, src => src.MapFrom(x => x.JobSeekerId));

            CreateMap<JobSeekerProfile, JobSeekerProfileDTO>()
                .ForMember(dest => dest.JobSeekerProfileId, src => src.MapFrom(x => x.JobSeekerId))
                .ForMember(dest => dest.Resume, src => src.MapFrom(x => x.JobSeekerId))
                .ForMember(dest => dest.Summary, src => src.MapFrom(x => x.JobSeekerId));

            CreateMap<WorkExperience, WorkExperienceDTO>()
                .ForMember(dest => dest.CompanyName, src => src.MapFrom(x => x.CompanyName))
                .ForMember(dest => dest.Designation, src => src.MapFrom(x => x.Designation))
                .ForMember(dest => dest.StartDate, src => src.MapFrom(x => x.StartDate))
                .ForMember(dest => dest.EndDate, src => src.MapFrom(x => x.EndDate))
                .ForMember(dest => dest.WorkDescription, src => src.MapFrom(x => x.WorkDescription))
                .ForMember(dest => dest.IsCurrent, src => src.MapFrom(x => x.IsCurrent));
        }
    }
}
