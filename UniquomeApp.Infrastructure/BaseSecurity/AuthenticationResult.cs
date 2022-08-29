using UniquomeApp.SharedKernel;

namespace UniquomeApp.Infrastructure.BaseSecurity
{
    public class AuthenticationResult : ActionResult
    {
        //TODO: Explore the possibility of moving this to SharedKernel
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public static AuthenticationResult AuthSuccess()
        {
            return new AuthenticationResult { Succeeded = true, Token = "", RefreshToken = "", ExitCode = 0, Errors = new string[] { } };
        }
        public static AuthenticationResult AuthSuccessWithToken(string token, string refreshToken)
        {
            return new AuthenticationResult { Succeeded = true, Token = token, RefreshToken = refreshToken, ExitCode = 0, Errors = new string[] { } };
        }

        public static AuthenticationResult AuthFailure(IEnumerable<string> errors)
        {
            return new AuthenticationResult {Succeeded = false, Token = "", RefreshToken = "", ExitCode = -1, Errors = errors.ToArray()};
        }
    }
}
