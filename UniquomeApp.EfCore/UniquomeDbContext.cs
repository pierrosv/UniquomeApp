using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NodaTime;
using UniquomeApp.Domain.Base;
using UniquomeApp.EfCore.Configurations;

namespace UniquomeApp.EfCore;

public class UniquomeDbContext: DbContext
{
    private readonly Guid _currentContext = Guid.NewGuid();
    public UniquomeDbContext()
    {
        // Console.WriteLine($"Created Context {_currentContext}");
    }

    public UniquomeDbContext(DbContextOptions options) : base(options)
    {
        // Console.WriteLine($"Created Context {_currentContext}");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            throw new Exception("Why am I here !");
        }
        
        // Console.WriteLine($"Uniquome Context: {_currentContext}");
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.EnableDetailedErrors();
        // optionsBuilder.UseLoggerFactory(MyLoggerFactory);
        var connectionString = "";
        var timeoutLimit = 120;
        if (EfCache.ConnectionOptions != null && !string.IsNullOrEmpty(EfCache.ConnectionOptions.ConnectionString))
        {
            connectionString = EfCache.ConnectionOptions.ConnectionString;
            timeoutLimit = EfCache.ConnectionOptions.Timeout;
            // Console.WriteLine(EfCache.ConnectionOptions.ConnectionString);
        }
            

        //TODO: Change that
        #if DEBUG
         connectionString = "Server=192.168.254.128;port=5432;Database=uniquome;Username=bill;Password=Ikaros23@;Timeout=1024;Command Timeout=0"; // Local Timescale
        #endif
        optionsBuilder.UseNpgsql(connectionString, x =>
        {
            x.CommandTimeout((int)TimeSpan.FromMinutes(timeoutLimit).TotalSeconds);
            x.UseNodaTime();
        });
    }

    // public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
#if DEBUG
        Console.WriteLine($"Uniquome Context: {_currentContext} Added: {ChangeTracker.Entries().Count(x => x.State == EntityState.Added)} Unchanged: {ChangeTracker.Entries().Count(x => x.State == EntityState.Unchanged)} Modified: {ChangeTracker.Entries().Count(x => x.State == EntityState.Modified)} Detached: {ChangeTracker.Entries().Count(x => x.State == EntityState.Detached)}");
        // Console.WriteLine($"Uniquome Context: {_currentContext} Saving Entries: {ChangeTracker.Entries().Count()} Total Positions {Set<UniquomeEntity>().Count():N0} Total Ships {Set<SimpleEntity>().Count()}");
#endif

        //TODO: Decide whether to implement or not a logical/physical deletion structure
        //TODO: Check DateTime for timezone (maybe store everything on UTC)
        foreach (var entry in ChangeTracker.Entries<AuditableEntity<long>>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.RowVersion++;
                    entry.Entity.CreatedBy = "";
                    entry.Entity.CreatedAt = SystemClock.Instance.GetCurrentInstant();
                    break;
                case EntityState.Modified:
                    entry.Entity.RowVersion++;
                    entry.Entity.LastModifiedBy = "";
                    entry.Entity.LastModifiedAt = SystemClock.Instance.GetCurrentInstant();
                    break;
                // case EntityState.Deleted:
                //     entry.Entity.LastModifiedBy = "";//_currentUserService.UserId;
                //     entry.Entity.LastModified = _dateTime.Now;
                //     break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //TODO: Find how I can use something like below to avoid listing all configs
        // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
        foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            entityType.SetTableName(entityType.DisplayName());

        modelBuilder.ApplyConfiguration(new ApplicationUserEfConfig());
        modelBuilder.ApplyConfiguration(new NewsletterRegistrationEfConfig());
        modelBuilder.ApplyConfiguration(new OrganismEfConfig());
        modelBuilder.ApplyConfiguration(new UniquomeEfConfig());
        modelBuilder.ApplyConfiguration(new PeptideEfConfig());
        modelBuilder.ApplyConfiguration(new ProteinEfConfig());
        modelBuilder.ApplyConfiguration(new ProteomeEfConfig());
        modelBuilder.ApplyConfiguration(new UniquomeEfConfig());
        modelBuilder.ApplyConfiguration(new UniquomeProteinEfConfig());
    }
}