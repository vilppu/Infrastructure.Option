using Infrastructure;
using System;
using System.Diagnostics.CodeAnalysis;

namespace OptionExample
{
    class Program
    {
        static void Main()
        {
            var some = Option.Some("Hello!");
            var none = Option.None<string>();
            var chosenSome = none.Otherwise(some);
            var chosenNone = none.Otherwise(none);
            var fallback = none.Or("I am fallback value!");
            var anotherFallback = none.Otherwise(none).Or("I am another fallback value!");
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
            var firstSome = collection.FirstSomeOtherwiseNone();

            Print(some); // prints "Hello!"
            Print(none); // does nothing
            Print(chosenSome); // prints "Hello!"
            Print(chosenNone); // does nothing
            Console.WriteLine(fallback); // prints "I am fallback value!"
            Console.WriteLine(anotherFallback); // prints "I am another fallback value!"
            Console.WriteLine(string.Join("", collection.Values())); // prints !12345"
            Print(firstSome); // Prints "1"
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
