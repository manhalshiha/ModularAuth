using System.Net;
using Microsoft.Extensions.DependencyInjection;
using ModularAuth.Api.Common.Abstractions;
using ModularAuth.Api.Common.Responses;

namespace ModularAuth.Api.Middleware;

/// <summary>
/// Global exception handling middleware.
/// Ensures all unhandled exceptions are converted into standardized API responses.
/// </summary>
public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception)
        {
            await HandleExceptionAsync(context);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        // 🔥 الحل الحقيقي هنا
        var metadataProvider = context.RequestServices
            .GetRequiredService<IApiMetadataProvider>();

        var metadata = metadataProvider.Create();

        var response = ApiResponse<object?>.FailureResponse(
            new ApiError
            {
                Code = "SYSTEM_ERROR",
                Message = "An unexpected error occurred.",
                Type = "Failure"
            },
            metadata // 👈 تأكد أنه يُمرر هنا
        );

        await context.Response.WriteAsJsonAsync(response);
    }
}