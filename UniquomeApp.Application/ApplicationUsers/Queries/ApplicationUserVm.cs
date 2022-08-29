using UniquomeApp.Application.Mappings;
using UniquomeApp.Domain;

namespace UniquomeApp.Application.ApplicationUsers.Queries;

public class ApplicationUserVm : RootVm, IMapFrom<ApplicationUser>, IMapTo<ApplicationUser>
{
    public string Email { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? Institution { get; set; }
    public string? Position { get; set; }
    public string? Country { get; set; }
}