using ModularAuth.Domain.Errors;
using System.Net;

namespace ModularAuth.Api.Common.Mappers;

/// <summary>
/// Maps domain <see cref="ErrorType"/> values to corresponding HTTP status codes.
///
/// This ensures that API responses correctly communicate the nature of the result
/// using standard HTTP semantics, improving interoperability with clients,
/// proxies, and monitoring systems.
/// </summary>
public static class HttpStatusCodeMapper
{
    /// <summary>
    /// Maps a domain <see cref="ErrorType"/> to an HTTP status code.
    /// </summary>
    /// <param name="errorType">
    /// The type of error produced by the domain layer.
    /// </param>
    /// <returns>
    /// The corresponding HTTP status code.
    /// </returns>
    public static HttpStatusCode Map(ErrorType errorType)
    {
        return errorType switch
        {
            ErrorType.Validation => HttpStatusCode.BadRequest,        // 400
            ErrorType.NotFound => HttpStatusCode.NotFound,            // 404
            ErrorType.Unauthorized => HttpStatusCode.Unauthorized,    // 401
            ErrorType.Forbidden => HttpStatusCode.Forbidden,          // 403
            ErrorType.Conflict => HttpStatusCode.Conflict,            // 409
            ErrorType.BusinessRule => HttpStatusCode.Conflict,        // 409
            _ => HttpStatusCode.InternalServerError                  // 500
        };
    }
}