using AutoMapper;
using DevelopersDen.Contracts.DBModels;
using DevelopersDen.Contracts.DTOs.JobSeeker.Requests;
using DevelopersDen.Contracts.Enums;
using DevelopersDen.DataAccess.Data.Recruiter;
using DevelopersDen.Interfaces.Repository;
using DevelopersDen.Library.Services.Seeker;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevelopersDen.API.Controllers
{
    [ApiController]
    public class GenericController : ControllerBase
    {
        private readonly ILogger<JobSeekerController> _logger;
        private readonly Blanket.Generic.GenericBL _GenericBlanket;
        public GenericController(ILogger<JobSeekerController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _GenericBlanket = new Blanket.Generic.GenericBL(unitOfWork, mapper);
        }

        [HttpGet]
        [Route("/api/get-stakeholders")]
        public async Task<IActionResult> GetAllStakeholders()
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }


            try
            {
                var httpResponse = await _GenericBlanket.GetStakeholders();
                return Ok(httpResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("/api/get-application-statuses")]
        public async Task<IActionResult> GetAllApplicationStatuses()
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }


            try
            {
                var httpResponse = await _GenericBlanket.GetApplicationStatuses();
                return Ok(httpResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("/api/get-job-types")]
        public async Task<IActionResult> GetAllJobTypes()
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            try
            {
                var httpResponse = _GenericBlanket.GetJobTypes();
                return Ok(httpResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
