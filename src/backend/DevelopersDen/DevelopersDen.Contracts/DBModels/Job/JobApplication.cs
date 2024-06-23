using DevelopersDen.Contracts.DBModels.JobSeeker;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersDen.Contracts.DBModels.Job
{
    public class JobApplication
    {
        [Key]
        public Guid JobApplicationId { get; set; }
        public string Comments { get; set; }
        public Int32 ApplicationStatusId { get; set; }
        public Guid JobId { get; set; }
        public Guid JobSeekerId { get; set; }
        public Job Job { get; set; } = null!;
        public JobSeeker.JobSeeker JobSeeker { get; set; } = null!; 
    }
}
