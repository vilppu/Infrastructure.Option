[![Build Status](https://vilppu.visualstudio.com/Infrastructure.Option/_apis/build/status/vilppu.Infrastructure.Option?branchName=main)](https://vilppu.visualstudio.com/Infrastructure.Option/_build/latest?definitionId=1&branchName=main)

# Usage

Get the NuGet package: [Infrastructure.Option on NuGet](https://www.nuget.org/packages/Infrastructure.Option/)

# Overview

The purpose of the `Infrastructure.Option` is to help you write code that is easier to read and understand.

The `Infrastructure.Option` makes it explicit when a value might be missing, but it stays out of your sight when irrelevant.

- `Option<T>` presents the situation when you don't know if the value is present or not.
- `Option.Some<T>` tells that the value of type `T` is present.
- `Option.None<T>` tells that the value of type `T` is not present.

`Option.Some<T>` behaves like the object of type `T`, and you can, e.g., pass it directly to a method accepting a parameter of type `T`.

`Option.None<T>` means that the value is not present so you cannot even accidentally try to access it.

The `Option` provides fluent access to the underlying optional value with `Choose()` and `Otherwise()`.

- `Choose()` applied to a single object selects the chosen property or returns null.
- `Choose()` applied to a collection selects the underlying values, i.e., those of type `Some<T>`.
- `Otherwise()` defines fallback behavior when encountering `None<T>`.

For more information about option types, see [Option type on Wikipedia](https://en.wikipedia.org/wiki/Option_type).

## JSON Serialization

`Infrastructure.Option` supports JSON serialization using `System.Text.Json` without requiring any additional dependencies.

The `Option<T>` type is serialized as an object with a single `ValueOrNull` property.

The OpenAPI documentation support is also provided without any additional dependencies.

For example, `Option.Some("Hello!")` is serialized as:

```json
{ "valueOrNull": "Hello!" }
```

## ToString()

`ToString()` called on `Option.Some` returns the result of the underlying object's `ToString()`.

`ToString()` called on `Option.None` returns an empty string.

# Examples

## Basic usage

```csharp
using Infrastructure;
using System;

void Print(string value) => Console.WriteLine(value);

var something = Option.Some("Something");
var nothing = Option<string>.None;

Print(something); // something is implicitly cast to string.
// Print(nothing); // This does not compile
```

## Creating `Option`s

```csharp
var some = Option.Some("Example value");
var none = Option.None<string>();
```

## Pattern matching

```csharp
using Infrastructure;
using System;

var option = Option.Some("Example value");

var value = option switch
{
    { Value: {} some } => some,
    _ => "Something else"
};

Console.WriteLine(value); // Prints: Example value
```

## `Choose()` underlying value

```csharp
using Infrastructure;
using System;

Option<Country> optionalCountry = new Country("Finland");

var nameOfTheCountry = optionalCountry.Choose(country => country.Name); // nameOfTheCountry is of type Option<string>

Console.WriteLine(nameOfTheCountry); // Prints: Finland

record Country(string Name);
```


## Fallback with `Otherwise()`

```csharp
var option = Option.None<string>();

var result = option.Otherwise("Something else"); // result == "Something else"
```

## `Choose()` underlying values from collections

```csharp
var collection = new Option<string>[] {
    Option.None<string>(),
    Option.Some("1"),
    Option.None<string>(),
    Option.Some("2"),
    Option.None<string>(),
    Option.Some("3"),
    Option.None<string>(),
};

var values = collection.Choose(); // values == [ "1", "2", "3" ]
var first = collection.ChooseFirst(); // first == "1"
```

## `Choose()` underlying values from collections with mapping

```csharp
record ExampleType(string ExampleProperty);

var collection = new[] {
    Option.None<ExampleType>(),
    Option.Some(new ExampleType("1")),
    Option.None<ExampleType>(),
    Option.Some(new ExampleType("2")),
    Option.None<ExampleType>(),
    Option.Some(new ExampleType("3")),
    Option.None<ExampleType>(),
};

var values = collection.Choose(entry => entry.ExampleProperty); // values == [ "1", "2", "3" ]
var chosen = await collection.Choose(async entry => await Task.FromResult(entry.ExampleProperty)); // chosen == [ "1", "2", "3" ]
```

## Pick singular value from collections

```csharp
var collection = new Option<string>[] {
    Option.Some("1"),
    Option.Some("2"),
    Option.Some("3")
};

var first = collection.FirstOrNone(); // first == "1"
var firstMatch = collection.FirstOrNone(element => element == "2"); // firstMatch == "2"

var only = collection.SingleOrNone(); // first == "1"
var onlyMatch = collection.SingleOrNone(element => element == "2"); // firstMatch == "2"
```

## Checking if value matches the given predicate

```csharp
var option = Option.Some("Example value");

var holds = option.Holds(example => example == "Example value"); // holds == true
```
