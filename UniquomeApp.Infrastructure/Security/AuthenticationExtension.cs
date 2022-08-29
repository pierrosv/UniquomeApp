using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using UniquomeApp.Application.Behaviours;
using UniquomeApp.Infrastructure.BaseSecurity;
using UniquomeApp.SharedKernel;

namespace UniquomeApp.Infrastructure.Security;

public static class AuthenticationExtension
{
    public static IServiceCollection AddTokenAuthentication(this IServiceCollection services, IConfiguration config)
    {
        var dbConnections = new List<DbConnectionOptions>();
        config.GetSection("DbConnections").Bind(dbConnections);
        if (!dbConnections.Any()) throw new Exception("Unable to get DB Connections");

        var dbOptions = dbConnections.FirstOrDefault(x => x.Name == "Main");
        if (dbOptions == null) throw new Exception("Unable to get Main DB Connection");

        services.AddDbContext<UniquomeIdentityDbContext, UniquomeIdentityPostgresqlDbContext>(options => options.UseNpgsql(dbOptions.ConnectionString));
        services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>().AddEntityFrameworkStores<UniquomeIdentityDbContext>();
        // services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<UniquomeIdentityDbContext>().AddDefaultTokenProviders();
        // services.AddScoped<IUserService, IdentityService>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IIdentityWithRoleService, IdentityService>();
        services.AddScoped<IUniquomeAuthorizationService, UniquomeAuthorizationService>();

        var jwtSettings = new JwtSettings();
        config.GetSection(nameof(JwtSettings)).Bind(jwtSettings);
        services.AddSingleton(jwtSettings);

        var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

            
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });


        // services.AddAuthentication(x =>
        // {
        //     x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //     x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        //     x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        // });
        // IdentityModelEventSource.ShowPII = true;
        // var tokenValidationParameters = new TokenValidationParameters{
        //     ValidateIssuer = true,
        //     ValidateAudience = true,
        //     ValidIssuer = jwtSettings.Issuer,
        //     ValidAudience = jwtSettings.Audience,
        //     ValidateIssuerSigningKey = true,
        //     ValidateLifetime = true,
        //     IssuerSigningKey = new SymmetricSecurityKey(key)
        // };
        // services.AddSingleton(tokenValidationParameters);
        //
        // services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //     .AddJwtBearer(options =>
        //     {
        //         options.SaveToken = true; //By Nick
        //         options.TokenValidationParameters = tokenValidationParameters;
        //     });

        return services;
    }
}