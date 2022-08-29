using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using UniquomeApp.Application.Behaviours;

namespace UniquomeApp.Infrastructure.Security
{
    public class UniquomeAuthorizationService : IUniquomeAuthorizationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;

        public UniquomeAuthorizationService(UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService)
        {

            _userManager = userManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);
            
            var result = await _authorizationService.AuthorizeAsync(principal, policyName);
            
            return result.Succeeded;
            // return true;
        }
    }

}
