using AutoMapper;
using DevelopersDen.Contracts.DBModels.JobSeeker;
using DevelopersDen.Contracts.DTOs;
using DevelopersDen.Contracts.DTOs.JobSeeker.Requests;
using DevelopersDen.Contracts.Enums;
using DevelopersDen.Interfaces.Repository;
using DevelopersDen.Library.Generic;
using DevelopersDen.Library.Services.Seeker;

namespace DevelopersDen.Blanket.JobSeeker
{
    public class SeekerProfileBL
    {
        private readonly IMapper _mapper;
        private readonly JobSeekerService _jobSeekerService;
        private readonly IUnitOfWork _unitOfWork;
        public SeekerProfileBL(IUnitOfWork unitOfWork, JobSeekerService jobSeekerService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _jobSeekerService = jobSeekerService;
            _mapper = mapper;
        }
        public async Task<HTTPResponse> Login(LoginRequest loginRequest)
        {
            string message = string.Empty;
            Int32 retVal = -40;

            Object? data = default(Object);

            try
            {
                if (String.IsNullOrEmpty(loginRequest.GoogleId) && String.IsNullOrEmpty(loginRequest.Password))
                    throw new Exception("Password or GoogleId is missing");

                Contracts.DBModels.JobSeeker.JobSeeker jobSeeker = await _unitOfWork._JobSeekerRepository.Login(loginRequest.Email, loginRequest.GoogleId);

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

                data = jobSeeker;
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

                jobSeeker.JobSeekerId = new Guid();
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

                if (profileRequest != null)
                    throw new Exception("Invalid data");

                jobSeekerProfile.JobSeekerId = jobSeeker.JobSeekerId;
                jobSeekerProfile.JobSeekerProfileId = new Guid();

                await _unitOfWork._JobSeekerProfileRepository.AddAsync(jobSeekerProfile);
                _unitOfWork.Commit();
                retVal = 1;
            }
            catch (Exception ex)
            {
                return Library.Generic.APIResponse.ConstructExceptionResponse(-40, ex.Message);
            }

            return Library.Generic.APIResponse.ConstructHTTPResponse(data, retVal, message);
        }
        
        public async Task<HTTPResponse> UpdateProfile(string seekerGuid, SeekerProfileRequest profileRequest)
        {
            string message = string.Empty;
            Int32 retVal = -40;

            Object? data = default(Object);

            try
            {
                //check if the seeker exists or not
                Contracts.DBModels.JobSeeker.JobSeeker? jobSeeker = await _jobSeekerService.GetJobSeekerDetails(new Guid(seekerGuid));
                if (jobSeeker == null) throw new Exception("No Seeker found");

                JobSeekerProfile jobSeekerProfile = new JobSeekerProfile();
                _mapper.Map(profileRequest, jobSeekerProfile);

                if (profileRequest != null)
                    throw new Exception("Invalid data");

                await _unitOfWork._JobSeekerProfileRepository.UpdateAsync(jobSeekerProfile);
                _unitOfWork.Commit();
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
