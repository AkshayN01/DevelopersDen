using AutoMapper;
using DevelopersDen.Contracts.Application;
using DevelopersDen.Contracts.DBModels.JobSeeker;
using DevelopersDen.Contracts.DTOs;
using DevelopersDen.Contracts.DTOs.JobSeeker.Requests;
using DevelopersDen.Contracts.DTOs.JobSeeker.Responses;
using DevelopersDen.Contracts.Enums;
using DevelopersDen.Interfaces.Repository;
using DevelopersDen.Library.Authentication;
using DevelopersDen.Library.Generic;
using DevelopersDen.Library.Services.Seeker;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DevelopersDen.Blanket.JobSeeker
{
    public class SeekerProfileBL
    {
        private readonly IMapper _mapper;
        private readonly JobSeekerService _jobSeekerService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppSettings _appSettings;
        public SeekerProfileBL(IUnitOfWork unitOfWork, JobSeekerService jobSeekerService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _jobSeekerService = jobSeekerService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        public async Task<HTTPResponse> Login(SeeekrLoginRequest loginRequest)
        {
            string message = string.Empty;
            Int32 retVal = -40;

            Object? data = default(Object);

            try
            {
                if (String.IsNullOrEmpty(loginRequest.Password))
                    throw new Exception("Password is missing");

                Contracts.DBModels.JobSeeker.JobSeeker jobSeeker = await _unitOfWork._JobSeekerRepository.Login(loginRequest.Email);

                if (jobSeeker == null && String.IsNullOrEmpty(loginRequest.GoogleId))
                    throw new Exception("Invalid email id");
                else if (jobSeeker == null && !String.IsNullOrEmpty(loginRequest.GoogleId))
                {
                    JobSeekerResgisterRequest resgisterRequest = new JobSeekerResgisterRequest()
                    {
                        Name = loginRequest.Name,
                        Email = loginRequest.Email,
                        GoogleId = loginRequest.GoogleId
                    };
                    return await Register(resgisterRequest);
                }

                //check password
                bool IsValid = PasswordHasher.VerifyPassword(loginRequest.Password, jobSeeker.PasswordHash);

                //if login successfull update last login time
                if (!IsValid)
                    throw new Exception("Invalid password");

                jobSeeker.LastLogin = DateTime.UtcNow;

                await _unitOfWork._JobSeekerRepository.UpdateAsync(jobSeeker);
                _unitOfWork.Commit();

                var encrypterToken = JWTAuth.JWTTokenGenerator(_appSettings.Secret, jobSeeker.JobSeekerId.ToString());

                LoginResponse response = new LoginResponse() { Name = jobSeeker.Name, Token = encrypterToken };
                data = response;
                retVal = 1;
            }
            catch (Exception ex)
            {
                return Library.Generic.APIResponse.ConstructExceptionResponse(-40, ex.Message);
            }

            return Library.Generic.APIResponse.ConstructHTTPResponse(data, retVal, message);
        }

        public async Task<HTTPResponse> LoginWithGoogle(string credential)
        {
            string message = string.Empty;
            Int32 retVal = -40;

            Object? data = default(Object);

            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string> { _appSettings.GoogleClientId },
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(credential, settings);

                Contracts.DBModels.JobSeeker.JobSeeker jobSeeker = await _unitOfWork._JobSeekerRepository.Login(payload.Email);

                if(jobSeeker == null)
                {
                    //means the user has not been registered
                    JobSeekerResgisterRequest resgisterRequest = new JobSeekerResgisterRequest()
                    {
                        Email = payload.Email,
                        Name = payload.Name,
                    };

                    var registerResponse = await Register(resgisterRequest);
                    if(registerResponse != null && registerResponse.Data != null) 
                    {
                        jobSeeker = System.Text.Json.JsonSerializer.Deserialize<Contracts.DBModels.JobSeeker.JobSeeker>(System.Text.Json.JsonSerializer.Serialize(registerResponse.Data));
                    }
                    else
                    {
                        throw new Exception(registerResponse.Meta.Message);
                    }
                }

                var encrypterToken = JWTAuth.JWTTokenGenerator(_appSettings.Secret, jobSeeker.JobSeekerId.ToString());

                LoginResponse response = new LoginResponse() { Name = jobSeeker.Name, Token = encrypterToken };
                data = response;
                retVal = 1;
            }
            catch (Exception ex)
            {
                return Library.Generic.APIResponse.ConstructExceptionResponse(-40, ex.Message);
            }

            return Library.Generic.APIResponse.ConstructHTTPResponse(data, retVal, message);
        }

        public async Task<HTTPResponse> Register(JobSeekerResgisterRequest resgisterRequest)
        {
            string message = string.Empty;
            Int32 retVal = -40;

            Object? data = default(Object);

            try
            {
                Contracts.DBModels.JobSeeker.JobSeeker jobSeeker = new Contracts.DBModels.JobSeeker.JobSeeker();

                //map data from DTO
                _mapper.Map(resgisterRequest, jobSeeker);

                if (jobSeeker == null)
                    throw new Exception("data is null");

                if(!String.IsNullOrEmpty(resgisterRequest.Password))
                    jobSeeker.PasswordHash = PasswordHasher.HashPassword(resgisterRequest.Password);

                jobSeeker.JobSeekerId = Guid.NewGuid();
                jobSeeker.StakeholderId = (int)StakeholderEnum.JobSeeker;
                jobSeeker.CreatedAt = DateTime.UtcNow;
                jobSeeker.IsActive = 0;
                jobSeeker.IsEmailVerified = 0;


                await _unitOfWork._JobSeekerRepository.AddAsync(jobSeeker);
                _unitOfWork.Commit();

                data = true;
                retVal = 1;
            }
            catch (Exception ex)
            {
                return Library.Generic.APIResponse.ConstructExceptionResponse(-40, ex.Message);
            }

            return Library.Generic.APIResponse.ConstructHTTPResponse(data, retVal, message);
        }
        
        public async Task<HTTPResponse> AddProfile(string seekerGuid, SeekerProfileRequest profileRequest)
        {
            string message = string.Empty;
            Int32 retVal = -40;

            Object? data = default(Object);

            try
            {
                //check if the seeker exists or not
                Contracts.DBModels.JobSeeker.JobSeeker? jobSeeker = await _jobSeekerService.GetJobSeekerDetails(new Guid(seekerGuid), true);
                if (jobSeeker == null) throw new Exception("No Seeker found");

                if (jobSeeker.JobSeekerProfile != null) //job seeker already has a profile
                    throw new Exception("Profile already exists");

                JobSeekerProfile jobSeekerProfile = new JobSeekerProfile();
                _mapper.Map(profileRequest, jobSeekerProfile);

                if (profileRequest == null)
                    throw new Exception("Invalid data");

                jobSeekerProfile.JobSeekerId = jobSeeker.JobSeekerId;
                jobSeekerProfile.JobSeekerProfileId = Guid.NewGuid();
                jobSeekerProfile.WorkExperience = _mapper.Map<List<WorkExperience>>(profileRequest.WorkExperience);


                await _unitOfWork._JobSeekerProfileRepository.AddAsync(jobSeekerProfile);
                _unitOfWork.Commit();
                using (var memoryStream = new MemoryStream())
                {
                    await profileRequest.Resume.CopyToAsync(memoryStream);
                    var uploadedFile = new JobSeekerResume
                    {
                        FileName = profileRequest.Resume.FileName,
                        ContentType = profileRequest.Resume.ContentType,
                        Data = memoryStream.ToArray(),
                        JobSeekerProfileId = jobSeekerProfile.JobSeekerProfileId
                    };

                    await _unitOfWork._JobSeekerResumeRepository.AddAsync(uploadedFile);
                    _unitOfWork.Commit();
                }
                data = jobSeekerProfile.JobSeekerProfileId;
                retVal = 1;
            }
            catch (Exception ex)
            {
                return Library.Generic.APIResponse.ConstructExceptionResponse(-40, ex.Message);
            }

            return Library.Generic.APIResponse.ConstructHTTPResponse(data, retVal, message);
        }
        public async Task<HTTPResponse> GetProfile(string seekerGuid)
        {
            string message = string.Empty;
            Int32 retVal = -40;

            Object? data = default(Object);

            try
            {
                //check if the seeker exists or not
                Contracts.DBModels.JobSeeker.JobSeeker? jobSeeker = await _jobSeekerService.GetJobSeekerDetails(new Guid(seekerGuid), true);
                if (jobSeeker == null) throw new Exception("No Seeker found");

                if (jobSeeker.JobSeekerProfile == null) //job seeker already has a profile
                    throw new Exception("No Profile exists");
                JobSeekerProfileDTO jobSeekerProfile = new JobSeekerProfileDTO()
                {
                    JobSeekerProfileId = jobSeeker.JobSeekerProfile.JobSeekerProfileId,
                    KeySkills = jobSeeker.JobSeekerProfile.KeySkills,
                    Summary = jobSeeker.JobSeekerProfile.Summary,
                };
                var workexperiences = _mapper.Map<List<WorkExperienceDTO>>(jobSeeker.JobSeekerProfile.WorkExperience);
                jobSeekerProfile.WorkExperience = workexperiences;

                var resume = await _unitOfWork._JobSeekerResumeRepository.GetBySeekerProfileId(jobSeekerProfile.JobSeekerProfileId);
                jobSeekerProfile.Resume = new JobSeekerResumeDTO()
                {
                    ContentType = resume.ContentType,
                    Data = resume.Data,
                    FileName = resume.FileName,
                };

                data = jobSeekerProfile;
                retVal = 1;
            }
            catch (Exception ex)
            {
                return Library.Generic.APIResponse.ConstructExceptionResponse(-40, ex.Message);
            }

            return Library.Generic.APIResponse.ConstructHTTPResponse(data, retVal, message);
        }
        public async Task<JobSeekerResume> GetResume(string seekerGuid)
        {

            try
            {
                //check if the seeker exists or not
                Contracts.DBModels.JobSeeker.JobSeeker? jobSeeker = await _jobSeekerService.GetJobSeekerDetails(new Guid(seekerGuid), true);
                if (jobSeeker == null) throw new Exception("No Seeker found");

                if (jobSeeker.JobSeekerProfile == null) //job seeker already has a profile
                    throw new Exception("No Profile exists");

                var resume = await _unitOfWork._JobSeekerResumeRepository.GetBySeekerProfileId(jobSeeker.JobSeekerProfile.JobSeekerProfileId);
                return resume;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<HTTPResponse> UpdateProfile(string seekerGuid, SeekerProfileRequest profileRequest)
        {
            string message = string.Empty;
            Int32 retVal = -40;

            Object? data = default(Object);

            try
            {
                //check if the seeker exists or not
                Guid jobSeekerId = new Guid(seekerGuid);
                Contracts.DBModels.JobSeeker.JobSeeker? jobSeeker = await _jobSeekerService.GetJobSeekerDetails(jobSeekerId, true);
                if (jobSeeker == null) throw new Exception("No Seeker found");

                JobSeekerProfile jobSeekerProfile = jobSeeker.JobSeekerProfile;
                if(jobSeekerProfile == null)
                {
                    throw new Exception("No profile found");
                }

                //upload new resume
                if(profileRequest.Resume != null && profileRequest.Resume.Length != 0)
                {
                    //get prev resume
                    var resume = await _unitOfWork._JobSeekerResumeRepository.GetBySeekerProfileId(jobSeekerProfile.JobSeekerProfileId);
                    using (var memoryStream = new MemoryStream())
                    {
                        await profileRequest.Resume.CopyToAsync(memoryStream);
                        var uploadedFile = new JobSeekerResume
                        {
                            Id = resume.Id,
                            FileName = profileRequest.Resume.FileName,
                            ContentType = profileRequest.Resume.ContentType,
                            Data = memoryStream.ToArray(),
                            JobSeekerProfileId = jobSeekerProfile.JobSeekerProfileId
                        };

                        await _unitOfWork._JobSeekerResumeRepository.UpdateAsync(uploadedFile);
                    }
                }

                if (profileRequest.WorkExperience != null && profileRequest.WorkExperience.Any())
                    jobSeekerProfile.WorkExperience = _mapper.Map<List<WorkExperience>>(profileRequest.WorkExperience);

                if (profileRequest.KeySkills != null && profileRequest.KeySkills.Any())
                    jobSeekerProfile.KeySkills = profileRequest.KeySkills;

                if(!String.IsNullOrEmpty(profileRequest.Summary))
                    jobSeekerProfile.Summary = profileRequest.Summary;

                await _unitOfWork._JobSeekerProfileRepository.UpdateAsync(jobSeekerProfile);
                _unitOfWork.Commit();

                data = true;
                retVal = 1;
            }
            catch (Exception ex)
            {
                return Library.Generic.APIResponse.ConstructExceptionResponse(-40, ex.Message);
            }

            return Library.Generic.APIResponse.ConstructHTTPResponse(data, retVal, message);
        }
    }
}
