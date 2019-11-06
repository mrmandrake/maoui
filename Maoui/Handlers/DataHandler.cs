using System.Net;
using System.Threading;

namespace Maoui
{

    class DataHandler : RequestHandler
    {
        readonly byte[] data;
        readonly string etag;
        readonly string contentType;

        public byte[] Data => data;
        public string Etag => etag;
        public string ContentType => contentType;

        public DataHandler(byte[] data, string etag, string contentType = null)
        {
            this.data = data;
            this.etag = etag;
            this.contentType = contentType;
        }

        public override void Respond(HttpListenerContext listenerContext, CancellationToken token)
        {
            var url = listenerContext.Request.Url;
            var path = url.LocalPath;
            var response = listenerContext.Response;

            var inm = listenerContext.Request.Headers.Get("If-None-Match");
            if (!string.IsNullOrEmpty(inm) && inm == etag)
            {
                response.StatusCode = 304;
            }
            else
            {
                response.StatusCode = 200;
                response.AddHeader("Etag", etag);
                if (!string.IsNullOrEmpty(contentType))
                    response.ContentType = contentType;
                response.ContentLength64 = data.LongLength;

                using (var s = response.OutputStream)
                {
                    s.Write(data, 0, data.Length);
                }
            }
            response.Close();
        }
    }

}
