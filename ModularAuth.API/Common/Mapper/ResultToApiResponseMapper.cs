using ModularAuth.Domain.Results;
using ModularAuth.Api.Common.Responses;

namespace ModularAuth.Api.Common.Mappers;

/// <summary>
/// Provides mapping functionality between domain <see cref="Result"/> objects
/// and API-level <see cref="ApiResponse{T}"/> objects.
/// 
/// This acts as an Adapter between the Domain layer and the API layer,
/// ensuring separation of concerns and preventing leakage of domain concepts
/// into the presentation layer.
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
    /// A standardized API response representing the result.
    /// If the result is successful, a success response is returned with no data.
    /// If the result is a failure, an error response is returned.
    /// </returns>
    public static ApiResponse<object?> ToResponse(Result result)
    {
        if (result.IsSuccess)
        {
            return ApiResponse<object?>.SuccessResponse(null);
        }

        return ApiResponse<object?>.FailureResponse(
            new ApiError
            {
                Code = result.Error!.Code,
                Message = result.Error.Description,
                Type = result.Error.Type.ToString()
            });
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
    /// A standardized API response:
    /// - Success response containing the value when the operation succeeds.
    /// - Failure response containing mapped error details when the operation fails.
    /// </returns>
    public static ApiResponse<T> ToResponse<T>(Result<T> result)
    {
        if (result.IsSuccess)
        {
            return ApiResponse<T>.SuccessResponse(result.Value);
        }

        return ApiResponse<T>.FailureResponse(
            new ApiError
            {
                Code = result.Error!.Code,
                Message = result.Error.Description,
                Type = result.Error.Type.ToString()
            });
    }
}