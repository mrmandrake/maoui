using System;
using WebAssembly;

namespace Maoui.DOM
{
    public enum Direction
    {
        [Export(EnumValue = ConvertEnum.ToLower)]
        Inherit,
        [Export("rtl")]
        RightToLeft,
        [Export("ltr")]
        LeftToRight,
    }
}
