using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    DbSet<Product> products { get; set; }
    DbSet<Category> categories { get; set; }
}