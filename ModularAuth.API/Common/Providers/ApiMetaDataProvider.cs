using ModularAuth.Api.Common.Abstractions;
using ModularAuth.Api.Common.Responses;

namespace ModularAuth.Api.Common.Providers;

/// <summary>
/// Default implementation of <see cref="IApiMetadataProvider"/>.
///
/// Responsible for generating API metadata including:
/// - Correlation ID (unique per response/request)
/// - Timestamp (UTC)
///
/// This implementation is simple for now,
/// but will evolve later to integrate with:
/// - Request tracing
/// - Logging systems
/// - Distributed systems observability
/// </summary>
public class ApiMetadataProvider : IApiMetadataProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApiMetadataProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    /// <summary>
    /// Creates a new <see cref="ApiMeta"/> instance.
    /// Provides API metadata using request-scoped context.
    /// </summary>
    /// <returns>
    /// A populated metadata object.
    /// </returns>
    public ApiMeta Create()
    {
        var context = _httpContextAccessor.HttpContext;

        var correlationId = context?.Items[Middleware.CorrelationIdMiddleware.CorrelationIdKey]?.ToString()
                            ?? Guid.NewGuid().ToString();

        return new ApiMeta
        {
            CorrelationId = correlationId,
            Timestamp = DateTime.UtcNow
        };
    }
}