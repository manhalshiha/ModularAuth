using ModularAuth.Api.Common.Responses;

namespace ModularAuth.Api.Middleware;

/// <summary>
/// Middleware responsible for handling all unhandled exceptions globally.
/// 
/// This ensures:
/// - No internal details are leaked to clients
/// - All errors follow a consistent API response format
/// - System failures are properly translated into API-friendly responses
/// </summary>
public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Initializes a new instance of the <see cref="GlobalExceptionMiddleware"/> class.
    /// </summary>
    /// <param name="next">
    /// The next middleware in the pipeline.
    /// </param>
    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Invokes the middleware to process the HTTP request.
    /// Wraps the execution pipeline in a try/catch block to intercept all unhandled exceptions.
    /// </summary>
    /// <param name="context">
    /// The current HTTP context.
    /// </param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// Handles an exception and converts it into a standardized API response.
    /// </summary>
    /// <param name="context">
    /// The current HTTP context.
    /// </param>
    /// <param name="exception">
    /// The exception that was thrown during request processing.
    /// </param>
    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var apiResponse = ApiResponse<object?>.FailureResponse(
            new ApiError
            {
                Code = "SYSTEM_ERROR",
                Message = "An unexpected error occurred.",
                Type = "Failure"
            });

        await context.Response.WriteAsJsonAsync(apiResponse);
    }
}