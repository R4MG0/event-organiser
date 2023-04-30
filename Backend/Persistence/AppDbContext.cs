using Backend.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasMany(x => x.CreatedEvents)
            .WithOne(x => x.CreatedBy);

        modelBuilder.Entity<User>()
            .HasMany(x => x.AuthorizedForEvents)
            .WithMany(x => x.IsAuthorizedFor);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Event> Events { get; set; }
}