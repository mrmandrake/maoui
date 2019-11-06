using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace Maoui
{
    class ElementHandler : RequestHandler
    {
        readonly Lazy<Element> element;

        public bool DisposeElementWhenDone { get; }

        public ElementHandler(Func<Element> ctor, bool disposeElementWhenDone)
        {
            element = new Lazy<Element>(ctor);
            DisposeElementWhenDone = disposeElementWhenDone;
        }

        public Element GetElement() => element.Value;

        static string EscapeHtml(string text)
        {
            return text.Replace("&", "&amp;").Replace("<", "&lt;");
        }

        public static string HeadHtml { get; set; } = @"<link rel=""stylesheet"" href=""css/bootstrap.min.css"" />";
        public static string BodyHeaderHtml { get; set; } = @"";
        public static string BodyFooterHtml { get; set; } = @"";

        public static readonly StyleSelectors rules = new StyleSelectors();

        public static void RenderTemplate(TextWriter writer, string webSocketPath, string title, string initialHtml)
        {
            writer.Write(@"<!DOCTYPE html><html><head><title>");
            writer.Write(EscapeHtml(title));
            writer.Write(@"</title><meta name=""viewport"" content=""width=device-width, initial-scale=1""/>");
            writer.WriteLine(HeadHtml);
            writer.WriteLine(@"  <style>");
            writer.WriteLine(rules.ToString());
            writer.WriteLine(@"  </style></head><body>");
            writer.WriteLine(BodyHeaderHtml);
            writer.WriteLine(@"<div id=""ooui-body"" class=""container-fluid"" style=""padding:0;margin:0"">");
            writer.WriteLine(initialHtml);
            writer.Write(@"</div><script src=""/ooui.js""></script><script>ooui(""");
            writer.Write(webSocketPath);
            writer.WriteLine(@""");</script>");
            writer.WriteLine(BodyFooterHtml);
            writer.WriteLine(@"</body></html>");
        }

        public static string RenderTemplate(string webSocketPath, string title = "", string initialHtml = "")
        {
            using (var w = new System.IO.StringWriter())
            {
                RenderTemplate(w, webSocketPath, title, initialHtml);
                return w.ToString();
            }
        }

        public override void Respond(HttpListenerContext listenerContext, CancellationToken token)
        {
            var url = listenerContext.Request.Url;
            var path = url.LocalPath;
            var response = listenerContext.Response;

            response.StatusCode = 200;
            response.ContentType = "text/html";
            response.ContentEncoding = Encoding.UTF8;
            var html = Encoding.UTF8.GetBytes(RenderTemplate(path));
            response.ContentLength64 = html.LongLength;
            using (var s = response.OutputStream)
            {
                s.Write(html, 0, html.Length);
            }
            response.Close();
        }
    }
}

