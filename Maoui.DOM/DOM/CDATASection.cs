using System;
using WebAssembly;

namespace Maoui.DOM
{

    [Export("CDATASection", typeof(JSObject))]
    public sealed class CDATASection : Text
    {
        internal CDATASection(JSObject handle) : base(handle) { }

        //public CDATASection() { }
    }
}