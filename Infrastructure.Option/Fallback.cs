using System;

namespace Infrastructure;

public static class Fallback
{
    /// <summary>
    /// Choose the optional value or fallback value.
    /// </summary>
    /// <typeparam name="T">Type wrapped as optional</typeparam>
    /// <param name="option">The optional value which value is chosen</param>
    /// <param name="fallback">The fallback value to be resolved if <paramref name="option"/> is <see cref="Option.None{T}"/></param>
    /// <returns>The value of <paramref name="option"/> or <paramref name="fallback"/> if option is <see cref="Option.None{T}"/></returns>
    public static T Otherwise<T>(this Option<T> option, T fallback) =>
        option is Some<T> some ? some.Value : fallback;

    /// <summary>
    /// Choose the optional value or fallback value.
    /// </summary>
    /// <typeparam name="T">Type wrapped as optional</typeparam>
    /// <param name="option">The optional value which value is chosen</param>
    /// <param name="fallback">The fallback value to be resolved if <paramref name="option"/> is <see cref="Option.None{T}"/></param>
    /// <returns>The value of <paramref name="option"/> or <paramref name="fallback"/> if option is <see cref="Option.None{T}"/></returns>
    public static T Otherwise<T>(this Option<T> option, Func<T> fallback) =>
        option is Some<T> some ? some.Value : fallback();

    /// <summary>
    /// Choose the optional value or fallback value.
    /// </summary>
    /// <typeparam name="T">Type wrapped as optional</typeparam>
    /// <param name="option">The optional value which value is chosen</param>
    /// <param name="another">The fallback value to be resolved if <paramref name="option"/> is <see cref="Option.None{T}"/></param>
    /// <returns>The value of <paramref name="option"/> or <paramref name="another"/> if option is <see cref="Option.None{T}"/></returns>
    public static Option<T> Otherwise<T>(this Option<T> option, Option<T> another) =>
        option is Some<T> some ? some : another;

    /// <summary>
    /// Choose the optional value or fallback value.
    /// </summary>
    /// <typeparam name="T">Type wrapped as optional</typeparam>
    /// <param name="option">The optional value which value is chosen</param>
    /// <param name="another">The fallback value to be resolved if <paramref name="option"/> is <see cref="Option.None{T}"/></param>
    /// <returns>The value of <paramref name="option"/> or <paramref name="another"/> if option is <see cref="Option.None{T}"/></returns>
    public static Option<T> Otherwise<T>(this Option<T> option, Func<Option<T>> another) =>
        option is Some<T> some ? some : another();
}