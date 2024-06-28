using AutoMapper;
using DevelopersDen.Contracts.DTOs.JobSeeker.Requests;
using DevelopersDen.Interfaces.Repository;
using DevelopersDen.Library.Services.Seeker;
using Microsoft.AspNetCore.Mvc;

namespace DevelopersDen.API.Controllers
{
    [ApiController]
    public class JobSeekerController : ControllerBase
    {
        private readonly JobSeekerService _jobSeekerService;
        private readonly ILogger<JobSeekerController> _logger;
        private readonly Blanket.JobSeeker.SeekerProfileBL _JobSeekerBlanket;

        public JobSeekerController(ILogger<JobSeekerController> logger, IUnitOfWork unitOfWork, JobSeekerService jobSeekerService, IMapper mapper)
        {
            _logger = logger;
            _jobSeekerService = jobSeekerService;
            _JobSeekerBlanket = new Blanket.JobSeeker.SeekerProfileBL(unitOfWork, jobSeekerService, mapper);
        }

        [HttpPost]
        [Route("/api/jobseeker/login")]
        public async Task<IActionResult> JobSeekerLogin([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); };

            try
            {
                var httpResponse = await _JobSeekerBlanket.Login(loginRequest);
                return Ok(httpResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("/api/jobseeker/register")]
        public async Task<IActionResult> JobSeekerRegister([FromBody] JobSeekerResgisterRequest resgisterRequest)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); };

            try
            {
                var httpResponse = await _JobSeekerBlanket.Register(resgisterRequest);
                return Ok(httpResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("/api/jobseeker/{userguid}/add-profile")]
        public async Task<IActionResult> JobSeekerAddProfile(string userguid, [FromBody] SeekerProfileRequest profileRequest)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); };

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
        [Route("/api/jobseeker/{userguid}/update-profile")]
        public async Task<IActionResult> JobSeekerUpdateProfile(string userguid, [FromBody] SeekerProfileRequest profileRequest)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); };

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
    }
}
