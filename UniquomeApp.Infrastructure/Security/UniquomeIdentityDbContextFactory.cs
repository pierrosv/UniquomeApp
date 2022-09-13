using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace UniquomeApp.Infrastructure.Security;

public class UniquomeIdentityDbContextFactory : IDesignTimeDbContextFactory<UniquomeIdentityPostgresqlDbContext>
{
    public UniquomeIdentityPostgresqlDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<UniquomeIdentityPostgresqlDbContext>();
        optionsBuilder.UseNpgsql(@"Server=192.168.254.128;Database=uniquome;Username=bill;Password=Ikaros23@;Timeout=1024;Command Timeout=0");
        return new UniquomeIdentityPostgresqlDbContext(optionsBuilder.Options);
    }
}