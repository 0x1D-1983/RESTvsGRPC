using Google.Protobuf;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ModelLibrary.Data
{
    public class ProtobufJsonConvertor : JsonConverter<object>
    {
        //public override bool CanConvert(Type objectType)
        //{
        //    return typeof(IMessage).IsAssignableFrom(objectType);
        //}

        public override object Read(ref Utf8JsonReader reader, Type typeToConvert,
            JsonSerializerOptions options) => reader.TokenType switch
            {
                JsonTokenType.True => true,
                JsonTokenType.False => false,
                JsonTokenType.Number when reader.TryGetInt64(out long l) => l,
                JsonTokenType.Number => reader.GetDouble(),
                JsonTokenType.String when reader.TryGetDateTime(out DateTime datetime) => datetime,
                JsonTokenType.String => reader.GetString()!,
                _ => JsonDocument.ParseValue(ref reader).RootElement.Clone()
            };

        //public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        //{
        //    var converter = new ExpandoObjectConverter();
        //    var o = converter.ReadJson(reader, objectType, existingValue, serializer);

        //    var text = JsonConvert.SerializeObject(o);

        //    var message = (IMessage) Activator.CreateInstance(objectType);

        //    var parser = new JsonParser(JsonParser.Settings.Default.WithIgnoreUnknownFields(true));
        //    return parser.Parse(text, message.Descriptor);
        //}

        public override void Write(Utf8JsonWriter writer, object objectToWrite,
            JsonSerializerOptions options) =>
            JsonSerializer.Serialize(writer, objectToWrite, objectToWrite.GetType(), options);

        //public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        //{
        //    writer.WriteRawValue(JsonFormatter.Default.Format((IMessage)value));
        //}
    }
}
