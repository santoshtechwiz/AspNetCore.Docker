using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<DiscountType> DiscountTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Optionally configure the DiscountType entity here
        modelBuilder.Entity<DiscountType>().ToTable("DiscountTypes");
    }
}



