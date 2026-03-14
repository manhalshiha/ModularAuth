namespace ModularAuth.Domain.Results;

using ModularAuth.Domain.Errors;


/// <summary>
/// Represents the outcome of an operation with a return value.
/// </summary>
/// <typeparam name="T">The type of the returned value.</typeparam>
public class Result<T> : Result
{
    /// <summary>
    /// The value returned by a successful operation.
    /// Accessing this when the result is a failure is invalid.
    /// </summary>
    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Cannot access Value when the result is a failure.");

    private readonly T? _value;

    /// <summary>
    /// Initializes a successful result with a value.
    /// </summary>
    /// <param name="value">The value produced by the operation.</param>
    protected Result(T value)
        : base()
    {
        _value = value;
    }

    /// <summary>
    /// Initializes a failed result with a specific error.
    /// </summary>
    /// <param name="error">The domain error describing the failure.</param>
    protected Result(Error error)
        : base(error)
    {
        _value = default;
    }

    /// <summary>
    /// Creates a successful result containing a value.
    /// </summary>
    public static Result<T> Success(T value) => new(value);

    /// <summary>
    /// Creates a failed result.
    /// </summary>
    /// <param name="error">The domain error describing the failure.</param>
    public static new Result<T> Failure(Error error) => new(error);
}