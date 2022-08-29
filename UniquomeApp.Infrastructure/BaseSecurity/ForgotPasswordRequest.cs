using System.ComponentModel.DataAnnotations;

namespace UniquomeApp.Infrastructure.BaseSecurity;

public class ForgotPasswordRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}