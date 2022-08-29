namespace UniquomeApp.Infrastructure.BaseSecurity;

public interface IIdentityService
{
    Task<AuthenticationResult> AddUserOnRoleAsync(string email, string role);
    Task<AuthenticationResult> AddClaimOnUserAsync(string email, string claim);
    Task<AuthenticationResult> RegisterWithRoleAsync(RegisterRequest request, string ipAddress);
    Task<AuthenticationResult> LoginAsync(LoginRequest request, string ipAddress);
    Task<AuthenticationResult> RefreshTokenAsync(string token, string ipAddress);
    void RevokeTokenAsync(string token, string ipAddress);
    Task<AuthenticationResult> VerifyEmailAsync(string token, string ipAddress);
    void ForgotPassword(ForgotPasswordRequest model, string origin);
    void ValidateResetToken(ValidateResetTokenRequest request);
    Task<AuthenticationResult> ResetPassword(ResetPasswordRequest request, string ipAddress);
}