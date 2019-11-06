using System.Net;
using System.Threading;

namespace Maoui
{

    abstract class RequestHandler
    {
        public abstract void Respond(HttpListenerContext listenerContext, CancellationToken token);
    }

}
