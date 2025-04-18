using System;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure;

public class OptionJsonConverter : JsonConverter<object>
{
    private static readonly ConcurrentDictionary<Type, IGenericOptionJsonConverter> ReferenceTypeOptionJsonConverters = new();
    private static readonly ConcurrentDictionary<Type, IGenericOptionJsonConverter> ValueTypeOptionJsonConverters = new();

    public override bool CanConvert(Type typeToConvert) =>
        typeToConvert.IsGenericType &&
        (typeToConvert.GetGenericTypeDefinition() == typeof(Option<>) ||
         typeToConvert.GetGenericTypeDefinition() == typeof(Some<>) ||
         typeToConvert.GetGenericTypeDefinition() == typeof(None<>));

    public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var optionValueType = typeToConvert.GetGenericArguments()[0];
        var optionJsonConverter = GenericOptionJsonConverter(optionValueType);

        return optionJsonConverter.ReadObject(ref reader, typeToConvert, options);
    }

    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        var optionValueType = value.GetType().GetGenericArguments()[0];
        var optionJsonConverter = GenericOptionJsonConverter(optionValueType);

        optionJsonConverter.WriteObject(writer, value, options);
    }

    private static IGenericOptionJsonConverter GenericOptionJsonConverter(Type optionValueType) =>
        optionValueType.IsValueType
            ? GenericValueTypeOptionJsonConverter(optionValueType)
            : GenericReferenceTypeOptionJsonConverter(optionValueType);

    private static IGenericOptionJsonConverter GenericReferenceTypeOptionJsonConverter(Type optionValueType)
    {
        var optionJsonConverter = ReferenceTypeOptionJsonConverters.GetOrAdd(optionValueType, CreateReferenceTypeOptionJsonConverterFor);

        return optionJsonConverter;
    }

    private static IGenericOptionJsonConverter GenericValueTypeOptionJsonConverter(Type optionValueType)
    {
        var optionJsonConverter = ValueTypeOptionJsonConverters.GetOrAdd(optionValueType, CreateValueTypeOptionJsonConverterFor);

        return optionJsonConverter;
    }

    private static IGenericOptionJsonConverter CreateReferenceTypeOptionJsonConverterFor(Type optionValueType) =>
        (IGenericOptionJsonConverter)Activator.CreateInstance(typeof(ReferenceTypeOptionJsonConverter<>).MakeGenericType(optionValueType))!;

    private static IGenericOptionJsonConverter CreateValueTypeOptionJsonConverterFor(Type optionValueType) =>
        (IGenericOptionJsonConverter)Activator.CreateInstance(typeof(ValueTypeOptionJsonConverter<>).MakeGenericType(optionValueType))!;

}

public interface IGenericOptionJsonConverter
{
    object ReadObject(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options);
    void WriteObject(Utf8JsonWriter writer, object value, JsonSerializerOptions options);
}

public class ReferenceTypeOptionJsonConverter<T> : JsonConverter<Option<T>>, IGenericOptionJsonConverter
    where T : class
{
    public record SerializedOption(T? ValueOrNull);

    public override bool CanConvert(Type typeToConvert) =>
        typeToConvert.IsGenericType &&
        (typeToConvert.GetGenericTypeDefinition() == typeof(Option<>) ||
         typeToConvert.GetGenericTypeDefinition() == typeof(Some<>) ||
         typeToConvert.GetGenericTypeDefinition() == typeof(None<>));

    public override Option<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        JsonSerializer.Deserialize<SerializedOption>(ref reader, options) switch
        {
            { ValueOrNull: {} value } => Option<T>.Some(value),
            _ => Option<T>.None
        };

    public override void Write(Utf8JsonWriter writer, Option<T> value, JsonSerializerOptions options) =>
        JsonSerializer.Serialize(writer, new SerializedOption((T?)value.ValueOrNull), options);

    public object ReadObject(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        Read(ref reader, typeToConvert, options);

    public void WriteObject(Utf8JsonWriter writer, object value, JsonSerializerOptions options) =>
        Write(writer, (Option<T>)value, options);
}

public class ValueTypeOptionJsonConverter<T> : JsonConverter<Option<T>>, IGenericOptionJsonConverter
    where T : struct
{
    public record SerializedOption(T? ValueOrNull);

    public override bool CanConvert(Type typeToConvert) =>
        typeToConvert.IsGenericType &&
        (typeToConvert.GetGenericTypeDefinition() == typeof(Option<>) ||
         typeToConvert.GetGenericTypeDefinition() == typeof(Some<>) ||
         typeToConvert.GetGenericTypeDefinition() == typeof(None<>));

    public override Option<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        JsonSerializer.Deserialize<SerializedOption>(ref reader, options) switch
        {
            { ValueOrNull: { } value } => Option<T>.Some(value),
            _ => Option<T>.None
        };

    public override void Write(Utf8JsonWriter writer, Option<T> value, JsonSerializerOptions options) =>
        JsonSerializer.Serialize(writer, new SerializedOption((T?)value.ValueOrNull), options);

    public object ReadObject(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        Read(ref reader, typeToConvert, options);

    public void WriteObject(Utf8JsonWriter writer, object value, JsonSerializerOptions options) =>
        Write(writer, (Option<T>)value, options);
}