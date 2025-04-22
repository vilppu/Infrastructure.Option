namespace Infrastructure;

public static class Nullable
{
    /// <summary>
    /// The optional value as nullable.
    /// </summary>
    /// <typeparam name="T">Type wrapped as optional</typeparam>
    /// <param name="option">The value which is converted to nullable</param>
    /// <returns>The value or null if <paramref name="option"/> is <see cref="Option.None{T}"/>.</returns>
    public static T? OrNull<T>(this Option<T> option) where T : class =>
        (T?)option.ValueOrNull;
}