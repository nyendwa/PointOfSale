using System;
using System.Collections.Generic;

namespace PointOfSale;

public partial class MyStock
{
    public decimal ProductId { get; set; }

    public string? ProductName { get; set; }

    public int Qty { get; set; }

    public decimal Price { get; set; }

    public string? Description { get; set; }
}
