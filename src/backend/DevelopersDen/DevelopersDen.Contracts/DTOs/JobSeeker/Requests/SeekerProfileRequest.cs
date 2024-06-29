using System.ComponentModel.DataAnnotations;

namespace DevelopersDen.Contracts.DTOs.JobSeeker.Requests
{
    public class SeekerProfileRequest
    {
        [Required]
        public byte[] Resume { get; set; }
        public string Summary { get; set; }
        public List<WorkExperienceRequestDTO> WorkExperience { get; set; } = new List<WorkExperienceRequestDTO>();
        public List<string> KeySkills { get; set; } = new List<string>();
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
        public int IsCurrent { get; set; }
    }
}
