using System;
using System.Collections.Generic;

namespace Infrastructure;

public static class ObsoleteValues
{
    [Obsolete($"Use {nameof(Infrastructure.ChooseValues.Choose)} instead.")]
    public static IEnumerable<TValue> Values<TValue>(this IEnumerable<Option<TValue>> options) =>
        options.Choose();
}