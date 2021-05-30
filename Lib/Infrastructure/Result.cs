namespace Aoc.Lib.Infrastructure
{
    public class Result
    {
        public bool IsFailure => !IsSuccess;

        public bool IsSuccess { get; }

        public string Error { get; }

        public Result(bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public Result(bool isSuccess) : this(isSuccess, null) { }

        public static Result Fail(string error) => new(false, error);

        public static Result<T> Fail<T>(string error) =>
            new Result<T>(default(T), false, error);

        public static Result Ok() => new(true);

        public static Result<T> Ok<T>(T value) => new(value, true);
    }

    public sealed class Result<T> : Result
    {
        public T Value { get; }

        public Result(T value, bool isSuccess) : this(value, isSuccess, null) { }

        public Result(T value, bool isSuccess, string error) : base(isSuccess, error)
        {
            Value = value;
        }
    }
}
