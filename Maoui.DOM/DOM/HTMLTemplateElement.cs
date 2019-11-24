using System;
using WebAssembly;

namespace Maoui.DOM
{

    [Export("HTMLTemplateElement", typeof(JSObject))]
    public sealed class HTMLTemplateElement : HTMLElement, IHTMLTemplateElement
    {
        internal HTMLTemplateElement(JSObject handle) : base(handle) { }

        //public HTMLTemplateElement() { }
        [Export("content")]
        public DocumentFragment Content => GetProperty<DocumentFragment>("content");
    }
}