using Domain.Exception;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace Infrastructure.Configuration;

public class ExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandler> _logger;

    public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DomainException ex)
        {
            await HandleDomainExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            await HandleGeneralExceptionAsync(context, ex);
        }
    }

    private Task HandleDomainExceptionAsync(HttpContext context, DomainException exception)
    {
        _logger.LogError(exception, "Domain Exception");

        var statusCode = exception.StatusCode;
        var problemDetails = new ProblemDetails
        {
            Status = (int)statusCode,
            Title = exception.Title,
            Detail = exception.Description,
            Instance = context.Request.Path
        };

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = (int)statusCode;
        var result = JsonSerializer.Serialize(problemDetails);
        return context.Response.WriteAsync(result);
    }

    private Task HandleGeneralExceptionAsync(HttpContext context, Exception exception)
    {
        ProblemDetails problemDetails;

        // Verifica se exceção capturada veio do Refit, ou seja, de um requisição Http
        if (exception is Refit.ApiException apiException && !string.IsNullOrWhiteSpace(apiException.Content))
        {
            _logger.LogError(exception, "API Exception");
            problemDetails = JsonSerializer.Deserialize<ProblemDetails>(apiException.Content)
                             ?? new ProblemDetails
                             {
                                 Status = (int)apiException.StatusCode,
                                 Title = "API Error",
                                 Detail = "An unexpected error occurred while processing the API response.",
                                 Instance = context.Request.Path
                             };
        }
        else
        {

            _logger.LogError(exception, "General Exception");
            problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "An unexpected error has occurred!",
                Detail = exception.Message,
                Instance = context.Request.Path
            };
        }

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = problemDetails.Status ?? (int)HttpStatusCode.InternalServerError;
        var result = JsonSerializer.Serialize(problemDetails);
        return context.Response.WriteAsync(result);
    }
    
}
