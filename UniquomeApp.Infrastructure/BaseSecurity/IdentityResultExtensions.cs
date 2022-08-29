using Microsoft.AspNetCore.Identity;

namespace UniquomeApp.Infrastructure.BaseSecurity
{
    public static class IdentityResultExtensions
    {
        public static AuthenticationResult ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? AuthenticationResult.AuthSuccess()
                : AuthenticationResult.AuthFailure(result.Errors.Select(e => e.Description));
        }

        public static AuthenticationResult ToApplicationResultWithToken(this IdentityResult result, string token, string refreshToken)
        {
            return result.Succeeded
                ? AuthenticationResult.AuthSuccessWithToken(token, refreshToken)
                : AuthenticationResult.AuthFailure(result.Errors.Select(e => e.Description));
        }
    }
}