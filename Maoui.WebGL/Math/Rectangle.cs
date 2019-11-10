using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace WebGl
{
    [DataContract]
    public struct Rectangle : IEquatable<Rectangle>
    {
        private static readonly Rectangle empty = new Rectangle();
        [DataMember]
        public int X;
        [DataMember]
        public int Y;
        [DataMember]
        public int Width;
        [DataMember]
        public int Height;

        public int Left
        {
            get
            {
                return this.X;
            }
        }

        public int Right
        {
            get
            {
                return this.X + this.Width;
            }
        }

        public int Top
        {
            get
            {
                return this.Y;
            }
        }

        public int Bottom
        {
            get
            {
                return this.Y + this.Height;
            }
        }

        public Point Location
        {
            get
            {
                return new Point(this.X, this.Y);
            }
            set
            {
                this.X = value.X;
                this.Y = value.Y;
            }
        }

        public Point Center
        {
            get
            {
                return new Point(this.X + this.Width / 2, this.Y + this.Height / 2);
            }
        }

        public static Rectangle Empty
        {
            get
            {
                return Rectangle.empty;
            }
        }

        public bool IsEmpty
        {
            get
            {
                if (this.Width == 0 && this.Height == 0 && this.X == 0)
                    return this.Y == 0;
                return false;
            }
        }

        public Rectangle(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public void Offset(Point amount)
        {
            this.X += amount.X;
            this.Y += amount.Y;
        }

        public void Offset(int offsetX, int offsetY)
        {
            this.X += offsetX;
            this.Y += offsetY;
        }

        public void Inflate(int horizontalAmount, int verticalAmount)
        {
            this.X -= horizontalAmount;
            this.Y -= verticalAmount;
            this.Width += horizontalAmount * 2;
            this.Height += verticalAmount * 2;
        }

        public bool Contains(int x, int y)
        {
            if (this.X <= x && x < this.X + this.Width && this.Y <= y)
                return y < this.Y + this.Height;
            return false;
        }

        public bool Contains(Point pt)
        {
            if (this.X <= pt.X && pt.X < this.X + this.Width && this.Y <= pt.Y)
                return pt.Y < this.Y + this.Height;
            return false;
        }

        public void Contains(ref Point pt, out bool result)
        {
            result = this.X <= pt.X && pt.X < this.X + this.Width && this.Y <= pt.Y && pt.Y < this.Y + this.Height;
        }

        public bool Contains(Rectangle rect)
        {
            if (this.X <= rect.X && rect.X + rect.Width <= this.X + this.Width && this.Y <= rect.Y)
                return rect.Y + rect.Height <= this.Y + this.Height;
            return false;
        }

        public void Contains(ref Rectangle rect, out bool result)
        {
            result = this.X <= rect.X && rect.X + rect.Width <= this.X + this.Width && this.Y <= rect.Y && rect.Y + rect.Height <= this.Y + this.Height;
        }

        public bool Intersects(Rectangle rect)
        {
            if (rect.X < this.X + this.Width && this.X < rect.X + rect.Width && rect.Y < this.Y + this.Height)
                return this.Y < rect.Y + rect.Height;
            return false;
        }

        public void Intersects(ref Rectangle rect, out bool result)
        {
            result = rect.X < this.X + this.Width && this.X < rect.X + rect.Width && rect.Y < this.Y + this.Height && this.Y < rect.Y + rect.Height;
        }

        public Vector4 ToVector4()
        {
            return new Vector4((float)this.X, (float)this.Y, (float)this.Width, (float)this.Height);
        }

        public void ToVector4(ref Vector4 vector)
        {
            vector.X = (float)this.X;
            vector.Y = (float)this.Y;
            vector.Z = (float)this.Width;
            vector.W = (float)this.Height;
        }

        public static Rectangle Truncate(RectangleF value)
        {
            return new Rectangle((int)value.X, (int)value.Y, (int)value.Width, (int)value.Height);
        }

        public static Rectangle Round(RectangleF value)
        {
            return new Rectangle((int)System.Math.Round((double)value.X), (int)System.Math.Round((double)value.Y), (int)System.Math.Round((double)value.Width), (int)System.Math.Round((double)value.Height));
        }

        public static Rectangle Intersect(Rectangle a, Rectangle b)
        {
            int num1 = a.X + a.Width;
            int num2 = b.X + b.Width;
            int num3 = a.Y + a.Height;
            int num4 = b.Y + b.Height;
            int num5 = a.X > b.X ? a.X : b.X;
            int num6 = a.Y > b.Y ? a.Y : b.Y;
            int num7 = num1 < num2 ? num1 : num2;
            int num8 = num3 < num4 ? num3 : num4;
            if (num7 > num5 && num8 > num6)
            {
                Rectangle rectangle;
                rectangle.X = num5;
                rectangle.Y = num6;
                rectangle.Width = num7 - num5;
                rectangle.Height = num8 - num6;
                return rectangle;
            }
            Rectangle rectangle1;
            rectangle1.X = 0;
            rectangle1.Y = 0;
            rectangle1.Width = 0;
            rectangle1.Height = 0;
            return rectangle1;
        }

        public static void Intersect(ref Rectangle a, ref Rectangle b, out Rectangle result)
        {
            int num1 = a.X + a.Width;
            int num2 = b.X + b.Width;
            int num3 = a.Y + a.Height;
            int num4 = b.Y + b.Height;
            int num5 = a.X > b.X ? a.X : b.X;
            int num6 = a.Y > b.Y ? a.Y : b.Y;
            int num7 = num1 < num2 ? num1 : num2;
            int num8 = num3 < num4 ? num3 : num4;
            if (num7 > num5 && num8 > num6)
            {
                result.X = num5;
                result.Y = num6;
                result.Width = num7 - num5;
                result.Height = num8 - num6;
            }
            else
            {
                result.X = 0;
                result.Y = 0;
                result.Width = 0;
                result.Height = 0;
            }
        }

        public static Rectangle Union(Rectangle a, Rectangle b)
        {
            int num1 = a.X + a.Width;
            int num2 = b.X + b.Width;
            int num3 = a.Y + a.Height;
            int num4 = b.Y + b.Height;
            int num5 = a.X < b.X ? a.X : b.X;
            int num6 = a.Y < b.Y ? a.Y : b.Y;
            int num7 = num1 > num2 ? num1 : num2;
            int num8 = num3 > num4 ? num3 : num4;
            Rectangle rectangle;
            rectangle.X = num5;
            rectangle.Y = num6;
            rectangle.Width = num7 - num5;
            rectangle.Height = num8 - num6;
            return rectangle;
        }

        public static void Union(ref Rectangle a, ref Rectangle b, out Rectangle result)
        {
            int num1 = a.X + a.Width;
            int num2 = b.X + b.Width;
            int num3 = a.Y + a.Height;
            int num4 = b.Y + b.Height;
            int num5 = a.X < b.X ? a.X : b.X;
            int num6 = a.Y < b.Y ? a.Y : b.Y;
            int num7 = num1 > num2 ? num1 : num2;
            int num8 = num3 > num4 ? num3 : num4;
            result.X = num5;
            result.Y = num6;
            result.Width = num7 - num5;
            result.Height = num8 - num6;
        }

        public bool Equals(Rectangle other)
        {
            return this.Equals(ref other);
        }

        public bool Equals(ref Rectangle other)
        {
            if (this.X == other.X && this.Y == other.Y && this.Width == other.Width)
                return this.Height == other.Height;
            return false;
        }

        public override bool Equals(object obj)
        {
            bool flag = false;
            if (obj is Rectangle)
                flag = this.Equals((Rectangle)obj);
            return flag;
        }

        public override string ToString()
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            return string.Format((IFormatProvider)currentCulture, "{{X:{0} Y:{1} Width:{2} Height:{3}}}", (object)this.X.ToString((IFormatProvider)currentCulture), (object)this.Y.ToString((IFormatProvider)currentCulture), (object)this.Width.ToString((IFormatProvider)currentCulture), (object)this.Height.ToString((IFormatProvider)currentCulture));
        }

        public override int GetHashCode()
        {
            return (((17 * 29 + this.X.GetHashCode()) * 29 + this.Y.GetHashCode()) * 29 + this.Width.GetHashCode()) * 29 + this.Height.GetHashCode();
        }

        public static bool operator ==(Rectangle left, Rectangle right)
        {
            return left.Equals(ref right);
        }

        public static bool operator !=(Rectangle left, Rectangle right)
        {
            return !left.Equals(ref right);
        }
    }
}
