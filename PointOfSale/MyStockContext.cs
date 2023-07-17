using Microsoft.EntityFrameworkCore;

namespace PointOfSale;

public partial class MyStockContext : DbContext
{
    public MyStockContext()
    {
    }

    public DbSet<MyStock> MyStocks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Configure notification entities
        //https://learn.microsoft.com/en-us/ef/core/change-tracking/change-detection#configuring-notification-entities
        modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangingAndChangedNotifications);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MyStock;Trusted_Connection=True;");


}
