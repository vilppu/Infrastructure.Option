using Infrastructure;
using System;

namespace OptionExample
{
    class Program
    {
        static void Main()
        {
            var some = Option.Some("Hello!");
            var none = Option.None<string>();
            var fallback = none.Or("I am fallback value!");
            var collection = new Option<string>[]
                {
                    Option.None<string>(),
                    Option.Some("1"),
                    Option.None<string>(),
                    Option.None<string>(),
                    Option.Some("2"),
                    Option.Some("3"),
                    Option.Some("4"),
                    Option.None<string>(),
                    Option.Some("5"),
                    Option.None<string>(),
                };

            Print(some); // prints "Hello!"
            Print(none); // does nothing
            Console.WriteLine(fallback); // prints "I am fallback value!"
            Console.WriteLine(string.Join("", collection.Values())); // prints 12345
        }

        private static void Print(Option<string> option)
        {
            if (option is Some<string> some)
            {
                Console.WriteLine(some);
            }
        }
    }
}
