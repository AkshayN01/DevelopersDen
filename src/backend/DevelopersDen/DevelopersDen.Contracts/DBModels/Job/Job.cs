using DevelopersDen.Contracts.DBModels.Recruiter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersDen.Contracts.DBModels.Job
{
    public class Job : AuditableEntity
    {
        [Key]
        public Int64 JobId { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public string JobDescription { get; set; }
        public Int32 MinExperience { get; set; }
        [Required]
        public List<string> KeySkills { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string City { get; set; }
        public DateTime PostedDate { get; set; }
        public Int64 RecruiterAccountId { get; set; }
        public RecruiterAccount RecruiterAccount { get; set; }
    }
}
