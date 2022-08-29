using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace UniquomeApp.EfCore;

public class AisDbDesignTimeContextFactory : IDesignTimeDbContextFactory<AisDbContext>
{
    public AisDbContext CreateDbContext(string[] args)
    {
        var connectionString = "Server=192.168.1.11;port=5432;Database=poseidon_dev5;Username=paralos;Password=Ikaros23@;Timeout=1024;Command Timeout=0";
        var optionsBuilder = new DbContextOptionsBuilder<AisDbContext>();
        optionsBuilder.UseNpgsql(connectionString);
        return new AisDbContext(optionsBuilder.Options);
    }
}