
namespace RecruitmentTask.Database.Persistence.Models;

public partial class Inventory
{
    public int Id { get; set; }

    public string? Sku { get; set; }

    public decimal? Quantity { get; set; }

    public string? Unit { get; set; }

    public decimal? Shippincost { get; set; }

    public string? Shipping { get; set; }
}
