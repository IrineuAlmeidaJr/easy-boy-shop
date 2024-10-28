using Application.Client;
using Application.DTO.Customer.Request;
using Application.DTO.Seller;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Incoming.Http;

[ApiController]
[Tags("Seller")]
[Route("/api-gateway/v1/seller")]
public class SellerController : ControllerBase
{
    private readonly ISellerClient _sellerClient;

    public SellerController(ISellerClient sellerClient)
    {
        _sellerClient = sellerClient;
    }

    [HttpPost]
    //[ProducesResponseType(typeof(SellerDto), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] SellerDto request)
    {
        var response = await _sellerClient.CreateSellerAsync(request);
        return Ok(response);
    }

    [HttpGet("{id}")]
    //[ProducesResponseType(typeof(SellerDto), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCustomer([FromRoute] Guid id)
    {
        var response = await _sellerClient.GetSellerByIdAsync(id);
        return Ok(response);
    }


    [HttpGet("all")]
    //[ProducesResponseType(typeof(SellerDto), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCustomers()
    {
        var response = await _sellerClient.GetSellersAsync();
        return Ok(response);
    }
}
