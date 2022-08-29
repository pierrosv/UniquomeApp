using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using UniquomeApp.Infrastructure.BaseSecurity;

namespace UniquomeApp.Infrastructure.Security
{
    public class IdentityService : IIdentityWithRoleService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly UniquomeIdentityDbContext _dbContext;

        public IdentityService(JwtSettings jwtSettings, UserManager<ApplicationUser> userManager, UniquomeIdentityDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _jwtSettings = jwtSettings;
        }

        public async Task<AuthenticationResult> RegisterAsync(string email, string password)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser != null)
            {
                return AuthenticationResult.AuthFailure(new[] { "Υπάρχει ήδη ένας χρήστης με την ίδια διεύθυνση ηλεκτρονικού ταχυδρομείου" });
            }

            var newUserId = Guid.NewGuid();
            var newUser = new ApplicationUser
            {
                Id = newUserId.ToString(),
                Email = email,
                UserName = email
            };

            var createdUser = await _userManager.CreateAsync(newUser, password);

            if (!createdUser.Succeeded)
            {
                return AuthenticationResult.AuthFailure(createdUser.Errors.Select(x => x.Description));
            }

            // await _userManager.AddClaimAsync(newUser, new Claim("tags.view", "true"));

            return GenerateAuthenticationResultForUser(newUser);
        }

        public Task<AuthenticationResult> AddUserOnRoleAsync(string email, string role)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticationResult> AddClaimOnUserAsync(string email, string claim)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthenticationResult> RegisterWithRoleAsync(RegisterRequest request, string ipAddress)
        {
            return await RegisterWithRoleAsync(request.Email, request.Email, request.Role);
        }

        public async Task<AuthenticationResult> LoginAsync(LoginRequest request, string ipAddress)
        {
            return await LoginAsync(request.Email, request.Password);
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return AuthenticationResult.AuthFailure(new[] { "Ο Χρήστης δεν υπάρχει" });
            }

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!userHasValidPassword)
            {
                return AuthenticationResult.AuthFailure(new[] { "Λάθος συνδυασμός Χρήστη/Κωδικού" });
            }
            return GenerateAuthenticationResultForUser(user);
        }

        public AuthenticationResult GenerateAuthenticationResultForUser(ApplicationUser user)
        {
            var email = user.Email;
            var roles = _userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var role = "guest";
            if (roles.Result.Any())
                role = roles.Result[0];
            //TODO: On claims add: Fullname, Role
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim("Role", role),
            };

            //TODO: Decide how expiration will be handled
            //TODO: Check why Token is not properly "expiring" server side
            // var greeceTimeDiff = 0; //Use UTC To properly adjust regardless of the server's timezone
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials,
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                Expires = DateTime.Now.AddMinutes(_jwtSettings.TokenLifetimeInMinutes),
                IssuedAt = DateTime.Now
            };

            var token = tokenHandler.CreateToken(tokenDescription);
            return AuthenticationResult.AuthSuccessWithToken(tokenHandler.WriteToken(token), "");
        }

        public Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
        {
            throw new NotImplementedException();
        }

        public void RevokeTokenAsync(string token, string ipAddress)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticationResult> VerifyEmailAsync(string token, string ipAddress)
        {
            throw new NotImplementedException();
        }

        public async Task<string> ForgotPassword2(ForgotPasswordRequest model, string origin)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            // always return ok response to prevent email enumeration
            if (user == null) throw  new Exception("Email not exists");
            user.ResetToken = randomTokenString();
            user.ResetTokenExpires = DateTime.Now.AddDays(2); //TODO: Make expiration Days a parameter
            _dbContext.Accounts.Update(user);
            await _dbContext.SaveChangesAsync();
            return user.ResetToken;
        }

        public async Task<bool> ChangeLostPassword(string token, string newPassword, string origin)
        {
            if (string.IsNullOrEmpty(token)) throw new Exception("Token is empty");
            var user = _dbContext.Accounts.FirstOrDefault(x => x.ResetToken == token);
            if (user == null) throw new Exception("Email not exists");
            var storedUser = await _userManager.FindByEmailAsync(user.Email);
            var newToken = await _userManager.GeneratePasswordResetTokenAsync(storedUser);
            var result = await _userManager.ResetPasswordAsync(storedUser, newToken, newPassword);
            user.ResetToken = randomTokenString();
            user.PasswordReset = DateTime.Now;
            _dbContext.Accounts.Update(user);
            await _dbContext.SaveChangesAsync();
            return result.Succeeded;
        }

        public async Task<bool> ChangePassword(string email, string currentPassword, string newPassword, string origin)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new Exception("Ο Χρήστης δεν υπάρχει");

            var passwordChanged = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            return passwordChanged.Succeeded;
        }

        public void ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            throw new NotImplementedException();
        }

        public void ValidateResetToken(ValidateResetTokenRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticationResult> ResetPassword(ResetPasswordRequest request, string ipAddress)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthenticationResult> RegisterWithRoleAsync(string email, string password, string role)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser != null)
            {
                return AuthenticationResult.AuthFailure(new[] { "Υπάρχει ήδη ένας χρήστης με την ίδια διεύθυνση ηλεκτρονικού ταχυδρομείου" });
            }

            var newUserId = Guid.NewGuid();
            var newUser = new ApplicationUser
            {
                Id = newUserId.ToString(),
                Email = email,
                UserName = email
            };

            var createdUser = await _userManager.CreateAsync(newUser, password);

            if (!createdUser.Succeeded)
            {
                return AuthenticationResult.AuthFailure(createdUser.Errors.Select(x => x.Description));
            }

            await _userManager.AddToRoleAsync(newUser, role);
            // await _userManager.AddClaimAsync(newUser, new Claim("tags.view", "true"));

            return GenerateAuthenticationResultForUser(newUser);
        }


        private string randomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }
        // public Task<string> GetUserNameAsync(string userId)
        // {
        //     return Task.FromResult("Vasilis Pierros");
        // }
        //
        // public Task<(ActionResult Result, string UserId)> CreateUserAsync(string userName, string password)
        // {
        //     throw new NotImplementedException();
        // }
        //
        // public Task<Result> DeleteUserAsync(string userId)
        // {
        //     throw new NotImplementedException();
        // }
    }
}