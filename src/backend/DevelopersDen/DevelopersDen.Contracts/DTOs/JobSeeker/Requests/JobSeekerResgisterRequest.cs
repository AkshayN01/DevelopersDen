﻿using System.ComponentModel.DataAnnotations;

namespace DevelopersDen.Contracts.DTOs.JobSeeker.Requests
{
    public class JobSeekerResgisterRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? GoogleId {  get; set; }
    }
}
