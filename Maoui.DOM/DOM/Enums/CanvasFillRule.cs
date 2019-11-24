using System;
using WebAssembly;

namespace Maoui.DOM
{
    // "nonzero" | "evenodd";
    public enum CanvasFillRule
    {
        [Export(EnumValue = ConvertEnum.ToLower)]
        NonZero,
        [Export(EnumValue = ConvertEnum.ToLower)]
        EventOdd,
    }
}
