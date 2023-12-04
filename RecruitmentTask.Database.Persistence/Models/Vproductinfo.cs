using System;
using System.Collections.Generic;

namespace RecruitmentTask.Database.Persistence.Models;

public partial class Vproductinfo
{
    public string? Sku { get; set; }

    public string? Name { get; set; }

    public string? Ean { get; set; }

    public string? Productername { get; set; }

    public string? Defaultimage { get; set; }

    public decimal? Quantity { get; set; }

    public string? Unit { get; set; }

    public string? Shipping { get; set; }

    public decimal? Nettproductpice { get; set; }
}
