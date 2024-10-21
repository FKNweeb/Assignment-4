using System;

namespace WebApi.Models;

public class ProductModel
{
    public string Name { get; set; }
    public string CategoryName { get; set; }
    public string ProductName { get; set; }
    public string? Url { get; set; }
}
