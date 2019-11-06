using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace Maoui
{
    public static class UI
    {
        public const int MaxFps = 30;

        private static readonly ManualResetEvent started = new ManualResetEvent(false);

        private static CancellationTokenSource serverCts;

        private static readonly Dictionary<string, RequestHandler> publishedPaths = new Dictionary<string, RequestHandler>();

        private static readonly Dictionary<string, WebAssemblySession> globalElementSessions = new Dictionary<string, WebAssemblySession>();

        private static readonly byte[] clientJsBytes;

        private static readonly string clientJsEtag;

        public static byte[] ClientJsBytes => clientJsBytes;
        public static string ClientJsEtag => clientJsEtag;

        private static string _host = "*";

        public static string Host
        {
            get => _host;
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && _host != value)
                {
                    _host = value;
                    Restart();
                }
            }
        }

        private static int _port = 8080;

        public static int Port
        {
            get => _port;
            set
            {
                if (_port != value)
                {
                    _port = value;
                    Restart();
                }
            }
        }

        private static bool _serverEnabled = true;
        public static bool ServerEnabled
        {
            get => _serverEnabled;
            set
            {
                if (_serverEnabled != value)
                {
                    _serverEnabled = value;
                    if (_serverEnabled)
                        Restart();
                    else
                        Stop();
                }
            }
        }

        [Preserve]
        static void DisableServer()
        {
            Trace.trace();
            ServerEnabled = false;
        }

        [Preserve]
        public static void StartWebAssemblySession(string sessionId, string elementPath, string initialSize)
        {
            Trace.trace();
            Element element;
            RequestHandler handler;
            lock (publishedPaths)
            {
                publishedPaths.TryGetValue(elementPath, out handler);
            }

            var disposeElementWhenDone = true;
            if (handler is ElementHandler eh)
            {
                element = eh.GetElement();
                disposeElementWhenDone = eh.DisposeElementWhenDone;
            }
            else
                element = new Div();

            var ops = initialSize.Split(' ');
            var initialWidth = double.Parse(ops[0]);
            var initialHeight = double.Parse(ops[1]);
            var g = new WebAssemblySession(sessionId, element, disposeElementWhenDone, initialWidth, initialHeight, Trace.Error);
            lock (globalElementSessions)
            {
                globalElementSessions[sessionId] = g;
            }
            g.StartSession();
        }

        [Preserve]
        public static void ReceiveWebAssemblySessionMessageJson(string sessionId, string json)
        {
            Trace.trace();
            WebAssemblySession g;
            lock (globalElementSessions)
            {
                if (!globalElementSessions.TryGetValue(sessionId, out g))
                    return;
            }

            g.ReceiveMessageJson(json);
        }

        static UI()
        {
            Trace.trace();
            var asm = typeof(UI).Assembly;
            Trace.Log($"ASM = {asm}");
            foreach (var n in asm.GetManifestResourceNames())
                Trace.Log($"  {n}");

            using (var s = asm.GetManifestResourceStream("Maoui.Client.js"))
            {
                if (s == null)
                    throw new Exception("Missing Maoui.Client.js");
                using (var r = new StreamReader(s))
                {
                    clientJsBytes = Encoding.UTF8.GetBytes(r.ReadToEnd());
                }
            }
            clientJsEtag = "\"" + Utilities.GetShaHash(clientJsBytes) + "\"";
        }

        static void Publish(string path, RequestHandler handler)
        {
            Trace.trace();
            Trace.Log($"PUBLISH {path} {handler}");
            lock (publishedPaths) publishedPaths[path] = handler;
            Start();
        }

        public static void Publish(string path, Func<Element> elementCtor, bool disposeElementWhenDone = true)
        {
            Trace.trace();
            Publish(path, new ElementHandler(elementCtor, disposeElementWhenDone));
        }

        public static void Publish(string path, Element element, bool disposeElementWhenDone = true)
        {
            Trace.trace();
            Publish(path, () => element, disposeElementWhenDone);
        }

        public static void PublishFile(string filePath)
        {
            Trace.trace();
            var path = "/" + System.IO.Path.GetFileName(filePath);
            PublishFile(path, filePath);
        }

        public static void PublishFile(string path, string filePath, string contentType = null)
        {
            Trace.trace();
            var data = System.IO.File.ReadAllBytes(filePath);
            if (contentType == null)
                contentType = GuessContentType(path, filePath);

            var etag = "\"" + Utilities.GetShaHash(data) + "\"";
            Publish(path, new DataHandler(data, etag, contentType));
        }

        public static void PublishFile(string path, byte[] data, string contentType)
        {
            Trace.trace();
            var etag = "\"" + Utilities.GetShaHash(data) + "\"";
            Publish(path, new DataHandler(data, etag, contentType));
        }

        public static void PublishFile(string path, byte[] data, string etag, string contentType)
        {
            Trace.trace();
            Publish(path, new DataHandler(data, etag, contentType));
        }

        public static bool TryGetFileContentAtPath(string path, out FileContent file)
        {
            Trace.trace();
            RequestHandler handler;
            lock (publishedPaths)
            {
                if (!publishedPaths.TryGetValue(path, out handler))
                {
                    file = null;
                    return false;
                }
            }
            if (handler is DataHandler dh)
            {
                file = new FileContent
                {
                    Etag = dh.Etag,
                    Content = dh.Data,
                    ContentType = dh.ContentType,
                };
                return true;
            }
            file = null;
            return false;
        }

        public static void PublishJson(string path, Func<object> ctor)
        {
            Trace.trace();
            Publish(path, new JsonHandler(ctor));
        }

        public static void PublishJson(string path, object value)
        {
            Trace.trace();
            var data = JsonHandler.GetData(value);
            var etag = "\"" + Utilities.GetShaHash(data) + "\"";
            Publish(path, new DataHandler(data, etag, JsonHandler.ContentType));
        }

        public static void PublishCustomResponse(string path, Action<HttpListenerContext, CancellationToken> responder)
        {
            Trace.trace();
            Publish(path, new CustomHandler(responder));
        }

        static string GuessContentType(string path, string filePath)
        {
            Trace.trace();
            return null;
        }

        public static void Present(string path, object presenter = null)
        {
            Trace.trace();
            WaitUntilStarted();
            var url = GetUrl(path);
            Trace.Log($"PRESENT {url}");
            Platform.OpenBrowser(url, presenter);
        }

        public static string GetUrl(string path)
        {
            Trace.trace();
            var localhost = _host == "*" ? "localhost" : _host;
            var url = $"http://{localhost}:{_port}{path}";
            return url;
        }

        public static void WaitUntilStarted() => started.WaitOne();

        static void Start()
        {
            Trace.trace();
            if ((!_serverEnabled) || (serverCts != null))
                return;

            serverCts = new CancellationTokenSource();
            var token = serverCts.Token;
            var listenerPrefix = $"http://{_host}:{_port}/";
            Task.Run(() => RunAsync(listenerPrefix, token), token);
        }

        static void Stop()
        {
            Trace.trace();
            var scts = serverCts;
            if (scts == null)
                return;

            serverCts = null;
            started.Reset();

            Trace.Log($"Stopping...");
            scts.Cancel();
        }

        static void Restart()
        {
            Trace.trace();
            if (serverCts == null)
                return;
            Stop();
            Start();
        }

        static async Task RunAsync(string listenerPrefix, CancellationToken token)
        {
            Trace.trace();
            HttpListener listener = null;
            var wait = 5;

            started.Reset();
            while (!started.WaitOne(0) && !token.IsCancellationRequested)
            {
                try
                {
                    listener = new HttpListener();
                    listener.Prefixes.Add(listenerPrefix);
                    listener.Start();
                    started.Set();
                }
                catch (System.Net.Sockets.SocketException ex)
                {
                    Trace.Error($"{listenerPrefix} error: {ex.Message}. Trying again in {wait} seconds...");
                    await Task.Delay(wait * 1000).ConfigureAwait(false);
                }
                catch (System.Net.HttpListenerException ex)
                {
                    Trace.Error($"{listenerPrefix} error: {ex.Message}. Trying again in {wait} seconds...");
                    await Task.Delay(wait * 1000).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Trace.Error(ex);
                    return;
                }
            }

            Trace.Log($"Listening at {listenerPrefix}...");
            while (!token.IsCancellationRequested)
            {
                var listenerContext = await listener.GetContextAsync().ConfigureAwait(false);
                if (listenerContext.Request.IsWebSocketRequest)
                    ProcessWebSocketRequest(listenerContext, token);
                else
                    ProcessRequest(listenerContext, token);
            }
        }

        static void ProcessRequest(HttpListenerContext listenerContext, CancellationToken token)
        {
            Trace.trace();
            var url = listenerContext.Request.Url;
            var path = url.LocalPath;

            Trace.Log($"{listenerContext.Request.HttpMethod} {url.LocalPath}");

            var response = listenerContext.Response;

            if (path == "/ooui.js")
            {
                var inm = listenerContext.Request.Headers.Get("If-None-Match");
                if (string.IsNullOrEmpty(inm) || inm != clientJsEtag)
                {
                    response.StatusCode = 200;
                    response.ContentLength64 = clientJsBytes.LongLength;
                    response.ContentType = "application/javascript";
                    response.ContentEncoding = Encoding.UTF8;
                    response.AddHeader("Cache-Control", "public, max-age=60");
                    response.AddHeader("Etag", clientJsEtag);
                    using (var s = response.OutputStream)
                    {
                        s.Write(clientJsBytes, 0, clientJsBytes.Length);
                    }
                    response.Close();
                }
                else
                {
                    response.StatusCode = 304;
                    response.Close();
                }
            }
            else
            {
                var found = false;
                RequestHandler handler;
                lock (publishedPaths) found = publishedPaths.TryGetValue(path, out handler);
                if (found)
                {
                    try
                    {
                        handler.Respond(listenerContext, token);
                    }
                    catch (Exception ex)
                    {
                        Trace.Error(ex);
                        try
                        {
                            response.StatusCode = 500;
                            response.Close();
                        }
                        catch { }
                    }
                }
                else
                {
                    response.StatusCode = 404;
                    response.Close();
                }
            }
        }

        static async void ProcessWebSocketRequest(HttpListenerContext listenerContext, CancellationToken serverToken)
        {
            Trace.trace();
            // Find the element
            var url = listenerContext.Request.Url;
            var path = url.LocalPath;

            RequestHandler handler;
            var found = false;
            lock (publishedPaths) found = publishedPaths.TryGetValue(path, out handler);
            var elementHandler = handler as ElementHandler;
            if (!found || elementHandler == null)
            {
                listenerContext.Response.StatusCode = 404;
                listenerContext.Response.Close();
                return;
            }

            Element element = null;
            bool disposeElementWhenDone = true;
            try
            {
                element = elementHandler.GetElement();
                disposeElementWhenDone = elementHandler.DisposeElementWhenDone;

                if (element == null)
                    throw new Exception("Handler returned a null element");
            }
            catch (Exception ex)
            {
                listenerContext.Response.StatusCode = 500;
                listenerContext.Response.Close();
                Trace.Error(ex);
                return;
            }

            // Connect the web socket
            System.Net.WebSockets.WebSocketContext webSocketContext = null;
            System.Net.WebSockets.WebSocket webSocket = null;
            try
            {
                webSocketContext = await listenerContext.AcceptWebSocketAsync(subProtocol: "ooui").ConfigureAwait(false);
                webSocket = webSocketContext.WebSocket;
                Trace.Log($"WEBSOCKET {listenerContext.Request.Url.LocalPath}");
            }
            catch (Exception ex)
            {
                listenerContext.Response.StatusCode = 500;
                listenerContext.Response.Close();
                Trace.Error(ex);
                return;
            }

            // Set the element's dimensions
            var query = (from part in listenerContext.Request.Url.Query.Split(new[] { '?', '&' })
                         where part.Length > 0
                         let kvs = part.Split('=')
                         where kvs.Length == 2
                         select kvs).ToDictionary(x => Uri.UnescapeDataString(x[0]), x => Uri.UnescapeDataString(x[1]));

            if (!query.TryGetValue("w", out var wValue) || string.IsNullOrEmpty(wValue))
                wValue = "640";

            if (!query.TryGetValue("h", out var hValue) || string.IsNullOrEmpty(hValue))
                hValue = "480";

            var icult = System.Globalization.CultureInfo.InvariantCulture;
            if (!double.TryParse(wValue, System.Globalization.NumberStyles.Any, icult, out var w))
                w = 640;
            if (!double.TryParse(hValue, System.Globalization.NumberStyles.Any, icult, out var h))
                h = 480;

            // Create a new session and let it handle everything from here
            try
            {
                var session = new WebSocketSession(webSocket, element, disposeElementWhenDone, w, h, Trace.Error, serverToken);
                await session.RunAsync().ConfigureAwait(false);
            }
            catch (System.Net.WebSockets.WebSocketException ex) when (ex.WebSocketErrorCode == System.Net.WebSockets.WebSocketError.ConnectionClosedPrematurely)
            {
                // The remote party closed the WebSocket connection without completing the close handshake.
            }
            catch (Exception ex)
            {
                Trace.Error(ex);
            }
            finally
            {
                webSocket?.Dispose();
            }
        }
    }
}
