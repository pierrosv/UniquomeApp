using System.ComponentModel.DataAnnotations;

namespace UniquomeApp.Infrastructure.BaseSecurity
{
    public class ValidateResetTokenRequest
    {
        [Required]
        public string Token { get; set; }
    }
}