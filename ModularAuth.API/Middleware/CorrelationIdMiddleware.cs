namespace ModularAuth.Api.Middleware;

/// <summary>
/// Middleware responsible for generating and attaching
/// a unique Correlation ID to each incoming request.
///
/// This enables request tracing across the system.
/// </summary>
public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;

    public const string CorrelationIdKey = "CorrelationId";

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Generate correlation ID once per request
        var correlationId = Guid.NewGuid().ToString();

        // Store in HttpContext for downstream usage
        context.Items[CorrelationIdKey] = correlationId;

        await _next(context);
    }
}