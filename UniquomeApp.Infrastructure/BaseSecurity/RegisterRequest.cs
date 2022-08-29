using System.ComponentModel.DataAnnotations;

namespace UniquomeApp.Infrastructure.BaseSecurity;

public class RegisterRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }

    [Required]
    public string Role { get; set; }
}