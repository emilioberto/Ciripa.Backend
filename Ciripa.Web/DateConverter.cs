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
}

