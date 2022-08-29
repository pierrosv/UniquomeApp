using UniquomeApp.Infrastructure.BaseSecurity;

namespace UniquomeApp.Infrastructure.Security;

public interface IIdentityWithRoleService : IIdentityService
{
    Task<AuthenticationResult> RegisterWithRoleAsync(string email, string password, string role);

    Task<string> ForgotPassword2(ForgotPasswordRequest model, string origin);
    Task<bool> ChangeLostPassword(string token, string newPassword, string origin);
    Task<bool> ChangePassword(string email, string currentPassword, string newPassword, string origin);
}