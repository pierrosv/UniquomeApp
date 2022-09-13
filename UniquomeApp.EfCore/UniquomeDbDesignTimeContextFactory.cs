using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace UniquomeApp.EfCore;

public class UniquomeDbDesignTimeContextFactory : IDesignTimeDbContextFactory<UniquomeDbContext>
{
    public UniquomeDbContext CreateDbContext(string[] args)
    {
        var connectionString = "Server=192.168.254.128;port=5432;Database=uniquome;Username=bill;Password=Ikaros23@;Timeout=1024;Command Timeout=0";
        var optionsBuilder = new DbContextOptionsBuilder<UniquomeDbContext>();
        optionsBuilder.UseNpgsql(connectionString);
        return new UniquomeDbContext(optionsBuilder.Options);
    }
}