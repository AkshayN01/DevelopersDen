using System.ComponentModel.DataAnnotations;

namespace DevelopersDen.Contracts.DBModels.Job
{
    public class ApplicationStatus
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;
    }
}
