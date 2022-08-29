using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace UniquomeApp.Infrastructure.Security
{
    public class UoACceIdentityDbContextFactory : IDesignTimeDbContextFactory<UniquomeIdentityPostgresqlDbContext>
    {
        public UniquomeIdentityPostgresqlDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UniquomeIdentityPostgresqlDbContext>();
            // optionsBuilder.UseNpgsql(@"Server=88.197.52.85;port=30000;Database=cce_auth;Username=cce;Password=Cc3@dm1n;Timeout=1024;Command Timeout=0");
            optionsBuilder.UseNpgsql(@"Server=192.168.1.39;Database=uniquome;Username=cce;Password=Ikaros23@;Timeout=1024;Command Timeout=0");
            return new UniquomeIdentityPostgresqlDbContext(optionsBuilder.Options);
        }
    }
}