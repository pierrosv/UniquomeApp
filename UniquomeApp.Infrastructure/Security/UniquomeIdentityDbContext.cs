using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UniquomeApp.Infrastructure.Security
{
    public class UniquomeIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<ApplicationUser> Accounts { get; set; }

        public UniquomeIdentityDbContext()
        {
        }

        public UniquomeIdentityDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
#if DEBUG
            builder.EnableSensitiveDataLogging();
#endif
            builder.EnableDetailedErrors();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
