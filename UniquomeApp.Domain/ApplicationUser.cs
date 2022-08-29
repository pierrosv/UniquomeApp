using UniquomeApp.SharedKernel.DomainCore;

namespace UniquomeApp.Domain;

public class ApplicationUser : SimpleEntity
{
    public string Email { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? Institution { get; set; }
    public string? Position { get; set; }
    public string? Country { get; set; }
}