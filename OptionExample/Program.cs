using Infrastructure;
using System;

namespace OptionExample;

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
        var options = new[]
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
        var values = new[]
        {
            "1",
            "2",
            "3",
            "4",
            "5",
        };
        var firstSome = options.FirstOrNone();
        var firstMatch = values.FirstOrNone(value => value == "4");
        var singleMatch = values.SingleOrNone(value => value == "5");
        var noMatch = values.FirstOrNone(value => value == "6");
        var noSingleMatch = values.SingleOrNone(value => value == "6");

        Print(some); // prints "Hello!"
        Print(none); // does nothing
        Print(chosenSome); // prints "Hello!"
        Print(chosenNone); // does nothing
        Console.WriteLine(fallback); // prints "I am fallback value!"
        Console.WriteLine(anotherFallback); // prints "I am another fallback value!"
        Console.WriteLine(string.Join("", options.Values())); // prints !12345"
        Print(firstSome); // prints "1"
        Print(firstMatch); // prints "4"
        Print(singleMatch); // prints "5"
        Print(noMatch); // does nothing
        Print(noSingleMatch); // does nothing
    }

    private static void Print(Option<string> option)
    {
        if (option is Some<string> some)
        {
            Console.WriteLine(some);
        }
    }
}