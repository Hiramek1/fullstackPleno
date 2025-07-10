namespace Size.Receivables.CrossCutting.Services;

public class CustomResult
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string? Error { get; }

    protected CustomResult(bool isSuccess, string? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static CustomResult Ok() => new(true, null);
    public static CustomResult Fail(string error) => new(false, error);
}

public class CustomResult<T> : CustomResult
{
    public T? Value { get; }

    protected CustomResult(bool isSuccess, T? value, string? error)
        : base(isSuccess, error)
    {
        Value = value;
    }

    public static CustomResult<T> Ok(T value) => new(true, value, null);
    public new static CustomResult<T> Fail(string error) => new(false, default, error);
}
