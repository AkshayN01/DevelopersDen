using AutoMapper;
using DevelopersDen.Contracts.Application;
using DevelopersDen.Contracts.DBModels.JobSeeker;
using DevelopersDen.Contracts.DTOs.JobSeeker.Requests;
using DevelopersDen.Interfaces.Repository;
using DevelopersDen.Library.Services.Seeker;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevelopersDen.API.Controllers
{
    [Authorize]
    [ApiController]
    public class JobSeekerController : ControllerBase
    {
        private readonly JobSeekerService _jobSeekerService;
        private readonly ILogger<JobSeekerController> _logger;
        private readonly Blanket.JobSeeker.SeekerProfileBL _JobSeekerBlanket;

        public JobSeekerController(ILogger<JobSeekerController> logger, IUnitOfWork unitOfWork, JobSeekerService jobSeekerService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _jobSeekerService = jobSeekerService;
            _JobSeekerBlanket = new Blanket.JobSeeker.SeekerProfileBL(unitOfWork, jobSeekerService, mapper, appSettings);
        }

        [HttpPost]
        [Route("/api/jobseeker/add-profile")]
        public async Task<IActionResult> JobSeekerAddProfile([FromForm] SeekerProfileRequest profileRequest)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); };
            var claims = User.Claims;
            var userguid = claims.FirstOrDefault(c => c.Type == "id")?.Value;
            if (userguid == null) { return Unauthorized(); }

            if (profileRequest.Resume == null || profileRequest.Resume.Length == 0) {  return BadRequest(); }

            try
            {
                var httpResponse = await _JobSeekerBlanket.AddProfile(userguid, profileRequest);
                return Ok(httpResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("/api/jobseeker/update-profile")]
        public async Task<IActionResult> JobSeekerUpdateProfile([FromForm] SeekerProfileRequest profileRequest)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); };
            var claims = User.Claims;
            var userguid = claims.FirstOrDefault(c => c.Type == "id")?.Value;
            if (userguid == null) { return Unauthorized(); }

            try
            {
                var httpResponse = await _JobSeekerBlanket.UpdateProfile(userguid, profileRequest);
                return Ok(httpResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("/api/jobseeker/get-profile")]
        public async Task<IActionResult> JobSeekerGetProfile()
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); };
            var claims = User.Claims;
            var userguid = claims.FirstOrDefault(c => c.Type == "id")?.Value;
            if (userguid == null) { return Unauthorized(); }

            try
            {
                var httpResponse = await _JobSeekerBlanket.GetProfile(userguid);
                return Ok(httpResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("/api/jobseeker/get-resume")]
        public async Task<IActionResult> JobSeekerGetResume()
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); };
            var claims = User.Claims;
            var userguid = claims.FirstOrDefault(c => c.Type == "id")?.Value;
            if (userguid == null) { return Unauthorized(); }

            try
            {
                JobSeekerResume resume = await _JobSeekerBlanket.GetResume(userguid);
                if (resume == null)
                    return NotFound();
                return File(resume.Data, "application/octet-stream", resume.FileName);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
