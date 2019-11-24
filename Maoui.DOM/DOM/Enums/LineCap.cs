using System;
using WebAssembly;

namespace Maoui.DOM
{
    public enum LineCap
    {
        [Export(EnumValue = ConvertEnum.ToLower)]
        Butt,
        [Export(EnumValue = ConvertEnum.ToLower)]
        Round,
        [Export(EnumValue = ConvertEnum.ToLower)]
        Square,
    }
}
