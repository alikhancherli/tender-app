using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Tender.App.Domain.Entities;

namespace Tender.App.Infra.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext() { }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .AddJsonFile("appsettings.Development.json")
               .Build();
            var connectionString = configuration.GetConnectionString("DBConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Bid> Bids => Set<Bid>();
    public DbSet<BidDetails> BidDetails => Set<BidDetails>();
}
