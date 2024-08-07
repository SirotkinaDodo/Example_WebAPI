using Example.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Example.DataAccess;

public sealed class ExampleContext : DbContext
{
    public DbSet<Breed> Breeds { get; set; }
    
    public DbSet<Dog> Dogs { get; set; }
    
    public DbSet<Owner> Owners { get; set; }
    
    public DbSet<OwnerDog> OwnersDogs { get; set; }
    
    public ExampleContext() { }
    
    public ExampleContext(DbContextOptions<ExampleContext> options)
        : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // https://learn.microsoft.com/en-us/ef/core/modeling/relationships/many-to-many
        modelBuilder.Entity<Dog>().HasIndex(d => d.Name);
        modelBuilder.Entity<Dog>()
            .HasMany(d => d.Owners)
            .WithMany(o => o.Dogs)
            .UsingEntity(
                "OwnerDog",
                l => l.HasOne(typeof(Dog)).WithMany().HasForeignKey("DogId").HasPrincipalKey(nameof(Dog.Id)),
                r => r.HasOne(typeof(Owner)).WithMany().HasForeignKey("OwnerId").HasPrincipalKey(nameof(Owner.Id)),
                j => j.HasKey("DogId", "OwnerId"));
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ExampleDogsDb;Trusted_Connection=True;");
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        optionsBuilder.EnableSensitiveDataLogging(true);
    }
}