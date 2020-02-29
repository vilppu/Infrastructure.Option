namespace Arado
{
    public abstract class Option<T>
    {
    }

    public sealed class Some<T> : Option<T>
    {
        public Some(T value) => Value = value;

        public T Value { get; }

        public static implicit operator T(Some<T> option) => option.Value;

        public static implicit operator Some<T>(T value) => new Some<T>(value);

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

        public override int GetHashCode() => Value.GetHashCode();
    }

    public sealed class None<T> : Option<T>
    {
        private None()
        {
        }

        public static None<T> Instance { get; } = new None<T>();

        public override bool Equals(object obj) => obj is None<T>;

        public override int GetHashCode() => 0;
    }

    public static class Option
    {
        public static Some<T> Some<T>(T value) => new Some<T>(value);
        public static None<T> None<T>() => Arado.None<T>.Instance;
    }
}
