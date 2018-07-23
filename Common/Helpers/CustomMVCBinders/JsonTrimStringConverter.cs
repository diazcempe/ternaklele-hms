using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Common.Helpers.CustomMVCBinders
{
    /// <inheritdoc />
    /// <summary>
    /// The purpose of this class is to TRIM all the JSON string inputs which come from body.
    /// </summary>
    public class JsonTrimStringConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override bool CanRead => true;

        public override bool CanWrite => false; // only used for reading inputs

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var text = (string)reader.Value;
            return text?.Trim();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new Exception("Not configured for writing");
        }
    }
}
