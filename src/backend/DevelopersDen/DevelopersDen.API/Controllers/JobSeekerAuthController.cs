using AutoMapper;
using DevelopersDen.Contracts.Application;
using DevelopersDen.Contracts.DTOs.JobSeeker.Requests;
using DevelopersDen.Interfaces.Repository;
using DevelopersDen.Library.Services.Seeker;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevelopersDen.API.Controllers
{
    [ApiController]
    public class JobSeekerAuthController : ControllerBase
    {
        private readonly JobSeekerService _jobSeekerService;
        private readonly ILogger<JobSeekerController> _logger;
        private readonly Blanket.JobSeeker.SeekerProfileBL _JobSeekerBlanket;

        public JobSeekerAuthController(ILogger<JobSeekerController> logger, IUnitOfWork unitOfWork, JobSeekerService jobSeekerService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _jobSeekerService = jobSeekerService;
            _JobSeekerBlanket = new Blanket.JobSeeker.SeekerProfileBL(unitOfWork, jobSeekerService, mapper, appSettings);
        }

        [HttpPost]
        [Route("/api/jobseeker/login")]
        public async Task<IActionResult> JobSeekerLogin([FromBody] SeeekrLoginRequest loginRequest)
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
        [Route("/api/jobseeker/google-login")]
        public async Task<IActionResult> JobSeekerGoogleLogin([FromBody] string credentials)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); };

            try
            {
                var httpResponse = await _JobSeekerBlanket.LoginWithGoogle(credentials);
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
    }
}
