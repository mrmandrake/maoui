using System;
using WebAssembly;

namespace Maoui.DOM
{

    [Export("CanvasGradient", typeof(JSObject))]
    public sealed class CanvasGradient : DOMObject, ICanvasGradient
    {
        internal CanvasGradient(JSObject handle) : base(handle) { }

        //public CanvasGradient() { }
        [Export("addColorStop")]
        public void AddColorStop(double offset, string color)
        {
            InvokeMethod<object>("addColorStop", offset, color);
        }
    }

}