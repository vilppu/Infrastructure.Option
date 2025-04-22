[![Build Status](https://vilppu.visualstudio.com/Infrastructure.Option/_apis/build/status/vilppu.Infrastructure.Option?branchName=main)](https://vilppu.visualstudio.com/Infrastructure.Option/_build/latest?definitionId=1&branchName=main)

# Usage

Get nuget https://www.nuget.org/packages/Infrastructure.Option/

# Overview

`Option<T>` is a simple option type usable in C#.

- `Option.Some<T>` represents an available value.
- `Option.None<T>` represents the absence of a value.

The `Option` static class provides factory methods to create instances of `Option<T>`.

For more information about option types, see [Option type on Wikipedia](https://en.wikipedia.org/wiki/Option_type).

Basic manipulation of `Option` is done via `Choose()` and `Otherwise()` functions.

`Choose()` functions choose the underlying values i.e. something of type `Some<T>`.

`Otherwise()` functions are used to defined fallback when `None<T>` are encountered.

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
var value = option is Some<string> some ? some.Value : "Something else";
```

## Fallback values using fluent syntax

```
var option = Option.None<string>();
var optionalFallbackValue = Option.Some("Example value");
var fallbackValue = Option.Some("Example value");

var fallbackToOptionalValue = option.Otherwise(optionalFallbackValue);
var fallbackToValue = option.Otherwise(fallbackValue);

var fallbackToOptionalValueProvidedByValueFactory = option.Otherwise(() => optionalFallbackValue);
var fallbackToValueProvidedByValueFactory = option.Otherwise(() => fallbackValue);
```

## Choosing first value from many options

```
var options = new[] {
    Option.None<string>(),
    Option.Some("Example value")
};

var option = options.ChooseFirst();
```

## Choosing existing values from collection

```
var collection = new Option<string>[]
    {
        Option.None<string>(),
        Option.Some("1"),
        Option.None<string>(),
        Option.Some("2"),
        Option.None<string>(),
        Option.Some("3"),
        Option.None<string>(),
    };
var values = collection.Choose(); // is [ "1", "2", "3" ]
```
## Choosing values with mapping

```
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

var propertyValues = collection.Choose(entry => entry.ExampleProperty); // is [ "1", "2", "3" ]
```

# JSON Serialization

`Infrastructure.Option` supports JSON serialization using `System.Text.Json` without requiring any additional dependencies.

The `Option<T>` type is serialized as an object with a single `ValueOrNull` property that contains the wrapped value.

This approach produces idiomatic JSON for both .NET and the broader JSON ecosystem, and the optional value is clearly described in OpenAPI documentation.

For example, `Option.Some("Hello!")` is serialized as `{ "ValueOrNull": "Hello!" }`.