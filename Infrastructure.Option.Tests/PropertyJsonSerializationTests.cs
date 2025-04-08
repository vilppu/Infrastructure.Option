using System.Text.Json;
using Shouldly;
using Xunit;

namespace Infrastructure.Tests.Core;


public class PropertyJsonSerializationTests
{
    private record TypeWithOptionalProperty<T>(Option<T> OptionalProperty);
    private record ExampleReferenceType(string ExampleProperty);
    private record struct ExampleValueType(string ExampleProperty);

    [Fact]
    public void None_is_serialized_as_empty()
    {
        var sut = new TypeWithOptionalProperty<string>(
            Option<string>.None
        );

        var serialized = JsonSerializer.Serialize(sut);
        var deserialized = JsonSerializer.Deserialize<TypeWithOptionalProperty<string>>(serialized);

        serialized.ShouldBe("{\"OptionalProperty\":[]}");
        deserialized.ShouldBe(sut);
    }

    [Fact]
    public void Object_that_is_reference_type_is_serialized_as_singleton_array()
    {
        var sut = new TypeWithOptionalProperty<ExampleReferenceType>(
            new ExampleReferenceType("Hello!")
            );

        var serialized = JsonSerializer.Serialize(sut);
        var deserialized = JsonSerializer.Deserialize<TypeWithOptionalProperty<ExampleReferenceType>>(serialized);

        serialized.ShouldBe("{\"OptionalProperty\":[{\"ExampleProperty\":\"Hello!\"}]}");
        deserialized.ShouldBe(sut);
    }

    [Fact]
    public void Object_that_is_value_type_is_serialized_as_singleton_array()
    {
        var sut = new TypeWithOptionalProperty<ExampleValueType>(
            new ExampleValueType("Hello!")
            );

        var serialized = JsonSerializer.Serialize(sut);
        var deserialized = JsonSerializer.Deserialize<TypeWithOptionalProperty<ExampleValueType>>(serialized);

        serialized.ShouldBe("{\"OptionalProperty\":[{\"ExampleProperty\":\"Hello!\"}]}");
        deserialized.ShouldBe(sut);
    }

    [Fact]
    public void Array_is_serialized_as_singleton_array()
    {
        var sut = new TypeWithOptionalProperty<int[]>(
            Option.Some<int[]>([1, 2, 3, 4, 5])
            );

        var serialized = JsonSerializer.Serialize(sut);
        var deserialized = JsonSerializer.Deserialize<TypeWithOptionalProperty<int[]>>(serialized);

        serialized.ShouldBe("{\"OptionalProperty\":[[1,2,3,4,5]]}");
        deserialized.ShouldBeEquivalentTo(sut);
    }

    [Fact]
    public void String_is_serialized_as_singleton_array()
    {
        var sut = new TypeWithOptionalProperty<string>(
            Option.Some("Hello!")
            );

        var serialized = JsonSerializer.Serialize(sut);
        var deserialized = JsonSerializer.Deserialize<TypeWithOptionalProperty<string>>(serialized);

        serialized.ShouldBe("{\"OptionalProperty\":[\"Hello!\"]}");
        deserialized.ShouldBe(sut);
    }

    [Fact]
    public void Integer_is_serialized_as_singleton_array()
    {
        var sut = new TypeWithOptionalProperty<int>(
            Option.Some(12345)
            );

        var serialized = JsonSerializer.Serialize(sut);
        var deserialized = JsonSerializer.Deserialize<TypeWithOptionalProperty<int>>(serialized);

        serialized.ShouldBe("{\"OptionalProperty\":[12345]}");
        deserialized.ShouldBe(sut);
    }

    [Fact]
    public void Boolean_is_serialized_as_singleton_array()
    {
        var sut = new TypeWithOptionalProperty<bool>(
            Option.Some(true)
            );

        var serialized = JsonSerializer.Serialize(sut);
        var deserialized = JsonSerializer.Deserialize<TypeWithOptionalProperty<bool>>(serialized);

        serialized.ShouldBe("{\"OptionalProperty\":[true]}");
        deserialized.ShouldBe(sut);
    }
}