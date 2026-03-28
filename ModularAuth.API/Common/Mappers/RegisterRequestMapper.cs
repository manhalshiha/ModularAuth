using ModularAuth.Api.Contracts.Auth;
using ModularAuth.Application.Auth.Commands;

namespace ModularAuth.Api.Common.Mappers;

/// <summary>
/// Provides mapping logic between API DTOs and application commands.
/// This acts as an Anti-Corruption Layer.
/// </summary>
public static class RegisterRequestMapper
{
    /// <summary>
    /// Maps a <see cref="RegisterRequest"/> to a <see cref="RegisterUserCommand"/>.
    /// </summary>
    /// <param name="request">The incoming API request.</param>
    /// <returns>A corresponding application command.</returns>
    public static RegisterUserCommand ToCommand(RegisterRequest request)
    {
        return new RegisterUserCommand(
            request.Email,
            request.Password
        );
    }
}