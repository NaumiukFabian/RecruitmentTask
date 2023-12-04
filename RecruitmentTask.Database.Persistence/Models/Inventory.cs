using System;
using System.Collections.Generic;

namespace RecruitmentTask.Database.Persistence.Models;

public partial class Inventory
{
    public int Id { get; set; }

    public string Sku { get; set; } = null!;

    public decimal? Quantity { get; set; }

    public string? Unit { get; set; }

    public decimal? Shippincost { get; set; }

    public virtual Product? Product { get; set; }
}
