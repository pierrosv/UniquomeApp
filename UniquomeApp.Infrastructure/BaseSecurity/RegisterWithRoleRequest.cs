using System.ComponentModel.DataAnnotations;

namespace UniquomeApp.Infrastructure.BaseSecurity;

public class RegisterWithRoleRequest : RegisterRequest
{
    [Required]
    public string Role { get; set; }
}