namespace ModularAuth.Api.Contracts.Auth;

/// <summary>
/// Represents the incoming request for user registration.
/// This is a pure DTO and should not contain any business logic.
/// </summary>
public class RegisterRequest
{
    /// <summary>
    /// User email address.
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// User password in plain text.
    /// </summary>
    public string Password { get; set; } = default!;
}