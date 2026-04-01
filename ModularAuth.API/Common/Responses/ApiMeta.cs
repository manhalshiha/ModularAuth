namespace ModularAuth.Api.Common.Responses;

/// <summary>
/// Represents metadata associated with an API response.
///
/// This object provides additional contextual information
/// that is not part of the main response data but is essential
/// for clients, monitoring systems, and infrastructure concerns.
///
/// Examples of metadata:
/// - Correlation identifiers for tracing requests
/// - Pagination details for paged responses
/// - Processing timestamps
///
/// This abstraction allows extending response capabilities
/// without breaking the API contract.
/// </summary>
public class ApiMeta
{
    /// <summary>
    /// A unique identifier used to trace a request across systems.
    ///
    /// This value is critical for debugging distributed systems,
    /// logging correlation, and observability.
    /// </summary>
    public string? CorrelationId { get; init; }

    /// <summary>
    /// The UTC timestamp when the response was generated.
    ///
    /// This helps clients understand timing and supports
    /// auditing and monitoring scenarios.
    /// </summary>
    public DateTime? Timestamp { get; init; }

    /// <summary>
    /// Optional pagination metadata.
    ///
    /// This can be used when returning paginated results
    /// to provide information such as page number, size,
    /// and total count.
    /// </summary>
    public object? Pagination { get; init; }
}