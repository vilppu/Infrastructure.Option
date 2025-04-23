[![Build Status](https://vilppu.visualstudio.com/Infrastructure.Option/_apis/build/status/vilppu.Infrastructure.Option?branchName=main)](https://vilppu.visualstudio.com/Infrastructure.Option/_build/latest?definitionId=1&branchName=main)

# Usage

Get the NuGet package here: [Infrastructure.Option on NuGet](https://www.nuget.org/packages/Infrastructure.Option/)

# Overview

`Option<T>` is a simple option type usable in C#.

- `Option.Some<T>` represents an available value.
- `Option.None<T>` represents the absence of a value.

The `Option` static class provides factory methods to create instances of `Option<T>`.

For more information about option types, see [Option type on Wikipedia](https://en.wikipedia.org/wiki/Option_type).

Basic manipulation of `Option` is done via the `Choose()` and `Otherwise()` functions.

- `Choose()` selects the underlying values, i.e., those of type `Some<T>`.
- `Otherwise()` defines fallback behavior when encountering `None<T>`.

# Examples

## Creating `Some` values

```csharp
var someString = Option.Some("Example value");
var someInteger = Option.Some(12345);
```

## Creating `None` values

```csharp
var noneString = Option.None<string>();
var noneInteger = Option.None<int>();
```

## Pattern matching optional values

```csharp
var option = Option.Some("Example value");
var value = option is Some<string> some ? some.Value : "Something else";
```

## Fallback values using fluent syntax

```csharp
var option = Option.None<string>();
var optionalFallbackValue = Option.Some("Example value");
var fallbackValue = Option.Some("Example value");

var fallbackToOptionalValue = option.Otherwise(optionalFallbackValue);
var fallbackToValue = option.Otherwise(fallbackValue);

var fallbackToOptionalValueProvidedByFactory = option.Otherwise(() => optionalFallbackValue);
var fallbackToValueProvidedByFactory = option.Otherwise(() => fallbackValue);
```

## Choosing the first value from multiple options

```csharp
var options = new[] {
    Option.None<string>(),
    Option.Some("Example value")
};

var option = options.ChooseFirst();
```

## Selecting existing values from a collection

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

var values = collection.Choose(); // returns [ "1", "2", "3" ]
```

## Selecting values with mapping

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

var propertyValues = collection.Choose(entry => entry.ExampleProperty); // returns [ "1", "2", "3" ]
```

## Checking if value matches the given predicate

```csharp
var option = Option.Some("Example value");

var holds = option.Holds(example => example == "Example value"); // holds == true
```

# JSON Serialization

`Infrastructure.Option` supports JSON serialization using `System.Text.Json` without requiring any additional dependencies.

The `Option<T>` type is serialized as an object with a single `ValueOrNull` property that contains the wrapped value.

This approach produces idiomatic JSON for both .NET and the broader JSON ecosystem, and the optional value is clearly described in OpenAPI documentation.

For example, `Option.Some("Hello!")` is serialized as:

```json
{ "ValueOrNull": "Hello!" }
```

---