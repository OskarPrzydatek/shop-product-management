using Microsoft.EntityFrameworkCore;

namespace SPM.Infrastructure.Context;

public class MainContext : DbContext
{
    public DbSet<Product>? Product { get; set; }

    public MainContext()
    {
    }

    public MainContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("DataSource=dbo.SPM.db");
    }
}