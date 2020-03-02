using System.Linq;
using System.Collections.Generic;

namespace Infrastructure
{
    public abstract class Option<T>
    {
        public static Some<T> Some(T value) =>
            new Some<T>(value);

        public static None<T> None { get; } =
            None<T>.Instance;

        public static implicit operator Option<T>(T value) =>
            Option.Some(value);
    }

    public sealed class Some<T> : Option<T>
    {
        public Some(T value) =>
            Value = value;

        public static implicit operator T(Some<T> option) =>
            option.Value;

        public static implicit operator Some<T>(T value) =>
            new Some<T>(value);

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case Some<T> another:
                    return Value.Equals(another.Value);

                case T another:
                    return Value.Equals(another);

                default:
                    return false;
            }
        }

        public override int GetHashCode() =>
            Value.GetHashCode();

        public override string ToString() =>
            Value.ToString();

        public T Value { get; }
    }

    public sealed class None<T> : Option<T>
    {
        private None()
        {
        }

        public static None<T> Instance { get; } =
            new None<T>();

        public override bool Equals(object obj) =>
            obj is None<T>;

        public override int GetHashCode() =>
            GetType().GetHashCode();

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
    }

    public static class Options
    {
        public static IEnumerable<T> Values<T>(this IEnumerable<Option<T>> options) =>
            options.OfType<Some<T>>().Select(some => some.Value);
    }
}
