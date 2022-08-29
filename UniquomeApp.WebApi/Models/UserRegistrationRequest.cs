using System.ComponentModel.DataAnnotations;

namespace UniquomeApp.WebApi.Models;

public class UserRegistrationRequest
{
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Institution { get; set; }
    public string? Position { get; set; }
    public string? Country { get; set; }
        
}