namespace Infrastructure;

public static class IsSomeOrNone
{
    /// <summary>
    /// Determine if <paramref name="option"/> is <see cref="Some{T}"/>.
    /// </summary>
    public static bool IsSome<T>(this Option<T> option) =>
        option is Some<T>;

    /// <summary>
    /// Determine if <paramref name="option"/> is <see cref="None{T}"/>.
    /// </summary>
    public static bool IsNone<T>(this Option<T> option) =>
        option is None<T>;
}