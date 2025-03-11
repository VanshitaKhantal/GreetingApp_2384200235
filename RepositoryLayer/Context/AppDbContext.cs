using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;  // Ensure the correct namespace is used

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<GreetingEntity> Greetings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) // Ensure correct signature
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<GreetingEntity>()
            .HasOne(g => g.User)
            .WithMany(u => u.Greetings)
            .HasForeignKey(g => g.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
