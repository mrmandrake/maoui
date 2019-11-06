using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace Maoui
{
    class ColorJsonConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Color);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var str = reader.ReadAsString();
            return Color.Parse(str);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}
