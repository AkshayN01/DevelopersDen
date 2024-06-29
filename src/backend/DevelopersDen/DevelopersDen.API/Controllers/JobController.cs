using AutoMapper;
using DevelopersDen.Contracts.DTOs.JobSeeker.Requests;
using DevelopersDen.Interfaces.Repository;
using DevelopersDen.Library.Services.Seeker;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace DevelopersDen.API.Controllers
{
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly JobSeekerService _jobSeekerService;
        private readonly ILogger<JobSeekerController> _logger;
        private readonly Blanket.JobSeeker.JobBL _JobSeekerBlanket;
        public JobController(ILogger<JobSeekerController> logger, IUnitOfWork unitOfWork, JobSeekerService jobSeekerService, IMapper mapper)
        {
            _logger = logger;
            _jobSeekerService = jobSeekerService;
            _JobSeekerBlanket = new Blanket.JobSeeker.JobBL(unitOfWork, jobSeekerService, mapper);
        }

        [HttpGet]
        [Route("/api/jobseeker/{userguid}/get-jobs")]
        public async Task<IActionResult> GetAllJobs(string userguid, [FromBody]JobSearchFilterDTO searchFilterDTO, int pageNumber, int pageSize)
        {
            if(!ModelState.IsValid) { return BadRequest(ModelState); }


            try
            {
                var httpResponse = await _JobSeekerBlanket.GetJobs(userguid, searchFilterDTO, pageNumber, pageSize);
                return Ok(httpResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("/api/jobseeker/{userguid}/get-all-job-applications")]
        public async Task<IActionResult> GetAllJobApplication(string userguid, int pageNumber, int pageSize)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }


            try
            {
                var httpResponse = await _JobSeekerBlanket.GetAllJobApplicationDetails(userguid, pageNumber, pageSize);
                return Ok(httpResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("/api/jobseeker/{userguid}/get-job-application/{jobApplicationId}")]
        public async Task<IActionResult> GetJobApplicationDetails(string userguid, string jobApplicationId)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }


            try
            {
                var httpResponse = await _JobSeekerBlanket.GetJobApplicationDetails(userguid, jobApplicationId);
                return Ok(httpResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("/api/jobseeker/{userguid}/edit-job-application/{jobId}")]
        public async Task<IActionResult> EditApplicationDetails(string userguid, string jobId, int status)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }


            try
            {
                var httpResponse = await _JobSeekerBlanket.UpdateJobApplication(userguid, jobId, status);
                return Ok(httpResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
