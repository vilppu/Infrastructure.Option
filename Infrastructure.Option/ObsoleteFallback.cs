using System;

namespace Infrastructure;

public static class ObsoleteFallback
{
    [Obsolete($"Use {nameof(Fallback.Otherwise)} instead.")]
    public static T Or<T>(this Option<T> option, T fallback) =>
        option is Some<T> some ? some.Value : fallback;

    [Obsolete($"Use {nameof(Fallback.Otherwise)} instead.")]
    public static T Or<T>(this Option<T> option, Func<T> fallback) =>
        option is Some<T> some ? some.Value : fallback();

    [Obsolete($"Use {nameof(Fallback.Otherwise)} instead.")]
    public static Option<T> Or<T>(this Option<T> option, Option<T> another) =>
        option is Some<T> some ? some : another;
    
    [Obsolete($"Use {nameof(Fallback.Otherwise)} instead.")]
    public static Option<T> Or<T>(this Option<T> option, Func<Option<T>> another) =>
        option is Some<T> some ? some : another();
}