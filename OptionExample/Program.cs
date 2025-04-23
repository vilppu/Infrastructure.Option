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

        Console.WriteLine((Option<string>)Option.Some("Hello!")); // prints "Hello!"
        Console.WriteLine(Option.None<string>()); // prints ""
        Console.WriteLine((Option<string>)Option.None<string>().Otherwise(Option.Some("Hello!"))); // prints "Hello!"
        Console.WriteLine(Option.None<string>().Otherwise(Option.None<string>())); // prints ""
        Console.WriteLine(Option.None<string>().Otherwise("I am fallback value!")); // prints "I am fallback value!"
        Console.WriteLine(Option.None<string>().Otherwise(Option.None<string>()).Otherwise("I am another fallback value!")); // prints "I am another fallback value!"
        Console.WriteLine(string.Join("", options.Choose())); // prints "12345"
        Console.WriteLine(options.ChooseFirst()); // prints "1"
        Console.WriteLine(options.ChooseFirst(value => value == "4")); // prints "4"
        Console.WriteLine(options.ChooseSingle(value => value == "5")); // prints "5"
        Console.WriteLine(options.ChooseFirst(value => value == "6")); // prints ""
        Console.WriteLine(options.ChooseSingle(value => value == "6")); // prints ""

        Console.WriteLine(
            Option.Some("Hello")
            .Choose(value => $"{value} world")
            .Choose(value => $"{value}!")
        ); // prints "Hello world!" by chaining the Choose() functions.


        var examples = new[] {
            Option.None<ExampleType>(),
            Option.Some(new ExampleType("1")),
            Option.None<ExampleType>(),
            Option.Some(new ExampleType("2")),
            Option.None<ExampleType>(),
            Option.Some(new ExampleType("3")),
            Option.None<ExampleType>(),
        };

        Console.WriteLine((Option<string>)("[" + string.Join(", ", examples.Choose(entry => entry.ExampleProperty)) + "]")); // [ 1, 2, 3 ]

        var option = Option.Some("Example value");

        Console.WriteLine(option.Holds(example => example == "Example value")); // prints true
    }
}

record ExampleType(string ExampleProperty);