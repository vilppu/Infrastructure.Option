namespace Infrastructure;

public static class IsSomeOrNone
{
    /// <summary>
    /// Determine if <paramref name="option"/> is <see cref="option.Some{T}"/>.
    /// </summary>
    /// <typeparam name="T">Type wrapped as optional</typeparam>
    /// <param name="option">The optional value to be checked</param>
    /// <returns>true if <paramref name="option"/> is <see cref="Option.Some{T}"/></returns>
    public static bool IsSome<T>(this Option<T> option) =>
        option is Some<T>;

    /// <summary>
    /// Determine if <paramref name="option"/> is <see cref="option.None{T}"/>.
    /// </summary>
    /// <typeparam name="T">Type wrapped as optional</typeparam>
    /// <param name="option">The optional value to be checked</param>
    /// <returns>true if <paramref name="option"/> is <see cref="Option.None{T}"/></returns>
    public static bool IsNone<T>(this Option<T> option) =>
        option is None<T>;
}