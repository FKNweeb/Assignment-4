using DataLayer;

namespace WebServer.DTO;

public class ProductDto
{
    public string? Url { get; set; }
    public string? Name { get; set; }

    public string? QuantityPerUnit { get; set; } = null;
    public int UnitPrice { get; set; }

    public int UnitsInStock { get; set; }

    public int CategoryId { get; set; }

    public Category? Category { get; set; } = null;

    public string CategoryName
    {
        get => Category?.Name ?? "";
    }
}
