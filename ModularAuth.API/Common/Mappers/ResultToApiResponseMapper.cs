using ModularAuth.Domain.Results;
using ModularAuth.Api.Common.Responses;

namespace ModularAuth.Api.Common.Mappers;

/// <summary>
/// Provides mapping functionality between domain <see cref="Result"/>
/// objects and API-level <see cref="ApiResponse{T}"/> objects.
///
/// Acts as a translation layer between the Domain and API boundaries,
/// ensuring that domain concepts are not leaked directly to clients.
/// </summary>
public static class ResultToApiResponseMapper
{
    /// <summary>
    /// Converts a non-generic <see cref="Result"/> into an <see cref="ApiResponse{T}"/>.
    /// </summary>
    /// <param name="result">
    /// The domain result representing the outcome of an operation.
    /// </param>
    /// <returns>
    /// A standardized API response.
    /// </returns>
    public static ApiResponse<object?> ToResponse(Result result)
    {
        var metadata = CreateMetadata();

        if (result.IsSuccess)
        {
            return ApiResponse<object?>.SuccessResponse(null, metadata);
        }

        var apiError = ApiErrorMapper.Map(result.Error!);

        return ApiResponse<object?>.FailureResponse(apiError, metadata);
    }

    /// <summary>
    /// Converts a generic <see cref="Result{T}"/> into an <see cref="ApiResponse{T}"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the value contained in the result.
    /// </typeparam>
    /// <param name="result">
    /// The domain result representing the outcome of an operation.
    /// </param>
    /// <returns>
    /// A standardized API response.
    /// </returns>
    public static ApiResponse<T> ToResponse<T>(Result<T> result)
    {
        var metadata = CreateMetadata();

        if (result.IsSuccess)
        {
            return ApiResponse<T>.SuccessResponse(result.Value, metadata);
        }

        var apiError = ApiErrorMapper.Map(result.Error!);

        return ApiResponse<T>.FailureResponse(apiError, metadata);
    }

    /// <summary>
    /// Creates default metadata for API responses.
    /// </summary>
    /// <returns>
    /// A populated <see cref="ApiMeta"/> instance.
    /// </returns>
    private static ApiMeta CreateMetadata()
    {
        return new ApiMeta
        {
            CorrelationId = Guid.NewGuid().ToString(),
            Timestamp = DateTime.UtcNow
        };
    }
}