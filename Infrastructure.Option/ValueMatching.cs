using System;
using System.Threading.Tasks;

namespace Infrastructure;

public static class ValueMatching
{
    /// <summary>
    /// Check if given predicate holds for the value.
    /// </summary>
    public static bool Holds<TValue>(this Option<TValue> option, Func<TValue, bool> predicate) =>
        option is Some<TValue> some && predicate(some);

    /// <summary>
    /// Check if given async predicate holds for the value.
    /// </summary>
    public static async Task<bool> Holds<TValue>(this Option<TValue> option, Func<TValue, Task<bool>> predicate) =>
        option is Some<TValue> some && await predicate(some);

    /// <summary>
    /// Check if given predicate holds for the value.
    /// </summary>
    public static async Task<bool> Holds<TValue>(this Task<Option<TValue>> option, Func<TValue, bool> predicate) =>
        await option is Some<TValue> some && predicate(some);

    /// <summary>
    /// Check if given async predicate holds for the value.
    /// </summary>
    public static async Task<bool> Holds<TValue>(this Task<Option<TValue>> option, Func<TValue, Task<bool>> predicate) =>
        await option is Some<TValue> some && await predicate(some);
}