﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersDen.Contracts.DTOs.JobSeeker.Requests
{
    public class SeeekrLoginRequest
    {
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        public string? GoogleId { get; set; }
        public string? Name { get; set; }
    }
}
