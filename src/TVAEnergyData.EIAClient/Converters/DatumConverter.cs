using System;
using Newtonsoft.Json;
using TVAEnergyData.EIAClient.Models;

namespace TVAEnergyData.EIAClient.Converters
{
    public class DatumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Datum) || t == typeof(Datum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                    var integerValue = serializer.Deserialize<long>(reader);
                    return new Datum { Integer = integerValue };
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new Datum { String = stringValue };
            }
            throw new Exception("Cannot un-marshal type Datum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (Datum)untypedValue;
            if (value.Integer != null)
            {
                serializer.Serialize(writer, value.Integer.Value);
                return;
            }

            if (value.String == null) throw new Exception("Cannot marshal type Datum");
            serializer.Serialize(writer, value.String);
        }

        public static readonly DatumConverter Singleton = new DatumConverter();
    }
}