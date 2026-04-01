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
    /// <summary>
    /// Creates a new <see cref="ApiMeta"/> instance.
    /// </summary>
    /// <returns>
    /// A populated metadata object.
    /// </returns>
    public ApiMeta Create()
    {
        return new ApiMeta
        {
            CorrelationId = Guid.NewGuid().ToString(),
            Timestamp = DateTime.UtcNow
        };
    }
}