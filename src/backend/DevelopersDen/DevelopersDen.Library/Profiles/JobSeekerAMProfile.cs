using AutoMapper;
using DevelopersDen.Contracts.DBModels.Job;
using DevelopersDen.Contracts.DBModels.JobSeeker;
using DevelopersDen.Contracts.DBModels.Recruiter;
using DevelopersDen.Contracts.DTOs.JobSeeker.Requests;
using DevelopersDen.Contracts.DTOs.JobSeeker.Responses;
using DevelopersDen.Contracts.DTOs.Recruiter.Responses;
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
            #region " resposnes "

            CreateMap<JobSeeker, JobSeekerDTO>()
                .ForMember(dest => dest.Guid, src => src.MapFrom(x => x.JobSeekerId))
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.EmailId, src => src.MapFrom(x => x.Email))
                .ForMember(dest => dest.PhoneNumber, src => src.MapFrom(x => x.PhoneNumber))
                .ForMember(dest => dest.GoogleId, src => src.MapFrom(x => x.GoogleId));

            CreateMap<Job, JobDTO>()
                .ForMember(dest => dest.JobId, src => src.MapFrom(x => x.JobId))
                .ForMember(dest => dest.PostedDate, src => src.MapFrom(x => x.PostedDate))
                .ForMember(dest => dest.MinExperience, src => src.MapFrom(x => x.MinExperience))
                .ForMember(dest => dest.City, src => src.MapFrom(x => x.City))
                .ForMember(dest => dest.JobDescription, src => src.MapFrom(x => x.JobDescription))
                .ForMember(dest => dest.JobTitle, src => src.MapFrom(x => x.JobTitle))
                .ForMember(dest => dest.JobType, src => src.MapFrom(x => x.JobType))
                .ForMember(dest => dest.KeySkills, src => src.MapFrom(x => x.KeySkills))
                .ForMember(dest => dest.Location, src => src.MapFrom(x => x.Location));

            CreateMap<Recruiter, RecruiterDTO>()
                .ForMember(dest => dest.CompanyName, src => src.MapFrom(x => x.CompanyName))
                .ForMember(dest => dest.CompanyDescription, src => src.MapFrom(x => x.CompanyDescription))
                .ForMember(dest => dest.WebsiteUrl, src => src.MapFrom(x => x.WebsiteUrl));

            CreateMap<JobApplication, JobApplicationDTO>()
                .ForMember(dest => dest.ApplicationStatusId, src => src.MapFrom(x => x.ApplicationStatusId))
                .ForMember(dest => dest.ApplicationId, src => src.MapFrom(x => x.JobApplicationId))
                .ForMember(dest => dest.Comments, src => src.MapFrom(x => x.Comments));

            #endregion

            #region " requests "

            CreateMap<SeekerProfileRequest, JobSeekerProfile>()
                .ForMember(dest => dest.KeySkills, src => src.MapFrom(x => x.KeySkills))
                .ForMember(dest => dest.Resume, src => src.MapFrom(x => x.Resume))
                .ForMember(dest => dest.Summary, src => src.MapFrom(x => x.Summary));

            CreateMap<WorkExperienceRequestDTO, WorkExperience>()
                .ForMember(dest => dest.CompanyName, src => src.MapFrom(x => x.CompanyName))
                .ForMember(dest => dest.Designation, src => src.MapFrom(x => x.Designation))
                .ForMember(dest => dest.StartDate, src => src.MapFrom(x => x.StartDate))
                .ForMember(dest => dest.EndDate, src => src.MapFrom(x => x.EndDate))
                .ForMember(dest => dest.WorkDescription, src => src.MapFrom(x => x.WorkDescription))
                .ForMember(dest => dest.IsCurrent, src => src.MapFrom(x => x.IsCurrent));

            CreateMap<JobSeekerResgisterRequest, JobSeeker>()
                .ForMember(dest => dest.Email, src => src.MapFrom(x => x.Email))
                .ForMember(dest => dest.PhoneNumber, src => src.MapFrom(x => x.PhoneNumber))
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.GoogleId, src => src.MapFrom(x => x.GoogleId));

            #endregion
        }
    }
}
