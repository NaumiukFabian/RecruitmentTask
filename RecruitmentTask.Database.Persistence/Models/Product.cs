using System;
using System.Collections.Generic;

namespace RecruitmentTask.Database.Persistence.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Sku { get; set; } = null!;

    public string? Ean { get; set; }

    public string? Producername { get; set; }

    public string? Category { get; set; }

    public string? Shipping { get; set; }

    public string? Defaultimage { get; set; }

    public virtual ICollection<Price> Prices { get; set; } = new List<Price>();

    public virtual Inventory SkuNavigation { get; set; } = null!;
}
