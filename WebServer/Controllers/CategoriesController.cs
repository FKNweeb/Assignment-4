using DataLayer;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using WebServer.DTO;

namespace WebServer.Controllers;


[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    IDataService _dataService;
    private readonly LinkGenerator _linkGenerator;

    public CategoriesController(IDataService dataService, LinkGenerator linkGenerator)
    {
        _dataService = dataService;
        _linkGenerator = linkGenerator;
    }

    [HttpGet]
    public IActionResult GetCategories()
    {
        var categories = _dataService.GetCategories();
        return Ok(categories);
    }


    [HttpGet("{id}", Name = nameof(GetCategory))]
    public IActionResult GetCategory(int id)
    {
        var category = _dataService.GetCategory(id);
        if (category == null) { return NotFound(); }
        var categoryDto = CreateCategoryDto(category);
        return Ok(categoryDto);
    }

    [HttpPost]
    public IActionResult CreateCategory([FromBody] CategoryModel categoryDto)
    {
        var category = _dataService.CreateCategory(categoryDto.Name, categoryDto.Description);
        var Dto = CreateCategoryDto(category);

        return Created(Dto.Url, Dto);

    }

    [HttpPut("{id}")]
    public IActionResult UpdateCategory(int id, [FromBody] CategoryModel categoryDto)
    {
        var category = _dataService.GetCategory(id);
        if (category == null) { return NotFound(); }

        category.Name = categoryDto.Name;
        category.Description = categoryDto.Description;

        _dataService.UpdateCategory(category.Id, category.Name, category.Description);
        return Ok(category);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(int id)
    {
        var category = _dataService.GetCategory(id);
        if (category == null) { return NotFound();}

        _dataService.DeleteCategory(category.Id);

        return Ok(category);
    }


    private CategoryModel? CreateCategoryDto(Category? category)
    {
        if (category == null) return null;

        var Dto = category.Adapt<CategoryModel>();
        Dto.Url = GetUrl(category.Id);

        return Dto;
        
    }


    private string? GetUrl(int id)
    {
        return _linkGenerator.GetUriByName(HttpContext, nameof(GetCategory), new { id });
    }
}
