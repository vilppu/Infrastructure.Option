using System.Linq;
using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;

namespace Infrastructure;

[JsonConverter(typeof(OptionJsonConverter))]
public abstract record Option<T>
{
    public static Some<T> Some(T value) =>
        new(value);

    public static Option<T> None =>
        None<T>.Instance;

    public static implicit operator Option<T>(T? value) =>
        value is null
            ? None
            : Option.Some(value);
}

[JsonConverter(typeof(OptionJsonConverter))]
public sealed record Some<T>(T Value) : Option<T>
{
    public static implicit operator T(Some<T> option) =>
        option.Value;

    public static implicit operator Some<T>(T value) =>
        new(value);

    public override string ToString() =>
        Value!.ToString()!;
}

[JsonConverter(typeof(OptionJsonConverter))]
public sealed record None<T> : Option<T>
{
    private None() { }

    public static None<T> Instance =>
        new();

    public override string ToString() =>
        string.Empty;
}

public static class Option
{
    public static Some<T> Some<T>(T value) =>
        Option<T>.Some(value);

    public static Option<T> None<T>() =>
        Option<T>.None;

    public static bool IsSome<T>(this Option<T> option) =>
        option is Some<T>;

    public static bool IsNone<T>(this Option<T> option) =>
        option is None<T>;

    public static T Or<T>(this Option<T> option, T fallback) =>
        option is Some<T> some ? some.Value : fallback;

    public static T Or<T>(this Option<T> option, Func<T> fallback) =>
        option is Some<T> some ? some.Value : fallback();

    public static Option<T> Otherwise<T>(this Option<T> option, Option<T> another) =>
        option is Some<T> some ? some : another;

    public static Option<T> Otherwise<T>(this Option<T> option, Func<Option<T>> another) =>
        option is Some<T> some ? some : another();

    public static T? OrNull<T>(this Option<T> option) where T : class =>
        option is Some<T> some ? some.Value : null;
}

public static class OptionCollection
{
    public static IEnumerable<T> Values<T>(this IEnumerable<Option<T>> options) =>
        options.OfType<Some<T>>().Select(some => some.Value);

    public static Option<T> FirstOrNone<T>(this IEnumerable<Option<T>> options) =>
        options.OfType<Some<T>>().DefaultIfEmpty(Option.None<T>()).First();

    public static Option<T> SingleOrNone<T>(this IEnumerable<Option<T>> options) =>
        options.OfType<Some<T>>().DefaultIfEmpty(Option.None<T>()).Single();

    public static Option<T> FirstOrNone<T>(this IEnumerable<T> options, Func<T, bool> filter) =>
        options.Where(filter).Select(Option<T>.Some).DefaultIfEmpty(Option.None<T>()).First();

    public static Option<T> SingleOrNone<T>(this IEnumerable<T> options, Func<T, bool> filter) =>
        options.Where(filter).Select(Option<T>.Some).DefaultIfEmpty(Option.None<T>()).Single();
}