using System.Security.Claims;
using UniquomeApp.Application;

namespace UniquomeApp.WebApi.Services;

public class CurrentUserService : ICurrentUserService
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
    }
    // public string UserId => HttpContext.GetUserId();
    public string UserId { get; }
}