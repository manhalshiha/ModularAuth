namespace ModularAuth.Application.Auth.Commands;

/// <summary>
/// Represents a command to register a new user.
/// This belongs to the application layer and acts as a boundary
/// between API and Domain.
/// </summary>
public class RegisterUserCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterUserCommand"/> class.
    /// </summary>
    /// <param name="email">User email.</param>
    /// <param name="password">User password.</param>
    public RegisterUserCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }

    /// <summary>
    /// User email.
    /// </summary>
    public string Email { get; }

    /// <summary>
    /// User password.
    /// </summary>
    public string Password { get; }
}