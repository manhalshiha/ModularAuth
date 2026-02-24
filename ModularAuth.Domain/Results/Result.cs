namespace ModularAuth.Domain.Results;


using ModularAuth.Domain.Errors;

/// <summary>
/// Represents the outcome of an operation without a return value.
/// </summary>
public class Result
{
    /// <summary>
    /// Indicates whether the operation succeeded.
    /// </summary>
    public bool IsSuccess => _error is null;

    /// <summary>
    /// Indicates whether the operation failed.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// The error associated with a failed result.
    /// Null when the result represents success.
    /// </summary>
    public Error? Error => _error;

    private readonly Error? _error;

    /// <summary>
    /// Initializes a successful result.
    /// </summary>
    protected Result()
    {
        _error = null;
    }

    /// <summary>
    /// Initializes a failed result with a specific error.
    /// </summary>
    /// <param name="error">The domain error describing the failure.</param>
    protected Result(Error error)
    {
        _error = error;
    }

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    public static Result Success() => new();

    /// <summary>
    /// Creates a failed result.
    /// </summary>
    /// <param name="error">The domain error describing the failure.</param>
    public static Result Failure(Error error) => new(error);
}