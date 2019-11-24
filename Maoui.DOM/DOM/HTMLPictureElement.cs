using System;
using WebAssembly;

namespace Maoui.DOM
{

    [Export("HTMLPictureElement", typeof(JSObject))]
    public sealed class HTMLPictureElement : HTMLElement
    {
        internal HTMLPictureElement(JSObject handle) : base(handle) { }

        //public HTMLPictureElement() { }
    }
}