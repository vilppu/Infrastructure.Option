namespace Infrastructure;

/// <summary>
/// Fallback to null or default when <see cref="None{T}"/> is encountered.
/// </summary>
public static class DefaultValue
{
    /// <summary>
    /// The optional value as nullable.
    /// </summary>
    public static T? OrNull<T>(this Option<T> option) where T : class =>
        option is Some<T> some ? some.Value : null;

    /// <summary>
    /// The optional value as .
    /// </summary>
    public static T? OrNullValue<T>(this Option<T> option) where T : struct =>
        option is Some<T> some ? some.Value : null;
}