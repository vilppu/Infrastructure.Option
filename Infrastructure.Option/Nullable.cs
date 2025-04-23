namespace Infrastructure;

public static class Nullable
{
    /// <summary>
    /// The optional value as nullable.
    /// </summary>
    public static T? OrNull<T>(this Option<T> option) where T : class =>
        (T?)option.ValueOrNull;
}