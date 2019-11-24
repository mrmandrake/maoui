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

        private static readonly Dictionary<string, RequestHandler> publishedPaths = new Dictionary<string, RequestHandler>();

        private static readonly Dictionary<string, WebAssemblySession> globalElementSessions = new Dictionary<string, WebAssemblySession>();

        private static string sessionId = "main";

        [Preserve]
        public static void StartWebAssemblySession(double initialWidth, double initialHeight)
        {
            Trace.Log("Starting webassembly...");
            Element element;
            RequestHandler handler;
            var elementPath = "/";
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

            var g = new WebAssemblySession(sessionId, element, disposeElementWhenDone, initialWidth, initialHeight, Trace.Error);
            lock (globalElementSessions)
                globalElementSessions[sessionId] = g;

            g.StartSession();
        }

        [Preserve]
        public static void ReceiveWebAssemblySessionMessageJson(string json)
        {
            Trace.trace();
            WebAssemblySession g;
            lock (globalElementSessions)
                if (!globalElementSessions.TryGetValue(sessionId, out g))
                    return;

            g.ReceiveMessageJson(json);
        }

        static UI()
        {
            Trace.trace();
            var asm = typeof(UI).Assembly;
            Trace.Log($"ASM = {asm}");
            foreach (var n in asm.GetManifestResourceNames())
                Trace.Log($"  {n}");
        }

        static void Publish(string path, RequestHandler handler)
        {
            Trace.trace();
            Trace.Log($"PUBLISH {path} {handler}");
            lock (publishedPaths) publishedPaths[path] = handler;
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
    }
}
