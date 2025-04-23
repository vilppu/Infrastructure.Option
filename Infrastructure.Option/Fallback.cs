using System;
using System.Threading.Tasks;

namespace Infrastructure;

public static class Fallback
{
    /// <summary>
    /// Choose the optional value or fallback value.
    /// </summary>
    public static T Otherwise<T>(this Option<T> option, T fallback) =>
        option is Some<T> some ? some.Value : fallback;

    /// <summary>
    /// Choose the optional value or fallback value.
    /// </summary>
    public static async Task<T> Otherwise<T>(this Task<Option<T>> option, T fallback) =>
        await option is Some<T> some ? some.Value : fallback;

    /// <summary>
    /// Choose the optional value or fallback value.
    /// </summary>
    public static T Otherwise<T>(this Option<T> option, Func<T> fallback) =>
        option is Some<T> some ? some.Value : fallback();

    /// <summary>
    /// Choose the optional value or fallback value.
    /// </summary>
    public static async Task<T> Otherwise<T>(this Task<Option<T>> option, Func<T> fallback) =>
       await option is Some<T> some ? some.Value : fallback();

    /// <summary>
    /// Choose the optional value or async fallback value.
    /// </summary>
    public static async Task<T> Otherwise<T>(this Option<T> option, Func<Task<T>> fallback) =>
        option is Some<T> some ? some.Value : await fallback();

    /// <summary>
    /// Choose the optional value or async fallback value.
    /// </summary>
    public static async Task<T> Otherwise<T>(this Task<Option<T>> option, Func<Task<T>> fallback) =>
        await option is Some<T> some ? some.Value : await fallback();

    /// <summary>
    /// Choose the optional value or fallback value.
    /// </summary>
    public static Option<T> Otherwise<T>(this Option<T> option, Option<T> another) =>
        option is Some<T> some ? some : another;

    /// <summary>
    /// Choose the optional value or fallback value.
    /// </summary>
    public static async Task<Option<T>> Otherwise<T>(this Task<Option<T>> option, Option<T> another) =>
        await option is Some<T> some ? some : another;

    /// <summary>
    /// Choose the optional value or fallback value.
    /// </summary>
    public static Option<T> Otherwise<T>(this Option<T> option, Func<Option<T>> another) =>
        option is Some<T> some ? some : another();

    /// <summary>
    /// Choose the optional value or fallback value.
    /// </summary>
    public static async Task<Option<T>> Otherwise<T>(this Task<Option<T>> option, Func<Option<T>> another) =>
        await option is Some<T> some ? some : another();

    /// <summary>
    /// Choose the optional value or async fallback value.
    /// </summary>
    public static async Task<Option<T>> Otherwise<T>(this Option<T> option, Func<Task<Option<T>>> another) =>
        option is Some<T> some ? some : await another();

    /// <summary>
    /// Choose the optional value or async fallback value.
    /// </summary>
    public static async Task<Option<T>> Otherwise<T>(this Task<Option<T>> option, Func<Task<Option<T>>> another) =>
        await option is Some<T> some ? some : await another();
}