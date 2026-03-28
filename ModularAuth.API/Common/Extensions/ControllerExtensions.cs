using Microsoft.AspNetCore.Mvc;
using ModularAuth.Api.Common.Mappers;
using ModularAuth.Api.Common.Responses;
using ModularAuth.Domain.Results;

namespace ModularAuth.Api.Common.Extensions;

/// <summary>
/// Provides extension methods for converting domain results
/// into HTTP responses with proper status codes.
/// </summary>
public static class ControllerExtensions
{
    /// <summary>
    /// Converts a non-generic <see cref="Result"/> into an <see cref="IActionResult"/>
    /// with the appropriate HTTP status code.
    /// </summary>
    /// <param name="controller">
    /// The controller instance.
    /// </param>
    /// <param name="result">
    /// The domain result.
    /// </param>
    /// <returns>
    /// A properly formatted HTTP response.
    /// </returns>
    public static IActionResult ToApiResponse(
        this ControllerBase controller,
        Result result)
    {
        var response = ResultToApiResponseMapper.ToResponse(result);

        if (result.IsSuccess)
        {
            return controller.Ok(response);
        }

        var statusCode = (int)HttpStatusCodeMapper.Map(result.Error!.Type);

        return controller.StatusCode(statusCode, response);
    }

    /// <summary>
    /// Converts a generic <see cref="Result{T}"/> into an <see cref="IActionResult"/>
    /// with the appropriate HTTP status code.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the result value.
    /// </typeparam>
    /// <param name="controller">
    /// The controller instance.
    /// </param>
    /// <param name="result">
    /// The domain result.
    /// </param>
    /// <returns>
    /// A properly formatted HTTP response.
    /// </returns>
    public static IActionResult ToApiResponse<T>(
        this ControllerBase controller,
        Result<T> result)
    {
        var response = ResultToApiResponseMapper.ToResponse(result);

        if (result.IsSuccess)
        {
            return controller.Ok(response);
        }

        var statusCode = (int)HttpStatusCodeMapper.Map(result.Error!.Type);

        return controller.StatusCode(statusCode, response);
    }
}