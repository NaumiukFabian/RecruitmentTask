using System;
using System.Collections.Generic;

namespace RecruitmentTask.Database.Persistence.Models;

public partial class Price
{
    public string Id { get; set; } = null!;

    public string Sku { get; set; } = null!;

    public decimal? Nettproductpirce { get; set; }

    public virtual Product SkuNavigation { get; set; } = null!;
}
