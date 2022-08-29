using Microsoft.EntityFrameworkCore;

namespace UniquomeApp.Infrastructure.Security;

public class UniquomeIdentityPostgresqlDbContext : UniquomeIdentityDbContext
{
    public UniquomeIdentityPostgresqlDbContext(DbContextOptions<UniquomeIdentityPostgresqlDbContext> options)
        : base(options)
    {
    }
}