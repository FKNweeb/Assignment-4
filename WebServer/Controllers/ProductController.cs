using DataLayer;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebServer.DTO;

namespace WebServer.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{

    IDataService _dataService;
    private readonly LinkGenerator _linkGenerator;

    public ProductController(IDataService dataService, LinkGenerator linkGenerator)
    {
        _dataService = dataService;
        _linkGenerator = linkGenerator;
    }

    [HttpGet("{id:int}", Name = nameof(GetProduct))]
    public IActionResult GetProduct(int id)
    {
        var product = _dataService.GetProduct(id);
        if (product == null) { return NotFound(); }
        var productDto = CreateProductDto(product);
        return Ok(productDto);
    }



    [HttpGet("category/{id}", Name = nameof(GetProducts))]
    public IActionResult GetProducts(int id)
    {
        var products = _dataService.GetProductByCategory(id);
        if (!products.Any()) { return NotFound(new List<Product>()); }


        IList<ProductDto> productDtos = new List<ProductDto>();
        foreach (var product in products)
        {
            productDtos.Add(CreateProductDto(product));
        }
        

        return Ok(productDtos);

    }

    [HttpGet(Name = nameof(GetProductsByName))]
    public IActionResult GetProductsByName()
    {
        var name = HttpContext.Request.Query["name"];
        var products = _dataService.GetProductByName(name);
        if (!products.Any()) { return NotFound(new List<ProductModelName>()); }
        IList<ProductModelName> productNames = new List<ProductModelName>();
        foreach (var product in products)
        {
            productNames.Add(CreateProductModelName(product));
        }
        return Ok(productNames);

    }



    public ProductModelName? CreateProductModelName(DTONames names)
    {
        if (names == null) return null;
        var Dto = names.Adapt<ProductModelName>();
        return Dto;
    }



    private ProductDto? CreateProductDto(Product? product)
    {
        if (product == null) return null;
        var Dto = product.Adapt<ProductDto>();
        Dto.Url = GetUrl(product.Id);

        return Dto;

    }


    private string? GetUrl(int id)
    {
        return _linkGenerator.GetUriByName(HttpContext, nameof(GetProduct), new { id });
    }
}
