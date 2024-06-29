using DevelopersDen.Contracts.DTOs.JobSeeker.Requests;
using DevelopersDen.Contracts.DTOs;
using DevelopersDen.Library.Services.Seeker;
using AutoMapper;
using DevelopersDen.Interfaces.Repository;
using System.Linq.Expressions;
using DevelopersDen.Contracts.DBModels.Job;
using DevelopersDen.Contracts.Enums;
using DevelopersDen.Contracts.DBModels.Recruiter;
using DevelopersDen.Contracts.DTOs.JobSeeker.Responses;
using DevelopersDen.Contracts.DTOs.Recruiter.Responses;

namespace DevelopersDen.Blanket.JobSeeker
{
    public class JobBL
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JobSeekerService _jobSeekerService;
        public JobBL(IUnitOfWork unitOfWork, JobSeekerService jobSeekerService, IMapper mapper)
        {
            _jobSeekerService = jobSeekerService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        //This method can be used to suggest jobs and also search based on the filters applied
        public async Task<HTTPResponse> GetJobs(string seekerId, JobSearchFilterDTO jobSearchFilter, int pageNumber, int pageSize)
        {
            string message = string.Empty;
            Int32 retVal = -40;

            Object? data = default(Object);

            try
            {
                JobListDTO response = new JobListDTO();
                //check if the seeker exists or not
                Contracts.DBModels.JobSeeker.JobSeeker? jobSeeker = await _jobSeekerService.GetJobSeekerDetails(new Guid(seekerId), true);
                if (jobSeeker == null) throw new Exception("No Seeker found");

                //update search filter in the seeker profile
                if(jobSeeker.JobSeekerProfile != null && jobSearchFilter != null) 
                {
                    _mapper.Map(jobSearchFilter, jobSeeker.JobSeekerProfile.SearchFilter);
                    await _unitOfWork._JobSeekerProfileRepository.UpdateAsync(jobSeeker.JobSeekerProfile);
                    _unitOfWork.Commit();
                }

                List<Expression<Func<Job, bool>>> expressions =
                    new List<Expression<Func<Job, bool>>>() { x => x.IsActive == 1 };

                //filter jobs
                if (jobSearchFilter != null)
                {

                    if (String.IsNullOrEmpty(jobSearchFilter.CompanyName))
                    {
                        List<Guid> companyIds = await _unitOfWork._RecruiterRepository.GetRecruiterIdByName(jobSearchFilter.CompanyName);
                        if (companyIds.Any())
                        {
                            Expression<Func<Job, bool>> expression = x => companyIds.Contains(x.RecruiterId);
                            expressions.Add(expression);
                        }
                    }

                    if(String.IsNullOrEmpty(jobSearchFilter.Location))
                    {
                        Expression<Func<Job, bool>> expression = x => x.Location.Contains(jobSearchFilter.Location);
                        expressions.Add(expression);
                    }


                    if (jobSearchFilter.JobType != 0)
                    {
                        Expression<Func<Job, bool>> expression = x => x.JobType == jobSearchFilter.JobType;
                        expressions.Add(expression);
                    }

                    if (jobSearchFilter.KeySkills.Any())
                    {
                        Expression<Func<Job, bool>> expression = x => x.KeySkills.Any(y => jobSearchFilter.KeySkills.Contains(y));
                        expressions.Add(expression);
                    }
                }
                else
                {
                    if (jobSeeker.JobSeekerProfile != null && jobSeeker.JobSeekerProfile.KeySkills.Any())
                    {
                        Expression<Func<Job, bool>> expression = x => x.KeySkills.Any(y => jobSeeker.JobSeekerProfile.KeySkills.Contains(y));
                        expressions.Add(expression);
                    }
                }

                //exclude applied jobs
                IEnumerable<JobApplication> applications = _unitOfWork._JobApplicationRepository.GetAllByJobSeekerId(new Guid(seekerId));
                if(applications.Any())
                {
                    List<Guid> jobIds = applications
                        .Where(x => x.ApplicationStatusId == (int)ApplicationStatusEnum.Canceled || x.ApplicationStatusId == (int)ApplicationStatusEnum.Declined)
                        .Select(x => x.JobId).ToList();
                    if (jobIds.Any())
                    {
                        Expression<Func<Job, bool>> expression = x => !jobIds.Contains(x.JobId);
                        expressions.Add(expression);
                    }
                }

                // Combine the conditions
                var combinedCondition = Library.Generic.Utility.CombineConditions<Job>(expressions);

                List<Job> jobs = await _unitOfWork._JobRepository.FilterJobs(combinedCondition);
                if(jobs.Any())
                {
                    response.TotalCount = jobs.Count;
                    jobs = jobs.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                    List<JobDTO> jobDTOs = new List<JobDTO>();
                    foreach(Job job in jobs)
                    {
                        JobDTO jobDTO = new JobDTO();
                        _mapper.Map(job, jobDTO);

                        RecruiterDTO recruiterDTO = new RecruiterDTO();

                        Recruiter recruiterDetails = await _unitOfWork._RecruiterRepository.GetByGuidAsync(job.RecruiterId);
                        _mapper.Map(recruiterDetails, recruiterDTO);

                        jobDTO.Recruiter = recruiterDTO;
                        jobDTOs.Add(jobDTO);
                    }

                    response.Jobs = jobDTOs;
                    data = response;
                }

            }
            catch (Exception ex)
            {
                return Library.Generic.APIResponse.ConstructExceptionResponse(-40, ex.Message);
            }

            return Library.Generic.APIResponse.ConstructHTTPResponse(data, retVal, message);
        }

        public async Task<HTTPResponse> UpdateJobApplication(string seekerId, string jobId, int status)
        {
            string message = string.Empty;
            Int32 retVal = -40;

            Object? data = default(Object);

            try
            {
                Guid SeekerGuid = new Guid(seekerId);
                Guid JobGuid = new Guid(jobId);

                //check if seeker exists or not
                Contracts.DBModels.JobSeeker.JobSeeker? jobSeeker = await _jobSeekerService.GetJobSeekerDetails(SeekerGuid, true);
                if (jobSeeker == null) throw new Exception("No Seeker found");

                //check if job exists or not
                Job job = await _unitOfWork._JobRepository.GetByGuidAsync(JobGuid);
                if (job == null) throw new Exception("No job found");

                //check if already exists
                JobApplication? application = await _unitOfWork._JobApplicationRepository.GetByJobAndSeekerId(SeekerGuid, JobGuid);

                if (application != null)
                {
                    if (application.ApplicationStatusId == (int)ApplicationStatusEnum.Declined)
                        throw new Exception("Application was already declined");

                    if (status == (int)ApplicationStatusEnum.Canceled)
                    {
                        application.ApplicationStatusId = (int)ApplicationStatusEnum.Canceled;
                        await _unitOfWork._JobApplicationRepository.UpdateAsync(application);
                        _unitOfWork.Commit();
                    }
                }
                else if (status == (int)ApplicationStatusEnum.Applied){
                    JobApplication jobApplication = new JobApplication()
                    {
                        ApplicationStatusId = status,
                        Comments = string.Empty,
                        JobId = new Guid(jobId),
                        JobSeekerId = new Guid(seekerId),
                        JobApplicationId = new Guid()
                    };
                    await _unitOfWork._JobApplicationRepository.AddAsync(jobApplication);
                    _unitOfWork.Commit();
                }

                data = true;
            }
            catch (Exception ex)
            {
                return Library.Generic.APIResponse.ConstructExceptionResponse(-40, ex.Message);
            }

            return Library.Generic.APIResponse.ConstructHTTPResponse(data, retVal, message);
        }

        public async Task<HTTPResponse> GetJobApplicationDetails(string seekerId, string jobApplicationId)
        {
            string message = string.Empty;
            Int32 retVal = -40;

            Object? data = default(Object);

            try
            {
                Guid SeekerGuid = new Guid(seekerId);
                Guid JobApplicationGuid = new Guid(jobApplicationId);

                //check if seeker exists or not
                Contracts.DBModels.JobSeeker.JobSeeker? jobSeeker = await _jobSeekerService.GetJobSeekerDetails(SeekerGuid, true);
                if (jobSeeker == null) throw new Exception("No Seeker found");

                //check if job application exists or not
                JobApplication jobApplication = await _unitOfWork._JobApplicationRepository.GetByGuidAsync(JobApplicationGuid);

                if (jobApplication == null) throw new Exception("No application found");

                Job jobDetails = await _unitOfWork._JobRepository.GetByGuidAsync(jobApplication.JobId);

                Recruiter recruiterDetails = await _unitOfWork._RecruiterRepository.GetByGuidAsync(jobDetails.RecruiterId);

                JobApplicationDTO jobApplicationDTO = new JobApplicationDTO();

                _mapper.Map(jobApplication, jobApplicationDTO);
                _mapper.Map(jobDetails, jobApplicationDTO.Job);
                _mapper.Map(recruiterDetails, jobApplicationDTO.Job.Recruiter);
                
                data = jobApplicationDTO;
            }
            catch (Exception ex)
            {
                return Library.Generic.APIResponse.ConstructExceptionResponse(-40, ex.Message);
            }

            return Library.Generic.APIResponse.ConstructHTTPResponse(data, retVal, message);
        }

        public async Task<HTTPResponse> GetAllJobApplicationDetails(string seekerId, int pageNumber, int pageSize)
        {
            string message = string.Empty;
            Int32 retVal = -40;

            Object? data = default(Object);

            try
            {
                Guid SeekerGuid = new Guid(seekerId);

                //check if seeker exists or not
                Contracts.DBModels.JobSeeker.JobSeeker? jobSeeker = await _jobSeekerService.GetJobSeekerDetails(SeekerGuid, true);
                if (jobSeeker == null) throw new Exception("No Seeker found");

                List<JobApplication> jobApplications = _unitOfWork._JobApplicationRepository.GetAllByJobSeekerId(SeekerGuid).ToList();

                if (jobApplications.Any())
                {
                    JobApplicationListDTO response = new JobApplicationListDTO() { TotalCount = jobApplications.Count };
                    jobApplications = jobApplications.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                    List<JobApplicationDTO> jobApplicationsDTO = new List<JobApplicationDTO>();
                    _mapper.Map(jobApplications, jobApplicationsDTO);
                    response.Applications = jobApplicationsDTO;
                    data = response;
                }

            }
            catch (Exception ex)
            {
                return Library.Generic.APIResponse.ConstructExceptionResponse(-40, ex.Message);
            }

            return Library.Generic.APIResponse.ConstructHTTPResponse(data, retVal, message);
        }

        public async Task<HTTPResponse> SaveJob(string seekerId, string jobId)
        {
            string message = string.Empty;
            Int32 retVal = -40;

            Object? data = default(Object);

            try
            {
                //check if seeker exists or not

                //check if job exists or not
            }
            catch (Exception ex)
            {
                return Library.Generic.APIResponse.ConstructExceptionResponse(-40, ex.Message);
            }

            return Library.Generic.APIResponse.ConstructHTTPResponse(data, retVal, message);
        }

        public async Task<HTTPResponse> GetAllSavedJob(string seekerId)
        {
            string message = string.Empty;
            Int32 retVal = -40;

            Object? data = default(Object);

            try
            {
                //check if seeker exists or not
            }
            catch (Exception ex)
            {
                return Library.Generic.APIResponse.ConstructExceptionResponse(-40, ex.Message);
            }

            return Library.Generic.APIResponse.ConstructHTTPResponse(data, retVal, message);
        }
    }
}
