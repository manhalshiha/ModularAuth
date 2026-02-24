namespace ModularAuth.Domain.Errors;

/// <summary>
/// Represents the category of an error.
/// </summary>
public enum ErrorType
{
    Failure = 0,
    Validation = 1,
    NotFound = 2,
    Unauthorized = 3,
    Forbidden = 4,
    Conflict = 5,
    BusinessRule=6
}
