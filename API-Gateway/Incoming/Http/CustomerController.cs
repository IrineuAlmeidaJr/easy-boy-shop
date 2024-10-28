using Application.Client;
using Application.DTO.Customer.Request;
using Application.DTO.Log;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Incoming.Http;

[ApiController]
[Tags("Customer")]
[Route("/api-gateway/v1/customer")]
public class CustomerController : ControllerBase
{

    private readonly ILogServices _logService;
    private readonly ICustomerClient _customerClient;

    public CustomerController(ILogServices logService, ICustomerClient customerClient)
    {
        _logService = logService;
        _customerClient = customerClient;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CustomerRequest), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] CustomerRequest request)
    {
        //LogRequest logRequest = new(HttpContext.Connection.RemoteIpAddress?.ToString(),
        //   $"{{\"Method\": \"{HttpContext.Request.Method}\", \"Path\": \"{HttpContext.Request.Path}\"}}");

        //try
        //{
        var response = await _customerClient.CreateCustomerAsync(request);
        return Ok(response);
        //}
        //catch (Exception ex)
        //{
        //    logRequest.StatusCode = ex;

        //    return BadRequest(ex);
        //}
        //finally
        //{
        //    await _logService.Create(logRequest);
        //}
    }    

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CustomerRequest), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCustomer([FromRoute] Guid id)
    {
        //LogRequest logRequest = new(HttpContext.Connection.RemoteIpAddress?.ToString(),
        //   $"{{\"Method\": \"{HttpContext.Request.Method}\", \"Path\": \"{HttpContext.Request.Path}\"}}");

        //try
        //{
            var response = await _customerClient.GetCustomerByIdAsync(id);
            return Ok(response);
        //}
        //catch (Exception ex)
        //{
        //    logRequest.StatusCode = ex;

        //    return BadRequest(ex);
        //}
        //finally
        //{
        //    await _logService.Create(logRequest);
        //}
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(CustomerRequest), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCustomers()
    {
        //LogRequest logRequest = new(HttpContext.Connection.RemoteIpAddress?.ToString(),
        //   $"{{\"Method\": \"{HttpContext.Request.Method}\", \"Path\": \"{HttpContext.Request.Path}\"}}");

        //try
        //{
        var response = await _customerClient.GetCustomerAllAsync();
        return Ok(response);
        //}
        //catch (Exception ex)
        //{
        //    logRequest.StatusCode = ex;

        //    return BadRequest(ex);
        //}
        //finally
        //{
        //    await _logService.Create(logRequest);
        //}
    }

    [HttpPut]
    [ProducesResponseType(typeof(CustomerRequest), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] CustomerRequest request)
    {
        //LogRequest logRequest = new(HttpContext.Connection.RemoteIpAddress?.ToString(),
        //   $"{{\"Method\": \"{HttpContext.Request.Method}\", \"Path\": \"{HttpContext.Request.Path}\"}}");

        //try
        //{
        var response = await _customerClient.CreateCustomerAsync(request);
        return Ok(response);
        //}
        //catch (Exception ex)
        //{
        //    logRequest.StatusCode = ex;

        //    return BadRequest(ex);
        //}
        //finally
        //{
        //    await _logService.Create(logRequest);
        //}
    }



}
