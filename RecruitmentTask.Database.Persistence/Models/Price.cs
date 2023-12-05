

namespace RecruitmentTask.Database.Persistence.Models;

public partial class Price
{
    public string Id { get; set; } = null!;

    public string? Sku { get; set; }

    public decimal? Nettproductpice { get; set; }
}
