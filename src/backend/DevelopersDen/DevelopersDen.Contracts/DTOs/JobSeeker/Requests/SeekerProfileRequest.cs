using System.ComponentModel.DataAnnotations;

namespace DevelopersDen.Contracts.DTOs.JobSeeker.Requests
{
    public class SeekerProfileRequest
    {
        [Required]
        public byte[] Resume { get; set; }
        public List<WorkExperienceDTO> WorkExperience { get; set; } = new List<WorkExperienceDTO>();
        public List<string> KeySkills { get; set; } = new List<string>();
    }
    public class WorkExperienceDTO
    {
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string WorkDescription { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int IsCurrent { get; set; }
    }
}
