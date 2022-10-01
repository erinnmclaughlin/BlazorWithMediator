namespace BlazorWithMediator.Shared.Common;

public class Result
{
    public bool IsSuccess { get; }
    public string[] Messages { get; } = Array.Empty<string>();

    public Result(bool isSuccess, string[] messages)
    {
        IsSuccess = isSuccess;
        Messages = messages;
    }

    public static Result Success() => new(true, Array.Empty<string>());
    public static Result Fail() => Fail(false, Array.Empty<string>());
    public static Result Fail(string message) => Fail(new[] { message });
    public static Result Fail(string[] messages) => new(false, messages);

    public static Result<T> Success<T>(T? data) => new(data, true, Array.Empty<string>());
    public static Result<T> Fail<T>() => Fail<T>(default, Array.Empty<string>());
    public static Result<T> Fail<T>(T? data) => Fail(data, Array.Empty<string>());
    public static Result<T> Fail<T>(T? data, string message) => Fail(data, new[] { message });
    public static Result<T> Fail<T>(T? data, string[] messages) => new(data, false, messages);
}

public class Result<T> : Result
{
    public T? Data { get; }

    public Result(T? data, bool isSuccess, string[] messages) : base(isSuccess, messages)
    {
        Data = data;
    }
}

public record PagedResult<T>(T[] Data, int PageNumber, int PageSize, int TotalCount, int PageLimit, string[] Messages);