using System.Text.Json;
using Shouldly;
using Xunit;

namespace Infrastructure.Tests.Core;

public class ObjectJsonSerializationTests
{
    private record ExampleReferenceType(string ExampleProperty);
    private record struct ExampleValueType(string ExampleProperty);

    [Fact]
    public void None_is_serialized_as_wrapped_null()
    {
        var sut = Option.None<string>();

        var serialized = JsonSerializer.Serialize(sut);
        var deserialized = JsonSerializer.Deserialize<Option<string>>(serialized);

        deserialized.ShouldBe(Option.None<string>());
    }

    [Fact]
    public void Object_that_is_reference_type_is_serialized_as_wrapped_value()
    {
        var sut = Option.Some(new ExampleReferenceType("Hello!"));

        var serialized = JsonSerializer.Serialize(sut);
        var deserialized = JsonSerializer.Deserialize<Option<ExampleReferenceType>>(serialized);

        deserialized.ShouldBe(sut);
    }

    [Fact]
    public void Object_that_is_value_type_is_serialized_as_wrapped_value()
    {
        var sut = Option.Some(new ExampleValueType("Hello!"));

        var serialized = JsonSerializer.Serialize(sut);
        var deserialized = JsonSerializer.Deserialize<Option<ExampleValueType>>(serialized);

        deserialized.ShouldBe(sut);
    }

    [Fact]
    public void Some_array_value_is_serialized_as_wrapped_value()
    {
        var sut = Option.Some<int[]>([1, 2, 3, 4, 5]);

        var serialized = JsonSerializer.Serialize(sut);
        var deserialized = JsonSerializer.Deserialize<Option<int[]>>(serialized);

        deserialized.ShouldBeEquivalentTo(sut);
    }

    [Fact]
    public void Some_string_value_is_serialized_as_wrapped_value()
    {
        var sut = Option.Some("Hello!");

        var serialized = JsonSerializer.Serialize(sut);
        var deserialized = JsonSerializer.Deserialize<Option<string>>(serialized);

        deserialized.ShouldBe(sut);
    }

    [Fact]
    public void Some_numeric_value_is_serialized_as_wrapped_value()
    {
        var sut = Option.Some(12345);

        var serialized = JsonSerializer.Serialize(sut);
        var deserialized = JsonSerializer.Deserialize<Option<int>>(serialized);

        deserialized.ShouldBe(sut);
    }

    [Fact]
    public void Some_boolean_value_is_serialized_as_wrapped_value()
    {
        var sut = Option.Some(true);

        var serialized = JsonSerializer.Serialize(sut);
        var deserialized = JsonSerializer.Deserialize<Option<bool>>(serialized);

        deserialized.ShouldBe(sut);
    }
}