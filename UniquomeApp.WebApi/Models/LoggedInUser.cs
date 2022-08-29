using UniquomeApp.Application.ApplicationUsers.Queries;
using UniquomeApp.Infrastructure.BaseSecurity;

namespace UniquomeApp.WebApi.Models;

public class LoggedInUser
{
    public AuthenticationResult AuthResult { get; set; }
    public ApplicationUserVm UserInfo { get; set; }
}