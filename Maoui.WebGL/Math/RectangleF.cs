using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace WebGl
{
    [DataContract]
    public struct RectangleF : IEquatable<RectangleF>
    {
        private static readonly RectangleF empty = new RectangleF();
        [DataMember]
        public float X;
        [DataMember]
        public float Y;
        [DataMember]
        public float Width;
        [DataMember]
        public float Height;

        public float Left
        {
            get
            {
                return this.X;
            }
        }

        public float Right
        {
            get
            {
                return this.X + this.Width;
            }
        }

        public float Top
        {
            get
            {
                return this.Y;
            }
        }

        public float Bottom
        {
            get
            {
                return this.Y + this.Height;
            }
        }

        public Vector2 Location
        {
            get
            {
                return new Vector2(this.X, this.Y);
            }
            set
            {
                this.X = value.X;
                this.Y = value.Y;
            }
        }

        public Vector2 Center
        {
            get
            {
                return new Vector2(this.X + this.Width / 2f, this.Y + this.Height / 2f);
            }
        }

        public static RectangleF Empty
        {
            get
            {
                return RectangleF.empty;
            }
        }

        public bool IsEmpty
        {
            get
            {
                if ((double)this.Width == 0.0 && (double)this.Height == 0.0 && (double)this.X == 0.0)
                    return (double)this.Y == 0.0;
                return false;
            }
        }

        public RectangleF(float x, float y, float width, float height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public void Offset(Vector2 amount)
        {
            this.X += amount.X;
            this.Y += amount.Y;
        }

        public void Offset(float offsetX, float offsetY)
        {
            this.X += offsetX;
            this.Y += offsetY;
        }

        public void Inflate(float horizontalAmount, float verticalAmount)
        {
            this.X -= horizontalAmount;
            this.Y -= verticalAmount;
            this.Width += horizontalAmount * 2f;
            this.Height += verticalAmount * 2f;
        }

        public bool Contains(float x, float y)
        {
            if ((double)this.X <= (double)x && (double)x < (double)this.X + (double)this.Width && (double)this.Y <= (double)y)
                return (double)y < (double)this.Y + (double)this.Height;
            return false;
        }

        public bool Contains(Vector2 pt)
        {
            if ((double)this.X <= (double)pt.X && (double)pt.X < (double)this.X + (double)this.Width && (double)this.Y <= (double)pt.Y)
                return (double)pt.Y < (double)this.Y + (double)this.Height;
            return false;
        }

        public void Contains(ref Vector2 pt, out bool result)
        {
            result = (double)this.X <= (double)pt.X && (double)pt.X < (double)this.X + (double)this.Width && (double)this.Y <= (double)pt.Y && (double)pt.Y < (double)this.Y + (double)this.Height;
        }

        public bool Contains(RectangleF rect)
        {
            if ((double)this.X <= (double)rect.X && (double)rect.X + (double)rect.Width <= (double)this.X + (double)this.Width && (double)this.Y <= (double)rect.Y)
                return (double)rect.Y + (double)rect.Height <= (double)this.Y + (double)this.Height;
            return false;
        }

        public void Contains(ref RectangleF rect, out bool result)
        {
            result = (double)this.X <= (double)rect.X && (double)rect.X + (double)rect.Width <= (double)this.X + (double)this.Width && (double)this.Y <= (double)rect.Y && (double)rect.Y + (double)rect.Height <= (double)this.Y + (double)this.Height;
        }

        public bool Intersects(RectangleF rect)
        {
            if ((double)rect.X < (double)this.X + (double)this.Width && (double)this.X < (double)rect.X + (double)rect.Width && (double)rect.Y < (double)this.Y + (double)this.Height)
                return (double)this.Y < (double)rect.Y + (double)rect.Height;
            return false;
        }

        public void Intersects(ref RectangleF rect, out bool result)
        {
            result = (double)rect.X < (double)this.X + (double)this.Width && (double)this.X < (double)rect.X + (double)rect.Width && (double)rect.Y < (double)this.Y + (double)this.Height && (double)this.Y < (double)rect.Y + (double)rect.Height;
        }

        public Vector2[] GetPoints()
        {
            return new Vector2[4]
            {
        new Vector2(this.Left, this.Top),
        new Vector2(this.Right, this.Top),
        new Vector2(this.Right, this.Bottom),
        new Vector2(this.Left, this.Bottom)
            };
        }

        public Vector4 ToVector4()
        {
            return new Vector4(this.X, this.Y, this.Width, this.Height);
        }

        public void ToVector4(ref Vector4 vector)
        {
            vector.X = this.X;
            vector.Y = this.Y;
            vector.Z = this.Width;
            vector.W = this.Height;
        }

        public static RectangleF Intersect(RectangleF a, RectangleF b)
        {
            float num1 = a.X + a.Width;
            float num2 = b.X + b.Width;
            float num3 = a.Y + a.Height;
            float num4 = b.Y + b.Height;
            float num5 = (double)a.X > (double)b.X ? a.X : b.X;
            float num6 = (double)a.Y > (double)b.Y ? a.Y : b.Y;
            float num7 = (double)num1 < (double)num2 ? num1 : num2;
            float num8 = (double)num3 < (double)num4 ? num3 : num4;
            if ((double)num7 > (double)num5 && (double)num8 > (double)num6)
            {
                RectangleF rectangleF;
                rectangleF.X = num5;
                rectangleF.Y = num6;
                rectangleF.Width = num7 - num5;
                rectangleF.Height = num8 - num6;
                return rectangleF;
            }
            RectangleF rectangleF1;
            rectangleF1.X = 0.0f;
            rectangleF1.Y = 0.0f;
            rectangleF1.Width = 0.0f;
            rectangleF1.Height = 0.0f;
            return rectangleF1;
        }

        public static void Intersect(ref RectangleF a, ref RectangleF b, out RectangleF result)
        {
            float num1 = a.X + a.Width;
            float num2 = b.X + b.Width;
            float num3 = a.Y + a.Height;
            float num4 = b.Y + b.Height;
            float num5 = (double)a.X > (double)b.X ? a.X : b.X;
            float num6 = (double)a.Y > (double)b.Y ? a.Y : b.Y;
            float num7 = (double)num1 < (double)num2 ? num1 : num2;
            float num8 = (double)num3 < (double)num4 ? num3 : num4;
            if ((double)num7 > (double)num5 && (double)num8 > (double)num6)
            {
                result.X = num5;
                result.Y = num6;
                result.Width = num7 - num5;
                result.Height = num8 - num6;
            }
            else
            {
                result.X = 0.0f;
                result.Y = 0.0f;
                result.Width = 0.0f;
                result.Height = 0.0f;
            }
        }

        public static RectangleF Union(RectangleF a, RectangleF b)
        {
            float num1 = a.X + a.Width;
            float num2 = b.X + b.Width;
            float num3 = a.Y + a.Height;
            float num4 = b.Y + b.Height;
            float num5 = (double)a.X < (double)b.X ? a.X : b.X;
            float num6 = (double)a.Y < (double)b.Y ? a.Y : b.Y;
            float num7 = (double)num1 > (double)num2 ? num1 : num2;
            float num8 = (double)num3 > (double)num4 ? num3 : num4;
            RectangleF rectangleF;
            rectangleF.X = num5;
            rectangleF.Y = num6;
            rectangleF.Width = num7 - num5;
            rectangleF.Height = num8 - num6;
            return rectangleF;
        }

        public static void Union(ref RectangleF a, ref RectangleF b, out RectangleF result)
        {
            float num1 = a.X + a.Width;
            float num2 = b.X + b.Width;
            float num3 = a.Y + a.Height;
            float num4 = b.Y + b.Height;
            float num5 = (double)a.X < (double)b.X ? a.X : b.X;
            float num6 = (double)a.Y < (double)b.Y ? a.Y : b.Y;
            float num7 = (double)num1 > (double)num2 ? num1 : num2;
            float num8 = (double)num3 > (double)num4 ? num3 : num4;
            result.X = num5;
            result.Y = num6;
            result.Width = num7 - num5;
            result.Height = num8 - num6;
        }

        public bool Equals(RectangleF other)
        {
            return this.Equals(ref other);
        }

        public bool Equals(ref RectangleF other)
        {
            if ((double)this.X == (double)other.X && (double)this.Y == (double)other.Y && (double)this.Width == (double)other.Width)
                return (double)this.Height == (double)other.Height;
            return false;
        }

        public override bool Equals(object obj)
        {
            bool flag = false;
            if (obj is RectangleF)
                flag = this.Equals((RectangleF)obj);
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

        public static bool operator ==(RectangleF left, RectangleF right)
        {
            return left.Equals(ref right);
        }

        public static bool operator !=(RectangleF left, RectangleF right)
        {
            return !left.Equals(ref right);
        }

        public static implicit operator RectangleF(Rectangle r)
        {
            return new RectangleF((float)r.X, (float)r.Y, (float)r.Width, (float)r.Height);
        }
    }
}
