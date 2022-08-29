using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using UniquomeApp.Domain.Base;
using UniquomeApp.EfCore.Repos;
using UniquomeApp.SharedKernel;

namespace UniquomeApp.EfCore;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConnections = new List<DbConnectionOptions>();
        configuration.GetSection("DbConnections").Bind(dbConnections);
        if (!dbConnections.Any()) throw new Exception("Unable to get DB Connections");

        var dbOptions = dbConnections.FirstOrDefault(x => x.Name == "Main");
        if (dbOptions == null) throw new Exception("Unable to get Main DB Connection");

        Console.WriteLine("Building DB Options");
        EfCache.ConnectionOptions = dbOptions;
        switch (dbOptions.DbProvider)
        {
            case "Postgresql":
                services.AddDbContext<UniquomeDbContext, UniquomeDbContext>(options =>
                {
                    options.UseNpgsql(dbOptions.ConnectionString, x =>
                    {
                        x.CommandTimeout((int)TimeSpan.FromMinutes(dbOptions.Timeout).TotalSeconds);
                        x.UseNodaTime();
                    });
                    Console.WriteLine("Applied " + dbOptions.ConnectionString);
                });
                NpgsqlConnection.GlobalTypeMapper.UseNodaTime();
                break;
        }

        //TODO: Reverting IRepositoryBase from Transient to Scoped was done in order to support complex commands like imports where multiple Repos were needed. The introduction of RepoFactory may have solved that and Repo can return to be a transient entity
        services.AddScoped(typeof(IRepositoryBase<>), typeof(UniquomeEfBaseRepo<>));
        services.AddScoped(typeof(IUniquomeExtendedRepository<>), typeof(UniquomeExtendedExtendedEfRepo<>));
        // services.AddSingleton(typeof(IUniquomeRepoFactory), typeof(UniquomeRepoFactory));
        return services;
    }
}