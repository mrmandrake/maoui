using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;

namespace Maoui
{
    class JsonHandler : RequestHandler
    {
        public const string ContentType = "application/json; charset=utf-8";

        readonly Func<object> ctor;

        public JsonHandler(Func<object> ctor)
        {
            this.ctor = ctor;
        }

        public static byte[] GetData(object obj)
        {
            var r = Maoui.JsonConvert.SerializeObject(obj);
            var e = new UTF8Encoding(false);
            return e.GetBytes(r);
        }

        public override void Respond(HttpListenerContext listenerContext, CancellationToken token)
        {
            var response = listenerContext.Response;

            var data = GetData(ctor());

            response.StatusCode = 200;
            response.ContentType = ContentType;
            response.ContentLength64 = data.LongLength;

            using (var s = response.OutputStream)
            {
                s.Write(data, 0, data.Length);
            }
            response.Close();
        }
    }

}
