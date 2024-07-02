namespace DevelopersDen.Contracts.DTOs.JobSeeker.Requests
{
    public class JobSearchFilterDTO
    {
        public string? CompanyName { get; set; }
        public List<string>? KeySkills { get; set; }
        public string? Location { get; set; }
        public int JobType { get; set; }
    }
}
