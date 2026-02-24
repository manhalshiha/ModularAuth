namespace ModularAuth.Domain.Errors;

/// <summary>
/// Represents a domain error as a value object.
/// An error describes why a domain operation failed,
/// without relying on exceptions or infrastructure concepts.
/// </summary>
public sealed class Error
{
    /// <summary>
    /// A unique and stable code identifying the error.
    /// Used for logging, auditing, and client-side mapping.
    /// </summary>
    public string Code { get; }

    /// <summary>
    /// A human-readable description of the error.
    /// Intended for diagnostics, not UI rendering.
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// The category of the error.
    /// Used to classify the error for higher layers.
    /// </summary>
    public ErrorType Type { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Error"/> class.
    /// Constructor is private to enforce controlled creation.
    /// </summary>
    private Error(string code, string description, ErrorType type)
    {
        Code = code;
        Description = description;
        Type = type;
    }

    /// <summary>
    /// Creates a validation error.
    /// </summary>
    public static Error Validation(string code, string description) =>
        new(code, description, ErrorType.Validation);

    /// <summary>
    /// Creates a not-found error.
    /// </summary>
    public static Error NotFound(string code, string description) =>
        new(code, description, ErrorType.NotFound);

    /// <summary>
    /// Creates an unauthorized error.
    /// </summary>
    public static Error Unauthorized(string code, string description) =>
        new(code, description, ErrorType.Unauthorized);

    /// <summary>
    /// Creates a business rule violation error.
    /// </summary>
    public static Error BusinessRule(string code, string description) =>
        new(code, description, ErrorType.BusinessRule);
}