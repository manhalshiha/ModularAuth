using Microsoft.AspNetCore.Mvc;
using ModularAuth.Api.Common.Extensions;
using ModularAuth.Domain.Errors;
using ModularAuth.Domain.Results;

namespace ModularAuth.Api.Controllers;

/// <summary>
/// Test controller used only for integration testing.
/// </summary>
[ApiController]
[Route("test")]
public class TestController : ControllerBase
{
#if DEBUG
    /// <summary>
    /// Endpoint that always throws an exception.
    /// Used to verify global exception handling.
    /// </summary>
    [HttpGet("exception")]
    public IActionResult ThrowException()
    {
        throw new Exception("Test exception");
    }
#endif


    /// <summary>
    /// Test controller used to validate API response pipeline behavior.
    ///
    /// This controller demonstrates how domain results are transformed
    /// into HTTP responses using extension methods, ensuring that
    /// controllers remain thin and free of business or mapping logic.
    /// </summary>



    /// <summary>
    /// Returns a successful result.
    /// </summary>
    /// <returns>
    /// A standardized API response representing a successful operation.
    /// </returns>
    [HttpGet("success")]
    public IActionResult GetSuccess()
    {
        var result = Result<string>.Success("Hello World");

        return this.ToApiResponse(result);
    }

    /// <summary>
    /// Returns a failure result.
    /// </summary>
    /// <returns>
    /// A standardized API response representing a failed operation.
    /// </returns>
    [HttpGet("failure")]
    public IActionResult GetFailure()
    {
        var error = Error.Validation("TEST_ERROR", "This is a test validation error");

        var result = Result<string>.Failure(error);

        return this.ToApiResponse(result);
    }
}
