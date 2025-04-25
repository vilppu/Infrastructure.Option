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
    public static Option<TResult> Choose<TSource, TResult>(this Option<TSource> option, Func<TSource, TResult> mapping) =>
        option switch
        {
            Some<TSource> some => mapping(some),
            _ => Option<TResult>.None
        };

    /// <summary>
    /// Choose underlying value and apply async mapping.
    /// </summary>
    public static async Task<Option<TResult>> Choose<TSource, TResult>(this Option<TSource> option, Func<TSource, Task<TResult>> mapping) =>
        option switch
        {
            Some<TSource> some => await mapping(some),
            _ => Option<TResult>.None
        };

    /// <summary>
    /// Choose underlying value and apply mapping.
    /// </summary>
    public static async Task<Option<TResult>> Choose<TSource, TResult>(this Task<Option<TSource>> source, Func<TSource, TResult> mapping) =>
        await source switch
        {
            Some<TSource> some => mapping(some),
            _ => Option<TResult>.None
        };

    /// <summary>
    /// Choose underlying value and apply async mapping.
    /// </summary>
    public static async Task<Option<TResult>> Choose<TSource, TResult>(this Task<Option<TSource>> source, Func<TSource, Task<TResult>> mapping) =>
        await source switch
        {
            Some<TSource> some => await mapping(some),
            _ => Option<TResult>.None
        };

    /// <summary>
    /// Choose underlying value and apply mapping to option.
    /// </summary>
    public static Option<TResult> Choose<TSource, TResult>(this Option<TSource> option, Func<TSource, Option<TResult>> mapping) =>
        option switch
        {
            Some<TSource> some => mapping(some),
            _ => Option<TResult>.None
        };

    /// <summary>
    /// Choose underlying value and apply async mapping to option.
    /// </summary>
    public static async Task<Option<TResult>> Choose<TSource, TResult>(this Option<TSource> option, Func<TSource, Task<Option<TResult>>> mapping) =>
        option switch
        {
            Some<TSource> some => await mapping(some),
            _ => Option<TResult>.None
        };

    /// <summary>
    /// Choose underlying value and apply mapping to option.
    /// </summary>
    public static async Task<Option<TResult>> Choose<TSource, TResult>(this Task<Option<TSource>> source, Func<TSource, Option<TResult>> mapping) =>
        await source switch
        {
            Some<TSource> some => mapping(some),
            _ => Option<TResult>.None
        };

    /// <summary>
    /// Choose underlying value and apply async mapping to option.
    /// </summary>
    public static async Task<Option<TResult>> Choose<TSource, TResult>(this Task<Option<TSource>> source, Func<TSource, Task<Option<TResult>>> mapping) =>
        await source switch
        {
            Some<TSource> some => await mapping(some),
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
    public static IEnumerable<TResult> Choose<TSource, TResult>(this IEnumerable<Option<TSource>> source, Func<TSource, TResult> mapping) =>
        source.OfType<Some<TSource>>().Select(some => mapping(some));

    /// <summary>
    /// Choose values by applying mapping to optional values.
    /// </summary>
    public static IEnumerable<TResult> Choose<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, Option<TResult>> mapping) =>
        source.Select(mapping).Choose();

    /// <summary>
    /// Choose values by applying async mapping to optional values.
    /// </summary>
    public static async Task<IEnumerable<TResult>> Choose<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, Task<Option<TResult>>> mapping) =>
        (await Task.WhenAll(source.Select(mapping))).Choose();

    /// <summary>
    /// Choose underlying values and apply async mapping to those.
    /// </summary>
    public static async Task<IEnumerable<TResult>> Choose<TSource, TResult>(this IEnumerable<Option<TSource>> source, Func<TSource, Task<TResult>> mapping) =>
        await Task.WhenAll(source.OfType<Some<TSource>>().Select(some => mapping(some)));

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