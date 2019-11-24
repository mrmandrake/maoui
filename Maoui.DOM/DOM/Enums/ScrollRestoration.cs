using System;

using WebAssembly;

namespace Maoui.DOM
{
    public enum ScrollRestoration
    {
        [Export(EnumValue = ConvertEnum.ToLower)]
        Auto,
        [Export(EnumValue = ConvertEnum.ToLower)]
        Manual
    }
}
