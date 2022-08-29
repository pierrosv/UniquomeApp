using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace UniquomeApp.Infrastructure.Security
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        public string ResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public DateTime? PasswordReset { get; set; }
    }
}