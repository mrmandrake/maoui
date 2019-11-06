using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Runtime.InteropServices;

namespace Maoui
{
    class CustomHandler : RequestHandler
    {
        readonly Action<HttpListenerContext, CancellationToken> responder;

        public CustomHandler(Action<HttpListenerContext, CancellationToken> responder)
        {
            this.responder = responder;
        }

        public override void Respond(HttpListenerContext listenerContext, CancellationToken token)
        {
            responder(listenerContext, token);
        }
    }

}
