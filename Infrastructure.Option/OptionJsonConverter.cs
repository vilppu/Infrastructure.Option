using System;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure;

public class OptionJsonConverter : JsonConverter<object>
{
    private static readonly ConcurrentDictionary<Type, IGenericOptionJsonConverter> OptionJsonConverters = new();

    public override bool CanConvert(Type typeToConvert) =>
        typeToConvert.IsGenericType &&
        (typeToConvert.GetGenericTypeDefinition() == typeof(Option<>) ||
         typeToConvert.GetGenericTypeDefinition() == typeof(Some<>) ||
         typeToConvert.GetGenericTypeDefinition() == typeof(None<>));

    public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var optionValueType = typeToConvert.GetGenericArguments()[0];
        var optionJsonConverter = OptionJsonConverters.GetOrAdd(optionValueType, CreateOptionJsonConverterFor);

        return optionJsonConverter.ReadObject(ref reader, typeToConvert, options);
    }

    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        var optionValueType = value.GetType().GetGenericArguments()[0];
        var optionJsonConverter = OptionJsonConverters.GetOrAdd(optionValueType, CreateOptionJsonConverterFor);

        optionJsonConverter.WriteObject(writer, value, options);
    }

    private static IGenericOptionJsonConverter CreateOptionJsonConverterFor(Type optionValueType) =>
        (IGenericOptionJsonConverter)Activator.CreateInstance(typeof(OptionJsonConverter<>).MakeGenericType(optionValueType))!;

}

public interface IGenericOptionJsonConverter
{
    object ReadObject(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options);
    void WriteObject(Utf8JsonWriter writer, object value, JsonSerializerOptions options);
}

public class OptionJsonConverter<T> : JsonConverter<Option<T>>, IGenericOptionJsonConverter
{
    public override bool CanConvert(Type typeToConvert) =>
        typeToConvert.IsGenericType &&
        (typeToConvert.GetGenericTypeDefinition() == typeof(Option<>) ||
         typeToConvert.GetGenericTypeDefinition() == typeof(Some<>) ||
         typeToConvert.GetGenericTypeDefinition() == typeof(None<>));

    public override Option<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        JsonSerializer.Deserialize<SerializedOption<T>>(ref reader, options) switch
        {
            { ValueOrDefault: {} value } => Option<T>.Some(value),
            _ => Option<T>.None
        };

    public override void Write(Utf8JsonWriter writer, Option<T> value, JsonSerializerOptions options) =>
        JsonSerializer.Serialize(writer, new SerializedOption<T>(value.ValueOrNull), options);

    public object ReadObject(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        Read(ref reader, typeToConvert, options);

    public void WriteObject(Utf8JsonWriter writer, object value, JsonSerializerOptions options) =>
        Write(writer, (Option<T>)value, options);
}

public record SerializedOption<T>(T? ValueOrDefault)
{
}