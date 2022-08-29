using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace UniquomeApp.Infrastructure.Security
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            await roleManager.CreateAsync(new IdentityRole(AuthorizationConstants.Roles.Administrators));
            await roleManager.CreateAsync(new IdentityRole(AuthorizationConstants.Roles.ApplicationUser));
            await roleManager.CreateAsync(new IdentityRole(AuthorizationConstants.Roles.Guest));
        }
    }
}
