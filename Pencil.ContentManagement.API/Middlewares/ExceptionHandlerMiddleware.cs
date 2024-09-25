using System.Net;
using System.Net.Mime;
using Pencil.ContentManagement.Application.Exceptions;
using static System.Text.Json.JsonSerializer;

namespace Pencil.ContentManagement.API.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var httpStatusCode = HttpStatusCode.InternalServerError;
        
        context.Response.ContentType = MediaTypeNames.Application.Json;

        var result = string.Empty;

        (httpStatusCode, result) = exception switch
        {
            ValidationException ex => (HttpStatusCode.BadRequest, Serialize(ex.ValidationErrors)),
            _ => (httpStatusCode, Serialize(new { error = "An error occurred while processing your request." }))
        };

        context.Response.StatusCode = (int) httpStatusCode;
        
        await context.Response.WriteAsync(result);
    }
}