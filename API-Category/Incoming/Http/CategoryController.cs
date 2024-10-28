using Domain.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Interface;
using Domain.Exception;
using Application.DTO;

namespace Incoming.Http;

[ApiController]
[Route("/api/v1/category")]
[Tags("Category")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryServices _categoryServices;

    public CategoryController(ICategoryServices categoryServices)
    {
        _categoryServices = categoryServices;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Create([FromBody] CategoryDto request)
    {
        var response = await _categoryServices.Create(request);
        var url = Url.Action(nameof(GetCategoryById), new { id = response.Id });

        return Created(url, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryDto>> GetCategoryById([FromRoute] Guid id)
    {
        var response = await _categoryServices.GetCategoryById(id);
        return Ok(response);
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryDto>> GetCategories()
    {
        var response = await _categoryServices.GetCategories();
        return Ok(response);
    }
}
