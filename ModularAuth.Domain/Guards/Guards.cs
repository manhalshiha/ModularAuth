using ModularAuth.Domain.Errors;
using ModularAuth.Domain.Results;

namespace ModularAuth.Domain.Guards;

/// <summary>
/// Provides fail-fast guard methods to protect
/// domain logic from invalid input.
/// </summary>
public static class Guard
{
    /// <summary>
    /// Ensures that a string value is not null or empty.
    /// Returns a failure result immediately if the check fails.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="errorCode">The error code to use if validation fails.</param>
    /// <param name="errorDescription">The error description.</param>
    /// <returns>
    /// A failure <see cref="Result"/> if the value is invalid;
    /// otherwise, a success <see cref="Result"/>.
    /// </returns>
    public static Result EnsureNotNullOrEmpty(
        string? value,
        string errorCode,
        string errorDescription)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure(
                Error.Validation(errorCode, errorDescription));
        }

        return Result.Success();
    }
}