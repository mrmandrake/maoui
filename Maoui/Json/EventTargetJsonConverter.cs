using System;
using System.Reflection;

namespace Maoui
{
    class EventTargetJsonConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanRead => false;

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteValue(((EventTarget)value).Id);
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(EventTarget).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }
    }
}
