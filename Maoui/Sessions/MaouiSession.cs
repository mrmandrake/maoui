using System;
using System.Collections.Generic;
using System.Text;
using WebAssembly;

namespace Maoui
{
    public class MaouiSession : Session
    {
        readonly string id;
        readonly Action<Message> handleElementMessageSent;

        public MaouiSession(string id, Element element, bool disposeElementAfterSession, double initialWidth, double initialHeight, Action<string, Exception> errorLogger)
            : base(element, disposeElementAfterSession, initialWidth, initialHeight, errorLogger)
        {
            this.id = id;
            handleElementMessageSent = QueueMessage;
        }

        protected override void QueueMessage(Message message)
        {
            lock (queuedMessages)
            {
                QueueMessageLocked(message);
                var i = 0;
                while (i < queuedMessages.Count)
                {
                    var sb = new System.IO.StringWriter();
                    sb.Write("__maouiReceiveMessages(\"");
                    sb.Write(id);
                    sb.Write("\",");
                    sb.Write("[");
                    var head = "";
                    sb.Write(head);
                    queuedMessages[i].WriteJson(sb);                    
                    sb.Write("])");
                    var jsonp = sb.ToString();
                    Trace.Log($"transmit {jsonp}");
                    i++;
                }                
                queuedMessages.Clear();
            }
        }

        public void ReceiveMessageJson(string json)
        {
            try
            {
                var message = Newtonsoft.Json.JsonConvert.DeserializeObject<Message>(json);
                element.Receive(message);
            }
            catch (Exception ex)
            {
                Error("Failed to process received message", ex);
            }
        }

        public void StartSession()
        {
            // Start watching for changes in the element
            element.MessageSent += handleElementMessageSent;

            // Add it to the document body
            if (element.WantsFullScreen)
            {
                element.Style.Width = initialWidth;
                element.Style.Height = initialHeight;
            }
            QueueMessage(Message.Call("document.body", "appendChild", element));
        }

        public void StopSession()
        {
            element.MessageSent -= handleElementMessageSent;
        }
    }
}
