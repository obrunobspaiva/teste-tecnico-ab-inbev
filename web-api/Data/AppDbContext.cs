using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<ChatMessage> ChatMessages { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}