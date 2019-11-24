using System;
using WebAssembly;

namespace Maoui.DOM
{

    [Export("HTMLUnknownElement", typeof(JSObject))]
    public sealed class HTMLUnknownElement : HTMLElement
    {
        internal HTMLUnknownElement(JSObject handle) : base(handle) { }

        //public HTMLUnknownElement () { }
    }
}