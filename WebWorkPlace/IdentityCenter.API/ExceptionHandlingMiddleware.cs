using System.ComponentModel.DataAnnotations;
using SharedLibrary.Exceptions;

namespace SharedLibrary;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context); // request weiterleiten
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status400BadRequest,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                InvalidCredentialsException => StatusCodes.Status401Unauthorized,
                TokenExpiredException => StatusCodes.Status401Unauthorized,
                InvalidTokenException => StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status500InternalServerError
            };

            var result = new
            {
                error = ex.Message,
                type = ex.GetType().Name
            };

            await context.Response.WriteAsJsonAsync(result);
        }
    }
}