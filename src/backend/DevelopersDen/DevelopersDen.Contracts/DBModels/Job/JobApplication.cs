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
        public Int64 JobApplicationId { get; set; }
        public string Comments { get; set; }
        public Int32 ApplicationStatusId { get; set; }
        public Int64 JobId { get; set; }
        public Int64 JobSeekerId { get; set; }

        public ApplicationStatus ApplicationStatus { get; set; }
        public Job Job { get; set; }
        public JobSeeker.JobSeeker JobSeeker { get; set; }    
    }
}
