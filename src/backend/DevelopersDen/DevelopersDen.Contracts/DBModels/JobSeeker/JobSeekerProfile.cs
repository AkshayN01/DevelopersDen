using System.ComponentModel.DataAnnotations;

namespace DevelopersDen.Contracts.DBModels.JobSeeker
{
    public class JobSeekerProfile
    {
        [Key]
        public Guid JobSeekerProfileId { get; set; }
        [Required]
        public String Summary { get; set; }
        [Required]
        public byte[] Resume {  get; set; }
        public List<WorkExperience>? WorkExperience { get; set; } = new List<WorkExperience>();
        public List<string>? KeySkills { get; set; } = new List<string>();
        public SearchFilter? SearchFilter { get; set; } = new SearchFilter();
        public Guid JobSeekerId { get; set; }
        public JobSeeker JobSeeker { get; set; } = null!;
    }

    public class WorkExperience
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

    public class SearchFilter
    {
        public string? CompanyName { get; set; }
        public List<string>? KeySkills { get; set; } = null;
        public string? Location { get; set; }
        public int JobType { get; set; }
    }
}
