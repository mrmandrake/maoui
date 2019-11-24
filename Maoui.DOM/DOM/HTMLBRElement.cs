using System;
using WebAssembly;

namespace Maoui.DOM
{

    [Export("HTMLBRElement", typeof(JSObject))]
    public sealed class HTMLBRElement : HTMLElement, IHTMLBRElement
    {
        internal HTMLBRElement(JSObject handle) : base(handle) { }

        //public HTMLBRElement() { }
        [Export("clear")]
        public string Clear { get => GetProperty<string>("clear"); set => SetProperty<string>("clear", value); }
    }
}