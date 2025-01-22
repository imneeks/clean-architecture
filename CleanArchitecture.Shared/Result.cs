﻿

using System.Text.Json.Serialization;

namespace CleanArchitecture.Shared
{
    public class Result
    {
        protected internal Result(bool isSuccess, Error error)
        {
            IsSuccess = isSuccess;
            Error = error;

        }

        public bool IsSuccess { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Error Error { get; }

        public static Result Success() => new(true, Error.None);
        public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);
        public static Result Failure(Error error) => new(false, error);
        public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);
    }

    public class Result<TValue> : Result
    {
        private readonly TValue? _value;
        protected internal Result(TValue? value, bool isSuccess, Error error) : base(isSuccess, error)
        {
            _value = value;
        }

        public TValue Data => IsSuccess ? _value! : throw new InvalidOperationException("Invalid Operation found for the Value");
        public static implicit operator Result<TValue>(TValue? value) => value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
    }
}
