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
    public abstract object? ValueOrNull { get; }

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

    public override object? ValueOrNull => Value;

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

    public override object? ValueOrNull => null;

    public override string ToString() =>
        string.Empty;
}

public static class Option
{
    /// <summary>
    /// Create an existing value.
    /// </summary>
    /// <typeparam name="T">Type wrapped as optional</typeparam>
    /// <param name="value">Value to be wrapped as optional</param>
    /// <returns>Value wrapped as optional</returns>
    public static Some<T> Some<T>(T value) =>
        Option<T>.Some(value);

    /// <summary>
    /// Create a non-existing value.
    /// </summary>
    /// <typeparam name="T">Type wrapped as optional</typeparam>
    /// <returns><see cref="None{T}"/></returns>
    public static Option<T> None<T>() =>
        Option<T>.None;

    /// <summary>
    /// Determine if <paramref name="option"/> is <see cref="Some{T}"/>.
    /// </summary>
    /// <typeparam name="T">Type wrapped as optional</typeparam>
    /// <param name="option">The optional value to be checked</param>
    /// <returns>true if <paramref name="option"/> is <see cref="Some{T}"/></returns>
    public static bool IsSome<T>(this Option<T> option) =>
        option is Some<T>;

    /// <summary>
    /// Determine if <paramref name="option"/> is <see cref="None{T}"/>.
    /// </summary>
    /// <typeparam name="T">Type wrapped as optional</typeparam>
    /// <param name="option">The optional value to be checked</param>
    /// <returns>true if <paramref name="option"/> is <see cref="None{T}"/></returns>
    public static bool IsNone<T>(this Option<T> option) =>
        option is None<T>;

    /// <summary>
    /// Choose the optional value or fallback value.
    /// </summary>
    /// <typeparam name="T">Type wrapped as optional</typeparam>
    /// <param name="option">The optional value which value is chosen</param>
    /// <param name="fallback">The fallback value to be resolved if <paramref name="option"/> is <see cref="None{T}"/></param>
    /// <returns>The value of <paramref name="option"/> or <paramref name="fallback"/> if option is <see cref="None{T}"/></returns>
    public static T Or<T>(this Option<T> option, T fallback) =>
        option is Some<T> some ? some.Value : fallback;

    /// <summary>
    /// Choose the optional value or fallback value.
    /// </summary>
    /// <typeparam name="T">Type wrapped as optional</typeparam>
    /// <param name="option">The optional value which value is chosen</param>
    /// <param name="fallback">The fallback value to be resolved if <paramref name="option"/> is <see cref="None{T}"/></param>
    /// <returns>The value of <paramref name="option"/> or <paramref name="fallback"/> if option is <see cref="None{T}"/></returns>
    public static T Or<T>(this Option<T> option, Func<T> fallback) =>
        option is Some<T> some ? some.Value : fallback();

    /// <summary>
    /// Choose the optional value or fallback value.
    /// </summary>
    /// <typeparam name="T">Type wrapped as optional</typeparam>
    /// <param name="option">The optional value which value is chosen</param>
    /// <param name="another">The fallback value to be resolved if <paramref name="option"/> is <see cref="None{T}"/></param>
    /// <returns>The value of <paramref name="option"/> or <paramref name="another"/> if option is <see cref="None{T}"/></returns>
    public static Option<T> Or<T>(this Option<T> option, Option<T> another) =>
        option is Some<T> some ? some : another;

    /// <summary>
    /// Choose the optional value or fallback value.
    /// </summary>
    /// <typeparam name="T">Type wrapped as optional</typeparam>
    /// <param name="option">The optional value which value is chosen</param>
    /// <param name="another">The fallback value to be resolved if <paramref name="option"/> is <see cref="None{T}"/></param>
    /// <returns>The value of <paramref name="option"/> or <paramref name="another"/> if option is <see cref="None{T}"/></returns>
    public static Option<T> Or<T>(this Option<T> option, Func<Option<T>> another) =>
        option is Some<T> some ? some : another();

    /// <summary>
    /// Choose the optional value or fallback value.
    /// </summary>
    /// <typeparam name="T">Type wrapped as optional</typeparam>
    /// <param name="option">The optional value which value is chosen</param>
    /// <param name="another">The fallback value to be resolved if <paramref name="option"/> is <see cref="None{T}"/></param>
    /// <returns>The value of <paramref name="option"/> or <paramref name="another"/> if option is <see cref="None{T}"/></returns>
    public static Option<T> Otherwise<T>(this Option<T> option, Option<T> another) =>
        option.Or(another);

    /// <summary>
    /// Choose the optional value or fallback value.
    /// </summary>
    /// <typeparam name="T">Type wrapped as optional</typeparam>
    /// <param name="option">The optional value which value is chosen</param>
    /// <param name="another">The fallback value to be resolved if <paramref name="option"/> is <see cref="None{T}"/></param>
    /// <returns>The value of <paramref name="option"/> or <paramref name="another"/> if option is <see cref="None{T}"/></returns>
    public static Option<T> Otherwise<T>(this Option<T> option, Func<Option<T>> another) =>
        option.Or(another);

    /// <summary>
    /// Choose a property of the optional value.
    /// </summary>
    /// <typeparam name="TOptional">Type wrapped as optional</typeparam>
    /// <typeparam name="TProperty">The property type to be chosen</typeparam>
    /// <param name="option">Optional value which property is chosen.</param>
    /// <param name="selector">The property selector.</param>
    /// <returns>The property value or <see cref="None{T}"/> if <paramref name="option"/> is <see cref="None{T}"/>.</returns>
    public static Option<TProperty> Choose<TOptional, TProperty>(this Option<TOptional> option, Func<TOptional, TProperty> selector) =>
        option switch 
        {
            Some<TOptional> some => selector(some),
            _ => Option<TProperty>.None
        };

    /// <summary>
    /// The optional value as nullable.
    /// </summary>
    /// <typeparam name="T">Type wrapped as optional</typeparam>
    /// <param name="option">The value which is converted to nullable</param>
    /// <returns>The value or null if <paramref name="option"/> is <see cref="None{T}"/>.</returns>
    public static T? OrNull<T>(this Option<T> option) where T : class =>
        (T?)option.ValueOrNull;
}

public static class OptionCollection
{
    /// <summary>
    /// Choose all existing values i.e. entries of type <see cref="Some{T}"/>.
    /// </summary>
    /// <typeparam name="T">Type wrapped as optional</typeparam>
    /// <param name="options">Optional values to choose from</param>
    /// <returns>All existing values i.e. entries of type <see cref="Some{T}"/></returns>
    public static IEnumerable<T> Choose<T>(this IEnumerable<Option<T>> options) =>
        options.OfType<Some<T>>().Select(some => some.Value);

    /// <summary>
    /// Choose all existing values i.e. entries of type <see cref="Some{T}"/>.
    ///
    /// This is an alias for <see cref="Choose{T}"/>
    /// </summary>
    /// <typeparam name="T">Type wrapped as optional</typeparam>
    /// <param name="options">Optional values to choose from</param>
    /// <returns>All existing values i.e. entries of type <see cref="Some{T}"/></returns>
    public static IEnumerable<T> Values<T>(this IEnumerable<Option<T>> options) =>
        options.Choose();

    /// <summary>
    /// Choose first existing value i.e. entry of type <see cref="Some{T}"/>.
    /// </summary>
    /// <typeparam name="T">Type wrapped as optional</typeparam>
    /// <param name="options">Optional values to choose from</param>
    /// <returns>First existing value i.e. entry of type <see cref="Some{T}"/> or <see cref="None{T}"/> if all values are <see cref="None{T}"/></returns>
    public static Option<T> FirstOrNone<T>(this IEnumerable<Option<T>> options) =>
        options.OfType<Some<T>>().DefaultIfEmpty(Option.None<T>()).First();

    /// <summary>
    /// Choose single existing value i.e. entry of type <see cref="Some{T}"/>.
    /// </summary>
    /// <typeparam name="T">Type wrapped as optional</typeparam>
    /// <param name="options">Optional values to choose from</param>
    /// <returns>Single existing value i.e. entry of type <see cref="Some{T}"/> or <see cref="None{T}"/> if all values are <see cref="None{T}"/></returns>
    /// <remarks>Throws an <see cref="InvalidOperationException"/> if more than value exists </remarks>
    public static Option<T> SingleOrNone<T>(this IEnumerable<Option<T>> options) =>
        options.OfType<Some<T>>().DefaultIfEmpty(Option.None<T>()).Single();

    /// <summary>
    /// Choose first existing value i.e. entry of type <see cref="Some{T}"/> that matches the filter.
    /// </summary>
    /// <typeparam name="T">Type wrapped as optional</typeparam>
    /// <param name="options">Optional values to choose from</param>
    /// <param name="filter">The filter to be applied</param>
    /// <returns>First existing value i.e. entry of type <see cref="Some{T}"/> that matches the filter or <see cref="None{T}"/> if all values are <see cref="None{T}"/></returns>
    public static Option<T> FirstOrNone<T>(this IEnumerable<T> options, Func<T, bool> filter) =>
        options.Where(filter).Select(Option<T>.Some).DefaultIfEmpty(Option.None<T>()).First();

    /// <summary>
    /// Choose single existing value i.e. entry of type <see cref="Some{T}"/> that matches the filter.
    /// </summary>
    /// <typeparam name="T">Type wrapped as optional</typeparam>
    /// <param name="options">Optional values to choose from</param>
    /// <param name="filter">The filter to be applied</param>
    /// <returns>Single existing value i.e. entry of type <see cref="Some{T}"/> that matches the filter or <see cref="None{T}"/> if all values are <see cref="None{T}"/></returns>
    /// <remarks>Throws an <see cref="InvalidOperationException"/> if more than value exists </remarks>
    public static Option<T> SingleOrNone<T>(this IEnumerable<T> options, Func<T, bool> filter) =>
        options.Where(filter).Select(Option<T>.Some).DefaultIfEmpty(Option.None<T>()).Single();

    /// <summary>
    /// Choose property values
    /// </summary>
    /// <typeparam name="TOptional">Type wrapped as optional</typeparam>
    /// <typeparam name="TProperty">Type of the chosen property</typeparam>
    /// <param name="options">Optional values which properties are chosen</param>
    /// <param name="selector">The property selector.</param>
    /// <returns>Property values of all entries that exist i.e. that are of type <see cref="Some{T}"/>.</returns>
    public static IEnumerable<TProperty> Choose<TOptional, TProperty>(this IEnumerable<Option<TOptional>> options, Func<TOptional, TProperty> selector) =>
        options.OfType<Some<TOptional>>().Select(some => selector(some));
}