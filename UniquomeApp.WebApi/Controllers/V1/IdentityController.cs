using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniquomeApp.Application;
using UniquomeApp.Application.ApplicationUsers.Commands;
using UniquomeApp.Application.ApplicationUsers.Queries;
using UniquomeApp.Infrastructure.BaseSecurity;
using UniquomeApp.Infrastructure.Security;
using UniquomeApp.SharedKernel;
using UniquomeApp.Utilities;
using UniquomeApp.WebApi.Models;
using UniquomeApp.WebApi.Options;

namespace UniquomeApp.WebApi.Controllers.V1
{
    public class IdentityController : BaseApiController
    {
        private readonly IIdentityWithRoleService _identityService;
        private readonly JwtSettings _jwtSettings;
        private readonly EmailSettings _emailSettings;
        private readonly List<EmailContext> _emailContexts;
        private readonly ApiOptions _apiOptions;
        private readonly ICurrentUserService _userService;

        public IdentityController(IIdentityWithRoleService identityService, JwtSettings jwtSettings, EmailSettings emailSettings, List<EmailContext> emailContexts, ApiOptions apiOptions, ICurrentUserService userService)
        {
            _identityService = identityService;
            _jwtSettings = jwtSettings;
            _emailSettings = emailSettings;
            _emailContexts = emailContexts;
            _apiOptions = apiOptions;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutesV1.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var authResponse = await _identityService.LoginAsync(request, GetIpAddress());

            if (!authResponse.Succeeded)
            {
                return BadRequest(AuthenticationResult.AuthFailure(authResponse.Errors));
            }

            SetTokenCookie(authResponse.RefreshToken);
            //TODO: Re-evaluate the placement of this code here.
            var user = await Mediator.Send(new GetApplicationUserByEmailDetailQuery(request.Email));
            var newUser = new LoggedInUser { UserInfo = user, AuthResult = AuthenticationResult.AuthSuccessWithToken(authResponse.Token, authResponse.RefreshToken) };
            return Ok(newUser);
        }

        [HttpPost(ApiRoutesV1.Identity.RefreshToken)]
        public async Task<IActionResult> Refresh()
        {
            var refreshToken = Request.Cookies["CceRefreshToken"];
            var authResponse = await _identityService.RefreshTokenAsync(refreshToken, GetIpAddress());

            if (!authResponse.Succeeded)
                return Unauthorized(AuthenticationResult.AuthFailure(authResponse.Errors));

            SetTokenCookie(authResponse.RefreshToken);
            return Ok(AuthenticationResult.AuthSuccessWithToken(authResponse.Token, authResponse.RefreshToken));
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutesV1.Identity.ForgotPassword)]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            // var command = new SendScientificDirectorInvitationCommand() { Email = email };
            var newForgotModel = new ForgotPasswordRequest { Email = email };
            var token = await _identityService.ForgotPassword2(newForgotModel, GetIpAddress());
            try
            {
                if (_emailSettings.SendEmail && _emailContexts.Any())
                {
                    var subject = "";
                    var body = "";
                    var context = _emailContexts.FirstOrDefault(x => x.Name == "Forgot Password");
                    if (context == null) return BadRequest("Δεν βρέθηκαν τα στοιχεία για την αποστολή Email");
                    subject = context.Subject;
                    body = context.Body
                        .Replace("@@ApiUrl", _apiOptions.ExternalUrl)
                        .Replace("@@token", token)
                        .Replace("@@CceSignature", CommunicationTexts.Signature)
                        .Replace("@@PrivacyDisclaimer", _apiOptions.PrivacyDisclaimer);

                    var listOfEmails = new List<string> {email};
                    if (!string.IsNullOrEmpty(body))
                    {
                        if (!_emailSettings.UseRelay)
                            EmailUtilities.SendEmail(_emailSettings.SimpleSmtpServer, _emailSettings.SimpleUseSsl, _emailSettings.SimpleSmtpPort, _emailSettings.SimpleSender, listOfEmails, subject, body, _emailSettings.SimpleSender, _emailSettings.Password, null);
                        else
                        {
                            EmailUtilities.SendRelayMail(_emailSettings.RelaySender, email, _emailSettings.RelaySmtpServer, subject, body);
                        }
                    }
                }
                return Ok(new { message = "Παρακαλώ δείτε το e-mail σας για οδηγίες επαναφοράς του κωδικού σας!" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = $"Συνέβη κάποιο λάθος κατά την αποστολή στοιχείων επαναφοράς του κωδικού σας! {e.Message}" });
            }
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutesV1.Identity.ChangeLostPassword)]
        public async Task<IActionResult> ChangeLostPassword([FromBody] ChangeLostPasswordRequest request)
        {
            var authResponse = await _identityService.ChangeLostPassword(request.Token, request.NewPassword, GetIpAddress());

            if (!authResponse)
            {
                return BadRequest(new { message = $"Συνέβη κάποιο λάθος κατά την επαναφορά του κωδικού σας. Παρακαλώ επικοινωνήστε με τον διαχιεριστή!" });
            }
            return Ok(new { message = "Επιτυχής αλλαγή κωδικού" });
        }

        [HttpPost(ApiRoutesV1.Identity.ChangePassword)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var authResponse = await _identityService.ChangePassword(_userService.UserId, request.CurrentPassword, request.NewPassword, GetIpAddress());

            if (!authResponse)
            {
                return BadRequest(new { message = $"Συνέβη κάποιο λάθος κατά την αλλαγή του κωδικού σας. Παρακαλώ επικοινωνήστε με τον διαχιεριστή!" });
            }
            return Ok(new { message = "Επιτυχής αλλαγή κωδικού" });
        }

        [HttpPost(ApiRoutesV1.Identity.RegisterAdmin)]
        public async Task<IActionResult> RegisterAdmin([FromBody] UserRegistrationRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(
                    AuthenticationResult.AuthFailure(
                        ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))));

            var alteredRequest = GetRegisterRequest(request.Email, request.Password, request.ConfirmPassword, AuthorizationConstants.Roles.Administrators);
            var result = await PerformRegistration(alteredRequest);
            return !result.Succeeded ? (IActionResult)BadRequest(result) : Ok(result);
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutesV1.Identity.RegisterAppUser)]
        public async Task<IActionResult> RegisterAppUser([FromBody] UserRegistrationRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(AuthenticationResult.AuthFailure(ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))));

            var result = await _identityService.RegisterWithRoleAsync(request.Email, request.Password, AuthorizationConstants.Roles.ApplicationUser);
            if (!result.Succeeded) return BadRequest("Unable to create account. Please contact system administrator by pressing <a mail=\"mailto:pierrosv@gmail.gr\">HERE</a> ");
            var createUserCommand = new CreateApplicationUserCommand
            {
                Email = request.Email,
                Country = request.Country,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Institution = request.Institution,
                Position = request.Position
            };
            await Mediator.Send(createUserCommand);
            try
            {
                //TODO: Send Confirmation Email
                // if (_emailSettings.SendEmail && _emailContexts.Any())
                // {
                //     var subject = "";
                //     var body = "";
                //     var context = _emailContexts.FirstOrDefault(x => x.Name == "Scientific Director Registration");
                //     if (context == null) return BadRequest("Δεν βρέθηκαν τα στοιχεία για την αποστολή Email");
                //     subject = context.Subject;
                //     body = context.Body
                //         .Replace("@@ApiUrl", _apiOptions.ExternalUrl)
                //         .Replace("@@CceSignature", CommunicationTexts.Signature)
                //         .Replace("@@PrivacyDisclaimer", _apiOptions.PrivacyDisclaimer);
                //
                //     if (!string.IsNullOrEmpty(body))
                //     {
                //         if (!_emailSettings.UseRelay)
                //             EmailUtilities.SendEmail(_emailSettings.SimpleSmtpServer, _emailSettings.SimpleUseSsl,
                //                 _emailSettings.SimpleSmtpPort, _emailSettings.SimpleSender,
                //                 new List<string> {command.Email}, subject, body, _emailSettings.SimpleSender,
                //                 _emailSettings.Password, null);
                //         else
                //             EmailUtilities.SendRelayMail(_emailSettings.RelaySender, command.Email,
                //                 _emailSettings.RelaySmtpServer, subject, body, null);
                //     }
                // }
            }
            catch (Exception e)
            {
                return BadRequest(new { message = $"Συνέβη κάποιο λάθος κατά την εγγραφή σας. Παρακαλώ δοκιμάστε σε λίγο ! {e.Message}" });
            }
            return Ok(result.Token);
        }

        private RegisterRequest GetRegisterRequest(string email, string password, string confirmPassword, string role)
        {
            var newRequest = new RegisterRequest
            { Role = role, Email = email, Password = password, ConfirmPassword = confirmPassword };

            return newRequest;
        }

        private string GetIpAddress()
        {
            //TODO: Review this method
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return "localhost";//Microsoft.AspNetCore.Http.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenLifetimeInMinutes)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private async Task<AuthenticationResult> PerformRegistration(RegisterRequest alteredRequest)
        {
            var authResponse = await _identityService.RegisterWithRoleAsync(alteredRequest, GetIpAddress());

            if (!authResponse.Succeeded)
                return AuthenticationResult.AuthFailure(authResponse.Errors);

            SetTokenCookie(authResponse.RefreshToken);
            return AuthenticationResult.AuthSuccessWithToken(authResponse.Token, authResponse.RefreshToken);
        }
    }
}