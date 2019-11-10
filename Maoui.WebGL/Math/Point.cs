using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace WebGl
{
    [DataContract]
    public struct Point : IEquatable<Point>
    {
        private static readonly Point zero = new Point();
        [DataMember]
        public int X;
        [DataMember]
        public int Y;

        public static Point Zero
        {
            get
            {
                return Point.zero;
            }
        }

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public bool Equals(Point other)
        {
            return this.Equals(ref other);
        }

        public bool Equals(ref Point other)
        {
            if (this.X == other.X)
                return this.Y == other.Y;
            return false;
        }

        public override bool Equals(object obj)
        {
            bool flag = false;
            if (obj is Point)
                flag = this.Equals((Point)obj);
            return flag;
        }

        public override int GetHashCode()
        {
            return (17 * 29 + this.X.GetHashCode()) * 29 + this.Y.GetHashCode();
        }

        public override string ToString()
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            return string.Format((IFormatProvider)currentCulture, "{{X:{0} Y:{1}}}", (object)this.X.ToString((IFormatProvider)currentCulture), (object)this.Y.ToString((IFormatProvider)currentCulture));
        }

        public static bool operator ==(Point a, Point b)
        {
            return a.Equals(ref b);
        }

        public static bool operator !=(Point a, Point b)
        {
            return !a.Equals(ref b);
        }
    }
}
