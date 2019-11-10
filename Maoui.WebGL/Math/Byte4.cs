using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace WebGl
{
    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public struct Byte4 : IEquatable<Byte4>
    {
        [FieldOffset(0)]
        public byte X;
        [FieldOffset(1)]
        public byte Y;
        [FieldOffset(2)]
        public byte Z;
        [FieldOffset(3)]
        public byte W;

        public Byte4(byte x, byte y, byte z, byte w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        public override string ToString()
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            return string.Format((IFormatProvider)currentCulture, "{{X:{0} Y:{1} Z:{2} W:{3}}}", (object)this.X.ToString((IFormatProvider)currentCulture), (object)this.Y.ToString((IFormatProvider)currentCulture), (object)this.Z.ToString((IFormatProvider)currentCulture), (object)this.W.ToString((IFormatProvider)currentCulture));
        }

        public bool Equals(Byte4 other)
        {
            return this.Equals(ref other);
        }

        public bool Equals(ref Byte4 other)
        {
            if ((int)this.X == (int)other.X && (int)this.Y == (int)other.Y && (int)this.Z == (int)other.Z)
                return (int)this.W == (int)other.W;
            return false;
        }

        public override bool Equals(object obj)
        {
            bool flag = false;
            if (obj is Vector4)
                flag = this.Equals((object)(Vector4)obj);
            return flag;
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() + this.Y.GetHashCode() + this.Z.GetHashCode() + this.W.GetHashCode();
        }
    }
}
