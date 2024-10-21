using DataLayer;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    IDataService _dataService;
    private readonly LinkGenerator _linkGenerator;

    public ProductsController(
        IDataService dataService,
        LinkGenerator linkGenerator)
    {
        _dataService = dataService;
        _linkGenerator = linkGenerator;
    }

    [HttpGet("{id}", Name = nameof(GetProduct))]
    public IActionResult GetProduct(int id)
    {
        var product = _dataService.GetProduct(id);

        if (product == null)
        {
            return NotFound();
        }
        var model = CreateProductModel(product);

        return Ok(model);
    }

    [HttpGet("category/{id}", Name = nameof(GetProductByCategory))]
    public IActionResult GetProductByCategory(int id)
    {
        var products = _dataService.GetProductByCategory(id);

        if (products == null || !products.Any())
        {
            return NotFound(new List<Product>());
        }
        var model = products.Select(CreateProductModel);

        return Ok(model);
    }

    [HttpGet(Name = nameof(GetProductByName))]
    public IActionResult GetProductByName(){
        var name = HttpContext.Request.Query["name"];
        var products = _dataService.GetProductByName(name);

        if (products == null || !products.Any())
        {
            return NotFound(new List<ProductNameModel>());
        }
        
        var model = products.Select(CreateProductNameModel);

        return Ok(model);
    }

    private ProductModel? CreateProductModel(Product? product)
    {
        if(product == null) { return null; }

        var model = product.Adapt<ProductModel>();
        model.Url = GetUrl(product.Id);

        return model;
    }
    private ProductNameModel? CreateProductNameModel(DTOName? product)
    {
        if(product == null) { return null; }

        var model = product.Adapt<ProductNameModel>();

        return model;
    }

    private string? GetUrl(int id)
    {
        return _linkGenerator.GetUriByName(HttpContext, nameof(GetProduct), new { id });
    }
}
