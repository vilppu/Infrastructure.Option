using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure;

/// <summary>
/// Choose underlying option value(s)
/// </summary>
public static class ChooseValues
{
    /// <summary>
    /// Unwrap underlying option.
    /// </summary>
    public static Option<TValue> Choose<TValue>(this Option<Option<TValue>> option) =>
        option switch
        {
            Some<Option<TValue>> some => some.Value,
            _ => Option<TValue>.None
        };

    /// <summary>
    /// Unwrap underlying option.
    /// </summary>
    public static Option<TValue> Choose<TValue>(this Some<Option<TValue>> option) =>
        option.Value;

    /// <summary>
    /// Choose underlying value and apply mapping.
    /// </summary>
    public static Option<TResult> Choose<TValue, TResult>(this Option<TValue> option, Func<TValue, TResult> mapping) =>
        option switch
        {
            Some<TValue> some => mapping(some),
            _ => Option<TResult>.None
        };

    /// <summary>
    /// Choose underlying value and apply async mapping.
    /// </summary>
    public static async Task<Option<TResult>> Choose<TValue, TResult>(this Option<TValue> option, Func<TValue, Task<TResult>> mapping) =>
        option switch
        {
            Some<TValue> some => await mapping(some),
            _ => Option<TResult>.None
        };

    /// <summary>
    /// Choose underlying value and apply mapping.
    /// </summary>
    public static async Task<Option<TResult>> Choose<TValue, TResult>(this Task<Option<TValue>> option, Func<TValue, TResult> mapping) =>
        await option switch
        {
            Some<TValue> some => mapping(some),
            _ => Option<TResult>.None
        };

    /// <summary>
    /// Choose underlying value and apply async mapping.
    /// </summary>
    public static async Task<Option<TResult>> Choose<TValue, TResult>(this Task<Option<TValue>> option, Func<TValue, Task<TResult>> mapping) =>
        await option switch
        {
            Some<TValue> some => await mapping(some),
            _ => Option<TResult>.None
        };

    /// <summary>
    /// Choose underlying value and apply mapping to option.
    /// </summary>
    public static Option<TResult> Choose<TValue, TResult>(this Option<TValue> option, Func<TValue, Option<TResult>> mapping) =>
        option switch
        {
            Some<TValue> some => mapping(some),
            _ => Option<TResult>.None
        };

    /// <summary>
    /// Choose underlying value and apply async mapping to option.
    /// </summary>
    public static async Task<Option<TResult>> Choose<TValue, TResult>(this Option<TValue> option, Func<TValue, Task<Option<TResult>>> mapping) =>
        option switch
        {
            Some<TValue> some => await mapping(some),
            _ => Option<TResult>.None
        };

    /// <summary>
    /// Choose underlying value and apply mapping to option.
    /// </summary>
    public static async Task<Option<TResult>> Choose<TValue, TResult>(this Task<Option<TValue>> option, Func<TValue, Option<TResult>> mapping) =>
        await option switch
        {
            Some<TValue> some => mapping(some),
            _ => Option<TResult>.None
        };

    /// <summary>
    /// Choose underlying value and apply async mapping to option.
    /// </summary>
    public static async Task<Option<TResult>> Choose<TValue, TResult>(this Task<Option<TValue>> option, Func<TValue, Task<Option<TResult>>> mapping) =>
        await option switch
        {
            Some<TValue> some => await mapping(some),
            _ => Option<TResult>.None
        };

    /// <summary>
    /// Choose underlying values.
    /// </summary>
    public static IEnumerable<TValue> Choose<TValue>(this IEnumerable<Option<TValue>> options) =>
        options.OfType<Some<TValue>>().Select(some => some.Value);

    /// <summary>
    /// Choose underlying values matching the filter.
    /// </summary>
    public static IEnumerable<TValue> Choose<TValue>(this IEnumerable<Option<TValue>> options, Func<TValue, bool> filter) =>
        options.Choose().Where(filter);

    /// <summary>
    /// Choose underlying values and apply mapping to those.
    /// </summary>
    public static IEnumerable<TResult> Choose<TValue, TResult>(this IEnumerable<Option<TValue>> options, Func<TValue, TResult> mapping) =>
        options.OfType<Some<TValue>>().Select(some => mapping(some));

    /// <summary>
    /// Choose underlying values and apply async mapping to those.
    /// </summary>
    public static async Task<IEnumerable<TResult>> Choose<TValue, TResult>(this IEnumerable<Option<TValue>> options, Func<TValue, Task<TResult>> mapping) =>
        await Task.WhenAll(options.OfType<Some<TValue>>().Select(some => mapping(some)));

    /// <summary>
    /// Choose first underlying value.
    /// </summary>
    /// <returns>First underlying value or <see cref="None{TValue}"/>> no values exist.</returns>
    public static Option<TValue> ChooseFirst<TValue>(this IEnumerable<Option<TValue>> options) =>
        options.OfType<Some<TValue>>().DefaultIfEmpty(Option.None<TValue>()).First();

    /// <summary>
    /// Choose single underlying value.
    /// </summary>
    /// <returns>Single underlying value or <see cref="None{TValue}"/>> no values exist.</returns>
    /// <remarks>Throws an <see cref="InvalidOperationException"/> if more than one value exists.</remarks>
    public static Option<TValue> ChooseSingle<TValue>(this IEnumerable<Option<TValue>> options) =>
        options.OfType<Some<TValue>>().DefaultIfEmpty(Option.None<TValue>()).Single();

    /// <summary>
    /// Choose first underlying value matching the filter.
    /// </summary>
    /// <returns>First underlying value matching the filter or <see cref="None{TValue}"/>> no values exist.</returns>
    public static Option<TValue> ChooseFirst<TValue>(this IEnumerable<Option<TValue>> options, Func<TValue, bool> filter) =>
        options.Choose().Where(filter).Select(Option<TValue>.Some).DefaultIfEmpty(Option.None<TValue>()).First();

    /// <summary>
    /// Choose single underlying value matching the filter.
    /// </summary>
    /// <returns>Single underlying value matching the filter or <see cref="None{TValue}"/>> no values exist.</returns>
    /// <remarks>Throws an <see cref="InvalidOperationException"/> if more than one value matching the filter exists.</remarks>
    public static Option<TValue> ChooseSingle<TValue>(this IEnumerable<Option<TValue>> options, Func<TValue, bool> filter) =>
        options.Choose().Where(filter).Select(Option<TValue>.Some).DefaultIfEmpty(Option.None<TValue>()).Single();
}