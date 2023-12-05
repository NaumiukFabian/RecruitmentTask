

namespace RecruitmentTask.Database.Persistence.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Sku { get; set; }

    public string? Ean { get; set; }

    public string? Productername { get; set; }

    public string? Category { get; set; }

    public string? Shipping { get; set; }

    public string? Defaultimage { get; set; }
}
