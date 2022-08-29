using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace UniquomeApp.Infrastructure.Security
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            // var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();// UserManager<ApplicationUser> userManager, RoleManager< IdentityRole > roleManager
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();// UserManager<ApplicationUser> userManager, RoleManager< IdentityRole > roleManager
            await roleManager.CreateAsync(new IdentityRole(AuthorizationConstants.Roles.Administrators));
            await roleManager.CreateAsync(new IdentityRole(AuthorizationConstants.Roles.Secretariat));
            await roleManager.CreateAsync(new IdentityRole(AuthorizationConstants.Roles.ScientificDirectors));
            await roleManager.CreateAsync(new IdentityRole(AuthorizationConstants.Roles.AcademicDirectors));
            await roleManager.CreateAsync(new IdentityRole(AuthorizationConstants.Roles.Cooperators));
            await roleManager.CreateAsync(new IdentityRole(AuthorizationConstants.Roles.CommitteeUser));
            await roleManager.CreateAsync(new IdentityRole(AuthorizationConstants.Roles.Guest));

            // var adminUserName = "pierrosv@uoa.gr";
            // var adminUser = new IdentityUser { UserName = adminUserName, Email = adminUserName };
            // await userManager.CreateAsync(adminUser, AuthorizationConstants.DEFAULT_PASSWORD);
            // adminUser = await userManager.FindByNameAsync(adminUserName);
            // await userManager.AddToRoleAsync(adminUser, AuthorizationConstants.Roles.Administrators);
            //
            // adminUserName = "admin@kedivim.gr";
            // adminUser = new IdentityUser { UserName = adminUserName, Email = adminUserName };
            // await userManager.CreateAsync(adminUser, AuthorizationConstants.DEFAULT_PASSWORD);
            // adminUser = await userManager.FindByNameAsync(adminUserName);
            // await userManager.AddToRoleAsync(adminUser, AuthorizationConstants.Roles.Administrators);
            //
            // var secretariatEmail = "secretariat@kedivim.gr";
            // var secretariatUser = new IdentityUser { UserName = secretariatEmail, Email = secretariatEmail };
            // await userManager.CreateAsync(secretariatUser, AuthorizationConstants.DEFAULT_PASSWORD);
            // secretariatUser = await userManager.FindByNameAsync(secretariatEmail);
            // await userManager.AddToRoleAsync(secretariatUser, AuthorizationConstants.Roles.Secretariat);
            //
            // var eyUserName = "ey@kedivim.gr";
            // var eyUser = new IdentityUser { UserName = eyUserName, Email = eyUserName };
            // await userManager.CreateAsync(eyUser, AuthorizationConstants.DEFAULT_PASSWORD);
            // eyUser = await userManager.FindByNameAsync(eyUserName);
            // await userManager.AddToRoleAsync(eyUser, AuthorizationConstants.Roles.AcademicDirectors);
            //
            // var cooperatorUserName = "cooperator@kedivim.gr";
            // var cooperatorUser = new IdentityUser { UserName = cooperatorUserName, Email = cooperatorUserName };
            // await userManager.CreateAsync(cooperatorUser, AuthorizationConstants.DEFAULT_PASSWORD);
            // cooperatorUser = await userManager.FindByNameAsync(cooperatorUserName);
            // await userManager.AddToRoleAsync(cooperatorUser, AuthorizationConstants.Roles.Cooperators);
            //
            // var committeeUserName = "committee@kedivim.gr";
            // var committeeUser = new IdentityUser { UserName = committeeUserName, Email = committeeUserName };
            // await userManager.CreateAsync(committeeUser, AuthorizationConstants.DEFAULT_PASSWORD);
            // committeeUser = await userManager.FindByNameAsync(committeeUserName);
            // await userManager.AddToRoleAsync(committeeUser, AuthorizationConstants.Roles.Cooperators);
        }
    }
}
