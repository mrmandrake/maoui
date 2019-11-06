using System;

namespace Maoui
{
    public class TargetEventArgs : EventArgs
    {
        public double OffsetX { get; set; }
        public double OffsetY { get; set; }
        public double ClientHeight { get; set; }
        public double ClientWidth { get; set; }
    }
}
