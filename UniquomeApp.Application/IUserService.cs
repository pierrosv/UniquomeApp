using UniquomeApp.SharedKernel;

namespace UniquomeApp.Application;

public interface IUserService
{
    Task<string> GetUserNameAsync(string userId);

    Task<(ActionResult Result, string UserId)> CreateUserAsync(string userName, string password);

    Task<ActionResult> DeleteUserAsync(string userId);
}