namespace ModularAuth.Api.Common.Responses;

/// <summary>
/// Represents a standardized API response envelope.
/// Ensures all API responses follow a consistent and predictable structure.
/// </summary>
/// <typeparam name="T">
/// The type of the data returned in the response.
/// Can be null in case of failure.
/// </typeparam>
public class ApiResponse<T>
{
    /// <summary>
    /// Indicates whether the request was successfully processed.
    /// </summary>
    public bool Success { get; init; }

    /// <summary>
    /// The data returned by the API when the request succeeds.
    /// Will be null if <see cref="Success"/> is false.
    /// </summary>
    public T? Data { get; init; }

    /// <summary>
    /// Contains error details when the request fails.
    /// Will be null if <see cref="Success"/> is true.
    /// </summary>
    public ApiError? Error { get; init; }

    /// <summary>
    /// Optional metadata that can include additional contextual information.
    /// This is intentionally flexible to support different scenarios
    /// such as pagination, tracing, or caching without enforcing a rigid schema.
    /// </summary>
    public object? Metadata { get; init; }

    /// <summary>
    /// Public constructor to enforce controlled creation via factory methods.
    /// Parameterless constructor required for serialization/deserialization.
    /// </summary>
    public ApiResponse() { }

    /// <summary>
    /// Creates a successful API response.
    /// </summary>
    /// <param name="data">
    /// The payload to return to the client.
    /// </param>
    /// <param name="metadata">
    /// Optional metadata associated with the response (e.g. pagination info).
    /// </param>
    /// <returns>
    /// A successful <see cref="ApiResponse{T}"/> instance.
    /// </returns>
    public static ApiResponse<T> SuccessResponse(T data, object? metadata = null)
        => new()
        {
            Success = true,
            Data = data,
            Metadata = metadata,
            Error = null
        };

    /// <summary>
    /// Creates a failed API response.
    /// </summary>
    /// <param name="error">
    /// The error details describing why the request failed.
    /// </param>
    /// <returns>
    /// A failed <see cref="ApiResponse{T}"/> instance.
    /// </returns>
    public static ApiResponse<T> FailureResponse(ApiError error)
        => new()
        {
            Success = false,
            Error = error,
            Data = default,
            Metadata = null
        };
}