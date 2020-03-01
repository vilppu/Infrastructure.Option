[![Build Status](https://vilppu.visualstudio.com/Infrastructure.Option/_apis/build/status/vilppu.Infrastructure.Option?branchName=master)](https://vilppu.visualstudio.com/Infrastructure.Option/_build/latest?definitionId=1&branchName=master)

# Usage

Get nuget https://www.nuget.org/packages/Infrastructure.Option/

# Overview

`Option<T>` is a simple option type usable with C#

`Option.Some<T>` represents an available value and `Option.None<T>` is non-available value.

`Option` is a static class that can be used to create `Option<T>` instances.

For more information about option types see https://en.wikipedia.org/wiki/Option_type

# Examples

## Creating some values

```
var someString = Option.Some("Example value");
var someInteger = Option.Some(12345);
```

## Creating none values

```
var noneString = Option.None<string>();
var noneInteger = Option.None<int>();
```

## Pattern matching optional values

```
var option = Option.Some("Example value");
var value = option is Some<string> some ? some.Value : "Someting else";
```

## Fallback values using fluent syntax

```
Option<string> option = Option.None<string>();
var fallback = option.Or("Fallback value");
```

## Mapping collection values

```
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
var values = collection.Values();
```

## Sample console app

```
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

```
