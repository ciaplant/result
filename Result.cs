﻿namespace ciaplant.result;

public readonly struct Result<T, E>
{
    public T Value { get; }
    public readonly E Error;

    private Result(T v, E e, bool success)
    {
        Value = v;
        Error = e;
        IsOk = success;
    }

    public bool IsOk { get; }

    public static Result<T, E> Ok(T v)
    {
        return new Result<T, E>(v, default, true);
    }

    public static Result<T, E> Err(E e)
    {
        return new Result<T, E>(default, e, false);
    }

    public static implicit operator Result<T, E>(T v) => new(v, default, true);
    
    public static implicit operator Result<T, E>(E e) => new(default, e, false);

    public R Match<R>(
        Func<T, R> success,
        Func<E, R> failure) =>
        IsOk ? success(Value) : failure(Error);
}
