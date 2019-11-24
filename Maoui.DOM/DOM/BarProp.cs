using System;
using WebAssembly;

namespace Maoui.DOM
{

    [Export("BarProp", typeof(JSObject))]
    public sealed class BarProp : DOMObject
    {
        internal BarProp(JSObject handle) : base(handle) { }

        //public BarProp() { }
        [Export("visible")]
        public bool Visible => GetProperty<bool>("visible");
    }
}