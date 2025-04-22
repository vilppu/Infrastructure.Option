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
}