using Microsoft.EntityFrameworkCore;
using DAPM.ResourceRegistryMS.Api.Models;

public class ResourceRegistryDbContext : DbContext
{
    ILogger<ResourceRegistryDbContext> _logger;
    public ResourceRegistryDbContext(DbContextOptions<ResourceRegistryDbContext> options, ILogger<ResourceRegistryDbContext> logger)
        : base(options)
    {
        _logger = logger;
        InitializeDatabase();
    }

    public DbSet<Resource> Resources { get; set; }
    public DbSet<Pipeline> Pipelines { get; set; }
    public DbSet<Repository> Repositories { get; set; }
    public DbSet<Peer> Peers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<ResourceType> ResourceTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Repository>().HasKey(r => new { r.PeerId, r.Id });
        builder.Entity<Resource>().HasKey(r => new { r.PeerId, r.RepositoryId, r.Id });
        builder.Entity<Pipeline>().HasKey(r => new { r.PeerId, r.RepositoryId, r.Id });
        // builder.Entity<User>().HasKey(r => new { r.Id, r.FirstName, r.LastName, r.Mail, r.Organization, r.HashPassword });

        var user = new User()
        {
            Id = Guid.NewGuid(),
            Mail = "admin@email.ch",
            FirstName = "admin",
            LastName = "admin",
            Organization = Guid.NewGuid(),
            HashPassword = "$2a$12$Jligef.ByeRACdblRiMuDejgNYXlUBZWfCSD3wTZ029g5MF/x8cDa",
            UserRole = (int)UserRole.Admin,
            accepted = 1,
        };
        builder.Entity<User>().HasData(user);


    }

    public void InitializeDatabase()
    {
        if (Database.GetPendingMigrations().Any())
        {
            Database.EnsureDeleted();
            Database.Migrate();


            SaveChanges();
        }
    }
}
