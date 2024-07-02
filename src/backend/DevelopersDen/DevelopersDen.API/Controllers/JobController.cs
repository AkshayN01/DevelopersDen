using AutoMapper;
using DevelopersDen.Contracts.DTOs.JobSeeker.Requests;
using DevelopersDen.Interfaces.Repository;
using DevelopersDen.Library.Services.Seeker;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace DevelopersDen.API.Controllers
{
    [Authorize]
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

        [HttpPost]
        [Route("/api/jobseeker/get-jobs")]
        public async Task<IActionResult> GetAllJobs([FromBody]JobSearchFilterDTO searchFilterDTO, int pageNumber, int pageSize)
        {
            if(!ModelState.IsValid) { return BadRequest(ModelState); }

            var claims = User.Claims;
            var userguid = claims.FirstOrDefault(c => c.Type == "id")?.Value;
            if(userguid == null) {  return Unauthorized(); }
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
        [Route("/api/jobseeker/get-all-job-applications")]
        public async Task<IActionResult> GetAllJobApplication(int pageNumber, int pageSize)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var claims = User.Claims;
            var userguid = claims.FirstOrDefault(c => c.Type == "id")?.Value;
            if (userguid == null) { return Unauthorized(); }

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
        [Route("/api/jobseeker/get-job-application/{jobApplicationId}")]
        public async Task<IActionResult> GetJobApplicationDetails(string jobApplicationId)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var claims = User.Claims;
            var userguid = claims.FirstOrDefault(c => c.Type == "id")?.Value;
            if (userguid == null) { return Unauthorized(); }

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
        [Route("/api/jobseeker/add-job-application/{jobId}")]
        public async Task<IActionResult> AddApplicationDetails(string jobId, int status)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var claims = User.Claims;
            var userguid = claims.FirstOrDefault(c => c.Type == "id")?.Value;
            if (userguid == null) { return Unauthorized(); }

            try
            {
                var httpResponse = await _JobSeekerBlanket.AddJobApplication(userguid, jobId, status);
                return Ok(httpResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("/api/jobseeker/edit-job-application/{jobApplicationId}")]
        public async Task<IActionResult> EditApplicationDetails(string jobApplicationId, int status)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var claims = User.Claims;
            var userguid = claims.FirstOrDefault(c => c.Type == "id")?.Value;
            if (userguid == null) { return Unauthorized(); }

            try
            {
                var httpResponse = await _JobSeekerBlanket.UpdateJobApplication(userguid, jobApplicationId, status);
                return Ok(httpResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
