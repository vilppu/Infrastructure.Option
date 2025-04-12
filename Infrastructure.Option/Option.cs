using System.Linq;
using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;

namespace Infrastructure;

/// <summary>
/// Optional value that is either something <see cref="Some">something</see> or <see cref="None">nothing</see>.
/// </summary>
/// <typeparam name="T"></typeparam>
[JsonConverter(typeof(OptionJsonConverter))]
public abstract record Option<T>
{
    /// <summary>
    /// Indicator intended for serializers to deduce if is value is <see cref="Some">something</see> or <see cref="None">nothing</see>.
    /// </summary>
    public abstract bool IsSome { get; }

    /// <summary>
    /// Value wrapped as an optional.
    /// </summary>
    /// <param name="value">The value to be wrapped as an optional</param>
    /// <returns>The value wrapped as an optional</returns>
    public static Some<T> Some(T value) =>
        new(value);

    /// <summary>
    /// Non existent value.
    /// </summary>
    public static Option<T> None =>
        None<T>.Instance;

    public static implicit operator Option<T>(T? value) =>
        value is null
            ? None
            : Option.Some(value);
}

/// <summary>
/// Value that exists i.e. is something.
/// </summary>
/// <typeparam name="T">The type of the  wrapped value.</typeparam>
/// <param name="Value">The type of the  wrapped value.</param>
[JsonConverter(typeof(OptionJsonConverter))]
public sealed record Some<T>(T Value) : Option<T>
{
    public static implicit operator T(Some<T> option) =>
        option.Value;

    public static implicit operator Some<T>(T value) =>
        new(value);

    public override bool IsSome => true;

    public override string ToString() =>
        Value!.ToString()!;
}
/// <summary>
/// Value that does not exist i.e. is nothing.
/// </summary>
/// <typeparam name="T">The type of the non-existent value.</typeparam>
[JsonConverter(typeof(OptionJsonConverter))]
public sealed record None<T> : Option<T>
{
    private None() { }

    /// <summary>
    /// Singleton instance.
    /// </summary>
    public static None<T> Instance =>
        new();

    public override bool IsSome => true;

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
    
    public static Option<T> Or<T>(this Option<T> option, Option<T> another) =>
        option is Some<T> some ? some : another;

    public static Option<T> Or<T>(this Option<T> option, Func<Option<T>> another) =>
        option is Some<T> some ? some : another();

    public static Option<T> Otherwise<T>(this Option<T> option, Option<T> another) =>
        option.Or(another);

    public static Option<T> Otherwise<T>(this Option<T> option, Func<Option<T>> another) =>
        option.Or(another);

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