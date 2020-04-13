using System;
using Ciripa.Domain;
using Newtonsoft.Json;

namespace Ciripa.Web
{
    public sealed class DateConverter : JsonConverter<Date>
    {
        public override void WriteJson(JsonWriter writer, Date value, JsonSerializer serializer)
        {
            writer.WriteValue(value.AsDateTime());
        }

        public override Date ReadJson(JsonReader reader, Type objectType, Date existingValue, bool hasExistingValue,
            JsonSerializer serializer) =>
            reader.Value is DateTime dateTimeValue ? (Date) dateTimeValue : new Date();
    }
    
    public sealed class NullableDateConverter : JsonConverter<Date?>
    {
        public override void WriteJson(JsonWriter writer, Date? value, JsonSerializer serializer)
        {
            if (value == null)
            {
                return;
            }
            writer.WriteValue(value.Value.AsDateTime());
        }

        public override Date? ReadJson(JsonReader reader, Type objectType, Date? existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (existingValue == null)
            {
                return null;
            }
            return reader.Value is DateTime dateTimeValue ? (Date) dateTimeValue : new Date();
        }
    }
}

