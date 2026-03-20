using Microsoft.AspNetCore.Mvc;

namespace ModularAuth.Api.Controllers;

/// <summary>
/// Test controller used only for integration testing.
/// </summary>
#if DEBUG
[ApiController]
[Route("test")]
public class TestController : ControllerBase
{
    /// <summary>
    /// Endpoint that always throws an exception.
    /// Used to verify global exception handling.
    /// </summary>
    [HttpGet("exception")]
    public IActionResult ThrowException()
    {
        throw new Exception("Test exception");
    }
}
# endif