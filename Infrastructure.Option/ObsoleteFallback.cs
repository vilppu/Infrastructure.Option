using System;

namespace Infrastructure;

/// <summary>
/// Obsolete fallback implementation for backwards compatibility.
/// </summary>
[Obsolete($"Use {nameof(Fallback)} instead.")]
public static class ObsoleteFallback
{
    /// <summary>
    /// Defines a fallback behaviour
    /// </summary>
    [Obsolete($"Use {nameof(Fallback.Otherwise)} instead.")]
    public static T Or<T>(this Option<T> option, T fallback) =>
        option is Some<T> some ? some.Value : fallback;

    /// <summary>
    /// Defines a fallback behaviour
    /// </summary>
    [Obsolete($"Use {nameof(Fallback.Otherwise)} instead.")]
    public static T Or<T>(this Option<T> option, Func<T> fallback) =>
        option is Some<T> some ? some.Value : fallback();

    /// <summary>
    /// Defines a fallback behaviour
    /// </summary>
    [Obsolete($"Use {nameof(Fallback.Otherwise)} instead.")]
    public static Option<T> Or<T>(this Option<T> option, Option<T> another) =>
        option is Some<T> some ? some : another;

    /// <summary>
    /// Defines a fallback behaviour
    /// </summary>
    [Obsolete($"Use {nameof(Fallback.Otherwise)} instead.")]
    public static Option<T> Or<T>(this Option<T> option, Func<Option<T>> another) =>
        option is Some<T> some ? some : another();
}