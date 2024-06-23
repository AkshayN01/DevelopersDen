namespace DevelopersDen.Contracts.DTOs
{
    public class JobSeekerDTO
    {
        public Guid Guid { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public JobSeekerProfileDTO JobSeekerProfile { get; set; }
    }

    public class JobSeekerProfileDTO
    {

    }
}
