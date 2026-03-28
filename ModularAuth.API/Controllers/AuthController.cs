using Microsoft.AspNetCore.Mvc;
using ModularAuth.Api.Common.Mappers;
using ModularAuth.Api.Contracts.Auth;

namespace ModularAuth.Api.Controllers;

/// <summary>
/// Handles authentication-related endpoints.
/// </summary>
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="request">Incoming registration request.</param>
    /// <returns>Temporary response.</returns>
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        // Mapping (ACL)
        var command = RegisterRequestMapper.ToCommand(request);

        // لاحقًا: سنرسل هذا للـ Application Layer
        return Ok(command);
    }
}