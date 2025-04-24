using System;
using System.Collections.Generic;

namespace Infrastructure;

/// <summary>
/// Obsolete choose implementation for backwards compatibility.
/// </summary>
[Obsolete($"Use {nameof(ChooseValues)} instead.")]
public static class ObsoleteValues
{
    /// <summary>
    /// Choose underlying values
    /// </summary>
    [Obsolete($"Use {nameof(ChooseValues.Choose)} instead.")]
    public static IEnumerable<TValue> Values<TValue>(this IEnumerable<Option<TValue>> options) =>
        options.Choose();
}