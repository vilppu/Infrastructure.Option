using Infrastructure;
using System;

namespace OptionExample;

class Program
{
    public static void Main()
    {
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

        Print(Option.Some("Hello!")); // prints "Hello!"
        Print(Option.None<string>()); // does nothing
        Print(Option.None<string>().Otherwise(Option.Some("Hello!"))); // prints "Hello!"
        Print(Option.None<string>().Or(Option.None<string>())); // does nothing
        Console.WriteLine(Option.None<string>().Or("I am fallback value!")); // prints "I am fallback value!"
        Console.WriteLine(Option.None<string>().Or(Option.None<string>()).Or("I am another fallback value!")); // prints "I am another fallback value!"
        Console.WriteLine(string.Join("", options.Choose())); // prints "12345"
        Print(options.FirstOrNone()); // prints "1"
        Print(values.FirstOrNone(value => value == "4")); // prints "4"
        Print(values.SingleOrNone(value => value == "5")); // prints "5"
        Print(values.FirstOrNone(value => value == "6")); // does nothing
        Print(values.SingleOrNone(value => value == "6")); // does nothing


        var examples = new[] {
            Option.None<ExampleType>(),
            Option.Some(new ExampleType("1")),
            Option.None<ExampleType>(),
            Option.Some(new ExampleType("2")),
            Option.None<ExampleType>(),
            Option.Some(new ExampleType("3")),
            Option.None<ExampleType>(),
        };

        Print("[" + string.Join(", ", examples.Choose(entry => entry.ExampleProperty)) + "]"); // [ 1, 2, 3 ]
    }
    
    private static void Print(Option<string> option)
    {
        if (option is Some<string> some)
        {
            Console.WriteLine(some);
        }
    }
}

record ExampleType(string ExampleProperty);