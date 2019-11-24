using System;
using WebAssembly;

namespace Maoui.DOM
{

    [Export("MSGraphicsTrust", typeof(JSObject))]
    public sealed class MSGraphicsTrust : DOMObject
    {
        internal MSGraphicsTrust(JSObject handle) : base(handle) { }

        //public MSGraphicsTrust() { }
        [Export("constrictionActive")]
        public bool ConstrictionActive => GetProperty<bool>("constrictionActive");
        [Export("status")]
        public string Status => GetProperty<string>("status");
    }
}