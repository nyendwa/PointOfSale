using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PointOfSale;

public partial class MyStockContext : DbContext
{
    public MyStockContext()
    {
    }

    public DbSet<MyStock> MyStocks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyStock;");


}
