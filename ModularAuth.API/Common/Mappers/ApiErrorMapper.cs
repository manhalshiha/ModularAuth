using ModularAuth.Domain.Errors;
using ModularAuth.Api.Common.Responses;

namespace ModularAuth.Api.Common.Mappers;

/// <summary>
/// Responsible for mapping domain <see cref="Error"/> objects
/// to API-level <see cref="ApiError"/> representations.
///
/// This abstraction centralizes error transformation logic
/// and prevents duplication across the API layer.
/// </summary>
public static class ApiErrorMapper
{
    /// <summary>
    /// Converts a domain <see cref="Error"/> into an <see cref="ApiError"/>.
    /// </summary>
    /// <param name="error">
    /// The domain error to map.
    /// </param>
    /// <returns>
    /// A client-safe <see cref="ApiError"/> instance.
    /// </returns>
    public static ApiError Map(Error error)
    {
        return new ApiError
        {
            Code = error.Code,
            Message = error.Description,
            Type = error.Type.ToString()
        };
    }
}