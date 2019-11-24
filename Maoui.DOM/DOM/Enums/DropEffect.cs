using System;

using WebAssembly;

namespace Maoui.DOM
{
    public enum DropEffect
    {
        [Export(EnumValue = ConvertEnum.ToLower)]
        None,
        [Export(EnumValue = ConvertEnum.ToLower)]
        Copy,
        [Export(EnumValue = ConvertEnum.ToLower)]
        Link,
        [Export(EnumValue = ConvertEnum.ToLower)]
        Move

    }
}
