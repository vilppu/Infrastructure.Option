using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure;

/// <summary>
/// Choose singular value from sequence as optional.
/// </summary>
public static class ChooseSingularValueFromSequence
{
    /// <summary>
    /// Select first value from sequence.
    /// </summary>
    /// <returns>First value from sequence or <see cref="None{TValue}"/>> no values exist.</returns>
    public static Option<TValue> FirstOrNone<TValue>(this IEnumerable<TValue> sequence) =>
        sequence
            .Select(Option.Some)
            .DefaultIfEmpty(Option.None<TValue>())
            .First();

    /// <summary>
    /// Select Single value from sequence.
    /// </summary>
    /// <returns>Single underlying value or <see cref="None{TValue}"/>> no values exist.</returns>
    /// <returns>Single value from sequence or <see cref="None{TValue}"/>> no values exist.</returns>
    public static Option<TValue> SingleOrNone<TValue>(this IEnumerable<TValue> sequence) =>
        sequence
            .Select(Option.Some)
            .DefaultIfEmpty(Option.None<TValue>())
            .Single();

    /// <summary>
    /// Select first value matching the filter from sequence.
    /// </summary>
    /// <returns>First underlying value matching the filter or <see cref="None{TValue}"/>> no values exist.</returns>
    public static Option<TValue> FirstOrNone<TValue>(this IEnumerable<TValue> sequence, Func<TValue, bool> filter) =>
        sequence
            .Where(filter)
            .Select(Option<TValue>.Some)
            .DefaultIfEmpty(Option.None<TValue>())
            .First();

    /// <summary>
    /// Select Single value matching the filter from sequence.
    /// </summary>
    /// <returns>Single underlying value matching the filter or <see cref="None{TValue}"/>> no values exist.</returns>
    /// <remarks>Throws an <see cref="InvalidOperationException"/> if more than one value matching the filter exists.</remarks>
    public static Option<TValue> SingleOrNone<TValue>(this IEnumerable<TValue> sequence, Func<TValue, bool> filter) =>
        sequence
            .Where(filter)
            .Select(Option<TValue>.Some)
            .DefaultIfEmpty(Option.None<TValue>())
            .Single();
}