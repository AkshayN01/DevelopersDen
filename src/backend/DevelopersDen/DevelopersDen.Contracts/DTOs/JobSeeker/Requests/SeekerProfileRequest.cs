using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DevelopersDen.Contracts.DTOs.JobSeeker.Requests
{
    public class SeekerProfileRequest
    {
        public IFormFile? Resume {  get; set; }
        public string? Summary { get; set; }
        public List<WorkExperienceRequestDTO>? WorkExperience { get; set; }
        public List<string>? KeySkills { get; set; }
    }
    public class WorkExperienceRequestDTO
    {
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string Designation { get; set; }
        [Required]
        public string WorkDescription { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string IsCurrent { get; set; }
    }
}
