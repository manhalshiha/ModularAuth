using ModularAuth.Api.Common.Responses;

namespace ModularAuth.Api.Common.Abstractions;

/// <summary>
/// Provides a centralized way to generate API metadata.
///
/// This abstraction ensures that all API responses share
/// a consistent metadata structure (e.g., correlation ID, timestamps),
/// and prevents duplication across the system.
///
/// This is a cross-cutting concern and must remain isolated
/// from business logic.
/// </summary>
public interface IApiMetadataProvider
{
    /// <summary>
    /// Creates a new <see cref="ApiMeta"/> instance.
    ///
    /// This method is responsible for generating metadata
    /// such as correlation identifiers and timestamps.
    /// </summary>
    /// <returns>
    /// A fully populated <see cref="ApiMeta"/> object.
    /// </returns>
    ApiMeta Create();
}