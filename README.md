[![Build Status](https://vilppu.visualstudio.com/Arado.Option/_apis/build/status/vilppu.Arado.Option?branchName=master)](https://vilppu.visualstudio.com/Arado.Option/_build/latest?definitionId=1&branchName=master)

# Usage

Get nuget https://www.nuget.org/packages/Arado.Option/

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

## Patternt matching optional values

```
var option = Option.Some("Example value");
var value = option is Some<string> some ? some.Value : "Someting else";
```

## Sample console app

```
using Arado;
using System;

namespace OptionExample
{
    class Program
    {
        static void Main()
        {
            var some = Option.Some("Hello!");
            var none = Option.None<string>();

            Print(some); // prints "Hello!"
            Print(none); // does nothing
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
