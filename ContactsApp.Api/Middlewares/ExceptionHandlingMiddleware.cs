using System.Net;
using System.Text.Json;

namespace ContactsApp.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    private static Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var response = new
        {
            StatusCode = context.Response.StatusCode,
            Message = message
        };
        
        var jsonResponse = JsonSerializer.Serialize(response);

        return context.Response.WriteAsync(jsonResponse);
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (KeyNotFoundException e)
        {
            await HandleExceptionAsync(context, HttpStatusCode.NotFound, e.Message);
        }
        catch (ArgumentException e)
        {
            await HandleExceptionAsync(context, HttpStatusCode.BadRequest, e.Message);
            
        }
        catch (ApplicationException ex)
        {
            await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, ex.Message);
        }

        catch (Exception e)
        {
            await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, e.Message);
        }
    }
}