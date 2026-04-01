using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ModularAuth.Api.Common.Mappers;
using ModularAuth.Domain.Results;

namespace ModularAuth.Api.Common.Extensions;

/// <summary>
/// Provides extension methods for controllers to convert domain-level results
/// into standardized API responses.
///
/// This class acts as a boundary adapter between:
/// - Application/Domain layers (Result pattern)
/// - API layer (ApiResponse contract)
///
/// IMPORTANT:
/// This ensures that all controllers:
/// - Return a unified response shape
/// - Do not manually construct responses
/// - Remain thin and free of mapping logic
///
/// Design Notes:
/// - Uses Service Locator pattern (via RequestServices) because extension methods are static
/// - This is an acceptable exception at the API boundary layer only
/// </summary>
public static class ControllerExtensions
{
    /// <summary>
    /// Converts a non-generic <see cref="Result"/> into an <see cref="IActionResult"/>.
    ///
    /// This method:
    /// - Maps Result → ApiResponse
    /// - Applies correct HTTP status codes
    /// - Ensures metadata is always included
    /// </summary>
    /// <param name="controller">The current controller instance.</param>
    /// <param name="result">The domain result to convert.</param>
    /// <returns>
    /// A standardized HTTP response containing an <see cref="ApiResponse"/>.
    /// </returns>
    public static IActionResult ToApiResponse(
        this ControllerBase controller,
        Result result)
    {
        // Resolve the mapper from DI container
        // NOTE: Service Locator is used here due to static context
        var mapper = controller.HttpContext.RequestServices
            .GetRequiredService<ResultToApiResponseMapper>();

        var response = mapper.ToResponse(result);

        if (result.IsSuccess)
        {
            return controller.Ok(response);
        }

        var statusCode = (int)HttpStatusCodeMapper.Map(result.Error!.Type);

        return controller.StatusCode(statusCode, response);
    }

    /// <summary>
    /// Converts a generic <see cref="Result{T}"/> into an <see cref="IActionResult"/>.
    ///
    /// This method:
    /// - Maps Result<T> → ApiResponse<T>
    /// - Applies correct HTTP status codes
    /// - Ensures metadata is always included
    /// </summary>
    /// <typeparam name="T">The type of the result value.</typeparam>
    /// <param name="controller">The current controller instance.</param>
    /// <param name="result">The domain result to convert.</param>
    /// <returns>
    /// A standardized HTTP response containing an <see cref="ApiResponse{T}"/>.
    /// </returns>
    public static IActionResult ToApiResponse<T>(
        this ControllerBase controller,
        Result<T> result)
    {
        var mapper = controller.HttpContext.RequestServices
            .GetRequiredService<ResultToApiResponseMapper>();

        var response = mapper.ToResponse(result);

        if (result.IsSuccess)
        {
            return controller.Ok(response);
        }

        var statusCode = (int)HttpStatusCodeMapper.Map(result.Error!.Type);

        return controller.StatusCode(statusCode, response);
    }
}