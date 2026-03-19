namespace ModularAuth.Api.Common.Responses;

/// <summary>
/// Represents a client-safe error object returned by the API.
/// This object is designed to expose necessary error information
/// without leaking internal implementation details.
/// </summary>
public class ApiError
{
    /// <summary>
    /// A machine-readable error code.
    /// This value should be stable and used for client-side logic,
    /// logging, and mapping.
    /// </summary>
    public string Code { get; init; } = default!;

    /// <summary>
    /// A human-readable error message.
    /// Intended for display or diagnostics.
    /// Should not expose sensitive internal details.
    /// </summary>
    public string Message { get; init; } = default!;

    /// <summary>
    /// The category or classification of the error.
    /// Typically derived from the domain error type.
    /// Example values: Validation, NotFound, Unauthorized.
    /// </summary>
    public string Type { get; init; } = default!;
}