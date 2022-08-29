using Microsoft.EntityFrameworkCore;
using UniquomeApp.Application;
using UniquomeApp.Domain.Base;
using UniquomeApp.SharedKernel.DomainCore;

namespace UniquomeApp.EfCore.Repos;

public class AisRepoFactory : IAisRepoFactory
{
    public IAisExtendedRepository<TEntity> GetRepository<TEntity>() where TEntity : DomainRootEntity<long>
    {
        var optionsBuilder = new DbContextOptionsBuilder();
        optionsBuilder.UseNpgsql(EfCache.ConnectionOptions.ConnectionString, x =>
        {
            x.CommandTimeout((int)TimeSpan.FromMinutes(120).TotalSeconds);
            x.UseNodaTime();
        });

        var db = new AisDbContext(optionsBuilder.Options);
        return new AisExtendedExtendedEfRepo<TEntity>(db);
    }

    // public IVesselPositionRepo GetVesselPositionRepo()
    // {
    //     var optionsBuilder = new DbContextOptionsBuilder();
    //     optionsBuilder.UseNpgsql(EfCache.ConnectionOptions.ConnectionString, x =>
    //     {
    //         x.CommandTimeout((int)TimeSpan.FromMinutes(120).TotalSeconds);
    //         x.UseNetTopologySuite();
    //     });
    //
    //     var db = new AisDbContext(optionsBuilder.Options);
    //     return new EfVesselPositionRepo(db);
    // }
}