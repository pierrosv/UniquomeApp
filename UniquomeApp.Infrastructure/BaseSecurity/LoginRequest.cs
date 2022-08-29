﻿using System.ComponentModel.DataAnnotations;

namespace UniquomeApp.Infrastructure.BaseSecurity
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}