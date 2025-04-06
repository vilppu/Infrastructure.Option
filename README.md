[![Build Status](https://vilppu.visualstudio.com/Infrastructure.Option/_apis/build/status/vilppu.Infrastructure.Option?branchName=main)](https://vilppu.visualstudio.com/Infrastructure.Option/_build/latest?definitionId=1&branchName=main)

# Usage

Get nuget https://www.nuget.org/packages/Infrastructure.Option/

# Overview

`Option<T>` is a simple option type usable with C#

`Option.Some<T>` represents an available value and `Option.None<T>` is non-available value.

`Option` is a static class that can be used to create `Option<T>` instances.

`Option<T>` is serialized as a singleton or empty array. In JSON `Option.Some<T>` is presented as a singleton array `[value]` and `Option.None<T>` is presented as an empty array `[]`.

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
var anotherFallback = option.Or(() => "Fallback expression")
```

```
Option<string> option = Option.None<string>();
Option<string> anotherOption = Option.Some("Example value");
var fallback = option.Otherwise(anotherOption);
var anotherFallback = option.Otherwise(() => anotherOption);
```

## Choosing first some value from many options

```
var options = new Option<string>[] {
    Option.None<string>,
    Option.Some("Example value")
};

var option = options.FirstSomeOtherwiseNone();
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
var values = collection.Values(); // values is [ "1", "2", "3", "4", "5" ]
```
