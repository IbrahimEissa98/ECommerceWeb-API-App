namespace ECommerceApp.Domain.Common;

public class Result
{
    public bool IsSuccess { get; }
    public Error? Error { get; private set; }
    public bool IsFailure => !IsSuccess;

    protected Result(bool isSuccess, Error? error = null)
    {
        if (isSuccess && error is not null)
            throw new InvalidOperationException("Success result cannot has an error");
        if (!isSuccess && error is null)
            throw new InvalidOperationException("Failure result must has an error");

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success()
        => new(true);

    public static Result Failure(Error error)
        => new(false, error);
}


public sealed class Result<TValue> : Result
{
    public TValue? Value { get; }

    private Result(TValue? value, bool isSuccess, Error? error = null)
        : base(isSuccess, error)
    {
        Value = value;
    }

    public static Result<TValue> Success(TValue value)
        => new(value, true);

    public static new Result<TValue> Failure(Error error)
        => new(default, false, error);
}
