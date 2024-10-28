using Application.DTO;
using Application.Interface;
using Domain.Exception;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Incoming.Http;

[ApiController]
[Route("/api/v1/seller")]
[Tags("Seller")]
public class SellerController : ControllerBase
{
    private readonly ISellerServices _sellerServices;

    public SellerController(ISellerServices sellerService)
    {
        _sellerServices = sellerService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(SellerResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Create([FromBody] SellerRequest request)
    {
        var response = await _sellerServices.Create(request);
        var url = Url.Action(nameof(GetById), new { id = response.Id });

        return Created(url, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SellerResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SellerResponse>> GetById([FromRoute] Guid id)
    {
        var response = await _sellerServices.GetSellerById(id);
        return Ok(response);        
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(SellerResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SellerResponse>> GetCustomers()
    {
        var response = await _sellerServices.GetSellers();
        return Ok(response);
       
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _sellerServices.Delete(id);
        return NoContent();
    }
}
