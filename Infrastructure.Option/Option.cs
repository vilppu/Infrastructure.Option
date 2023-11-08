using System.Linq;
using System.Collections.Generic;
using System;

namespace Infrastructure
{
    public abstract record Option<T>
    {
        public static Some<T> Some(T value) =>
            new(value);

        public static Option<T> None =>
            None<T>.Instance;

        public static implicit operator Option<T>(T value) =>
            Option.Some(value);
    }

    public sealed record Some<T>(T Value) : Option<T>
    {
        public static implicit operator T(Some<T> option) =>
            option.Value;

        public static implicit operator Some<T>(T value) =>
            new(value);

        public override string ToString() =>
            Value!.ToString()!;
    }

    public sealed record None<T> : Option<T>
    {
        private None() { }

        public static None<T> Instance =>
            new();

        public override string ToString() =>
            string.Empty;
    }

    public static class Option
    {
        public static Some<T> Some<T>(T value) =>
            Option<T>.Some(value);

        public static Option<T> None<T>() =>
            Option<T>.None;

        public static bool IsSome<T>(this Option<T> option) =>
            option is Some<T>;

        public static bool IsNone<T>(this Option<T> option) =>
            option is None<T>;

        public static T Or<T>(this Option<T> option, T fallback) =>
            option is Some<T> some ? some.Value : fallback;

        public static T Or<T>(this Option<T> option, Func<T> fallback) =>
            option is Some<T> some ? some.Value : fallback();

        public static Option<T> Otherwise<T>(this Option<T> option, Option<T> another) =>
            option is Some<T> some ? some : another;

        public static Option<T> Otherwise<T>(this Option<T> option, Func<Option<T>> another) =>
            option is Some<T> some ? some : another();
    }

    public static class Options
    {
        public static IEnumerable<T> Values<T>(this IEnumerable<Option<T>> options) =>
            options.OfType<Some<T>>().Select(some => some.Value);

        public static Option<T> FirstSomeOtherwiseNone<T>(this IEnumerable<Option<T>> options) =>
            options.FirstOrDefault(option => option is Some<T>) ?? Option.None<T>();
    }

    public static class OptionToNullable
    {
        public static T? OrNull<T>(this Option<T> option) where T : class =>
            option is Some<T> some ? some.Value : null;
    }
}
