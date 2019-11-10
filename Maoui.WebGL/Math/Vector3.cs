using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WebGl
{
    [DataContract(Name = "Vector3", Namespace = "WaveEngine.Common.Math")]
    [XmlType]
    [StructLayout(LayoutKind.Explicit, Size = 12)]
    public struct Vector3 : IEquatable<Vector3>
    {
        private static readonly Vector3 zero = new Vector3();
        private static readonly Vector3 one = new Vector3(1f, 1f, 1f);
        private static readonly Vector3 unitX = new Vector3(1f, 0.0f, 0.0f);
        private static readonly Vector3 unitY = new Vector3(0.0f, 1f, 0.0f);
        private static readonly Vector3 unitZ = new Vector3(0.0f, 0.0f, 1f);
        private static readonly Vector3 up = new Vector3(0.0f, 1f, 0.0f);
        private static readonly Vector3 down = new Vector3(0.0f, -1f, 0.0f);
        private static readonly Vector3 right = new Vector3(1f, 0.0f, 0.0f);
        private static readonly Vector3 left = new Vector3(-1f, 0.0f, 0.0f);
        private static readonly Vector3 forward = new Vector3(0.0f, 0.0f, -1f);
        private static readonly Vector3 backward = new Vector3(0.0f, 0.0f, 1f);
        [DataMember]
        [XmlAttribute]
        [FieldOffset(0)]
        public float X;
        [DataMember]
        [XmlAttribute]
        [FieldOffset(4)]
        public float Y;
        [DataMember]
        [XmlAttribute]
        [FieldOffset(8)]
        public float Z;

        public static Vector3 Zero
        {
            get
            {
                return Vector3.zero;
            }
        }

        public static Vector3 One
        {
            get
            {
                return Vector3.one;
            }
        }

        public static Vector3 UnitX
        {
            get
            {
                return Vector3.unitX;
            }
        }

        public static Vector3 UnitY
        {
            get
            {
                return Vector3.unitY;
            }
        }

        public static Vector3 UnitZ
        {
            get
            {
                return Vector3.unitZ;
            }
        }

        public static Vector3 Up
        {
            get
            {
                return Vector3.up;
            }
        }

        public static Vector3 Down
        {
            get
            {
                return Vector3.down;
            }
        }

        public static Vector3 Right
        {
            get
            {
                return Vector3.right;
            }
        }

        public static Vector3 Left
        {
            get
            {
                return Vector3.left;
            }
        }

        public static Vector3 Forward
        {
            get
            {
                return Vector3.forward;
            }
        }

        public static Vector3 Backward
        {
            get
            {
                return Vector3.backward;
            }
        }

        public Vector3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Vector3(float value)
        {
            this.X = this.Y = this.Z = value;
        }

        public Vector3(Vector2 value, float z)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = z;
        }

        public override string ToString()
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            return string.Format((IFormatProvider)currentCulture, "{{X:{0} Y:{1} Z:{2}}}", (object)this.X.ToString((IFormatProvider)currentCulture), (object)this.Y.ToString((IFormatProvider)currentCulture), (object)this.Z.ToString((IFormatProvider)currentCulture));
        }

        public bool Equals(Vector3 other)
        {
            return this.Equals(ref other);
        }

        private bool Equals(ref Vector3 other)
        {
            if (this.X.Equal(other.X, 0.0001f) && this.Y.Equal(other.Y, 0.0001f))
                return this.Z.Equal(other.Z, 0.0001f);
            return false;
        }

        public override bool Equals(object obj)
        {
            bool flag = false;
            if (obj is Vector3)
                flag = this.Equals((Vector3)obj);
            return flag;
        }

        public override int GetHashCode()
        {
            return ((17 * 29 + this.X.GetHashCode()) * 29 + this.Y.GetHashCode()) * 29 + this.Z.GetHashCode();
        }

        public float Length()
        {
            return (float)System.Math.Sqrt((double)this.X * (double)this.X + (double)this.Y * (double)this.Y + (double)this.Z * (double)this.Z);
        }

        public float LengthSquared()
        {
            return (float)((double)this.X * (double)this.X + (double)this.Y * (double)this.Y + (double)this.Z * (double)this.Z);
        }

        public Vector2 ToVector2()
        {
            return new Vector2(this.X, this.Y);
        }

        public void ToVector2(out Vector2 result)
        {
            result.X = this.X;
            result.Y = this.Y;
        }

        public static float Distance(Vector3 value1, Vector3 value2)
        {
            float num1 = value1.X - value2.X;
            float num2 = value1.Y - value2.Y;
            float num3 = value1.Z - value2.Z;
            return (float)System.Math.Sqrt((double)num1 * (double)num1 + (double)num2 * (double)num2 + (double)num3 * (double)num3);
        }

        public static void FromQuaternion(ref Quaternion q, out Vector3 result)
        {
            result.X = (float)System.Math.Atan2(-2.0 * ((double)q.Y * (double)q.Z - (double)q.W * (double)q.X), (double)q.W * (double)q.W - (double)q.X * (double)q.X - (double)q.Y * (double)q.Y + (double)q.Z * (double)q.Z);
            result.Y = (float)System.Math.Asin(2.0 * ((double)q.X * (double)q.Z + (double)q.W * (double)q.Y));
            result.Z = (float)System.Math.Atan2(-2.0 * ((double)q.X * (double)q.Y - (double)q.W * (double)q.Z), (double)q.W * (double)q.W + (double)q.X * (double)q.X - (double)q.Y * (double)q.Y - (double)q.Z * (double)q.Z);
        }

        public static void Distance(ref Vector3 value1, ref Vector3 value2, out float result)
        {
            float num1 = value1.X - value2.X;
            float num2 = value1.Y - value2.Y;
            float num3 = value1.Z - value2.Z;
            float num4 = (float)((double)num1 * (double)num1 + (double)num2 * (double)num2 + (double)num3 * (double)num3);
            result = (float)System.Math.Sqrt((double)num4);
        }

        public static float DistanceSquared(Vector3 value1, Vector3 value2)
        {
            float num1 = value1.X - value2.X;
            float num2 = value1.Y - value2.Y;
            float num3 = value1.Z - value2.Z;
            return (float)((double)num1 * (double)num1 + (double)num2 * (double)num2 + (double)num3 * (double)num3);
        }

        public static void DistanceSquared(ref Vector3 value1, ref Vector3 value2, out float result)
        {
            float num1 = value1.X - value2.X;
            float num2 = value1.Y - value2.Y;
            float num3 = value1.Z - value2.Z;
            result = (float)((double)num1 * (double)num1 + (double)num2 * (double)num2 + (double)num3 * (double)num3);
        }

        public static float Dot(Vector3 vector1, Vector3 vector2)
        {
            return (float)((double)vector1.X * (double)vector2.X + (double)vector1.Y * (double)vector2.Y + (double)vector1.Z * (double)vector2.Z);
        }

        public static float Dot(ref Vector3 vector1, ref Vector3 vector2)
        {
            return (float)((double)vector1.X * (double)vector2.X + (double)vector1.Y * (double)vector2.Y + (double)vector1.Z * (double)vector2.Z);
        }

        public static void Dot(ref Vector3 vector1, ref Vector3 vector2, out float result)
        {
            result = (float)((double)vector1.X * (double)vector2.X + (double)vector1.Y * (double)vector2.Y + (double)vector1.Z * (double)vector2.Z);
        }

        public float Dot(Vector3 vector)
        {
            float result;
            Vector3.Dot(ref this, ref vector, out result);
            return result;
        }

        public void Dot(ref Vector3 vector, out float result)
        {
            Vector3.Dot(ref this, ref vector, out result);
        }

        public float Normalize()
        {
            float num = (float)System.Math.Sqrt((double)this.X * (double)this.X + (double)this.Y * (double)this.Y + (double)this.Z * (double)this.Z);
            if ((double)num > 0.0)
            {
                this.X /= num;
                this.Y /= num;
                this.Z /= num;
            }
            return num;
        }

        public static Vector3 Normalize(Vector3 value)
        {
            float num = (float)System.Math.Sqrt((double)value.X * (double)value.X + (double)value.Y * (double)value.Y + (double)value.Z * (double)value.Z);
            Vector3 vector3;
            if ((double)num > 0.0)
            {
                vector3.X = value.X / num;
                vector3.Y = value.Y / num;
                vector3.Z = value.Z / num;
            }
            else
            {
                vector3.X = value.X;
                vector3.Y = value.Y;
                vector3.Z = value.Z;
            }
            return vector3;
        }

        public static float Normalize(ref Vector3 value, out Vector3 result)
        {
            float num = (float)System.Math.Sqrt((double)value.X * (double)value.X + (double)value.Y * (double)value.Y + (double)value.Z * (double)value.Z);
            if ((double)num > 0.0)
            {
                result.X = value.X / num;
                result.Y = value.Y / num;
                result.Z = value.Z / num;
            }
            else
            {
                result.X = value.X;
                result.Y = value.Y;
                result.Z = value.Z;
            }
            return num;
        }

        public static Vector3 Cross(Vector3 vector1, Vector3 vector2)
        {
            Vector3 vector3;
            vector3.X = (float)((double)vector1.Y * (double)vector2.Z - (double)vector1.Z * (double)vector2.Y);
            vector3.Y = (float)((double)vector1.Z * (double)vector2.X - (double)vector1.X * (double)vector2.Z);
            vector3.Z = (float)((double)vector1.X * (double)vector2.Y - (double)vector1.Y * (double)vector2.X);
            return vector3;
        }

        public static void Cross(ref Vector3 vector1, ref Vector3 vector2, out Vector3 result)
        {
            float num1 = (float)((double)vector1.Y * (double)vector2.Z - (double)vector1.Z * (double)vector2.Y);
            float num2 = (float)((double)vector1.Z * (double)vector2.X - (double)vector1.X * (double)vector2.Z);
            float num3 = (float)((double)vector1.X * (double)vector2.Y - (double)vector1.Y * (double)vector2.X);
            result.X = num1;
            result.Y = num2;
            result.Z = num3;
        }

        public Vector3 Cross(Vector3 vector)
        {
            Vector3 result;
            Vector3.Cross(ref this, ref vector, out result);
            return result;
        }

        public void Cross(ref Vector3 vector, out Vector3 result)
        {
            Vector3.Cross(ref this, ref vector, out result);
        }

        public static Vector3 Reflect(Vector3 vector, Vector3 normal)
        {
            float num = (float)((double)vector.X * (double)normal.X + (double)vector.Y * (double)normal.Y + (double)vector.Z * (double)normal.Z);
            Vector3 vector3;
            vector3.X = vector.X - 2f * num * normal.X;
            vector3.Y = vector.Y - 2f * num * normal.Y;
            vector3.Z = vector.Z - 2f * num * normal.Z;
            return vector3;
        }

        public static void Reflect(ref Vector3 vector, ref Vector3 normal, out Vector3 result)
        {
            float num = (float)((double)vector.X * (double)normal.X + (double)vector.Y * (double)normal.Y + (double)vector.Z * (double)normal.Z);
            result.X = vector.X - 2f * num * normal.X;
            result.Y = vector.Y - 2f * num * normal.Y;
            result.Z = vector.Z - 2f * num * normal.Z;
        }

        public Vector3 Reflect(Vector3 normal)
        {
            Vector3 result;
            Vector3.Reflect(ref this, ref normal, out result);
            return result;
        }

        public void Reflect(ref Vector3 normal, out Vector3 result)
        {
            Vector3.Reflect(ref this, ref normal, out result);
        }

        public static Vector3 Min(Vector3 value1, Vector3 value2)
        {
            Vector3 vector3;
            vector3.X = (double)value1.X < (double)value2.X ? value1.X : value2.X;
            vector3.Y = (double)value1.Y < (double)value2.Y ? value1.Y : value2.Y;
            vector3.Z = (double)value1.Z < (double)value2.Z ? value1.Z : value2.Z;
            return vector3;
        }

        public static void Min(ref Vector3 value1, ref Vector3 value2, out Vector3 result)
        {
            result.X = (double)value1.X < (double)value2.X ? value1.X : value2.X;
            result.Y = (double)value1.Y < (double)value2.Y ? value1.Y : value2.Y;
            result.Z = (double)value1.Z < (double)value2.Z ? value1.Z : value2.Z;
        }

        public static Vector3 Max(Vector3 value1, Vector3 value2)
        {
            Vector3 vector3;
            vector3.X = (double)value1.X > (double)value2.X ? value1.X : value2.X;
            vector3.Y = (double)value1.Y > (double)value2.Y ? value1.Y : value2.Y;
            vector3.Z = (double)value1.Z > (double)value2.Z ? value1.Z : value2.Z;
            return vector3;
        }

        public static void Max(ref Vector3 value1, ref Vector3 value2, out Vector3 result)
        {
            result.X = (double)value1.X > (double)value2.X ? value1.X : value2.X;
            result.Y = (double)value1.Y > (double)value2.Y ? value1.Y : value2.Y;
            result.Z = (double)value1.Z > (double)value2.Z ? value1.Z : value2.Z;
        }

        public static Vector3 Abs(Vector3 value)
        {
            Vector3 vector3;
            vector3.X = System.Math.Abs(value.X);
            vector3.Y = System.Math.Abs(value.Y);
            vector3.Z = System.Math.Abs(value.Z);
            return vector3;
        }

        public static void Abs(ref Vector3 value, out Vector3 result)
        {
            result.X = System.Math.Abs(value.X);
            result.Y = System.Math.Abs(value.Y);
            result.Z = System.Math.Abs(value.Z);
        }

        public static Vector3 Clamp(Vector3 value1, Vector3 min, Vector3 max)
        {
            float x = value1.X;
            float num1 = (double)x > (double)max.X ? max.X : x;
            float num2 = (double)num1 < (double)min.X ? min.X : num1;
            float y = value1.Y;
            float num3 = (double)y > (double)max.Y ? max.Y : y;
            float num4 = (double)num3 < (double)min.Y ? min.Y : num3;
            float z = value1.Z;
            float num5 = (double)z > (double)max.Z ? max.Z : z;
            float num6 = (double)num5 < (double)min.Z ? min.Z : num5;
            Vector3 vector3;
            vector3.X = num2;
            vector3.Y = num4;
            vector3.Z = num6;
            return vector3;
        }

        public static void Clamp(
          ref Vector3 value1,
          ref Vector3 min,
          ref Vector3 max,
          out Vector3 result)
        {
            float x = value1.X;
            float num1 = (double)x > (double)max.X ? max.X : x;
            float num2 = (double)num1 < (double)min.X ? min.X : num1;
            float y = value1.Y;
            float num3 = (double)y > (double)max.Y ? max.Y : y;
            float num4 = (double)num3 < (double)min.Y ? min.Y : num3;
            float z = value1.Z;
            float num5 = (double)z > (double)max.Z ? max.Z : z;
            float num6 = (double)num5 < (double)min.Z ? min.Z : num5;
            result.X = num2;
            result.Y = num4;
            result.Z = num6;
        }

        public static Vector3 Lerp(Vector3 value1, Vector3 value2, float amount)
        {
            Vector3 vector3;
            vector3.X = value1.X + (value2.X - value1.X) * amount;
            vector3.Y = value1.Y + (value2.Y - value1.Y) * amount;
            vector3.Z = value1.Z + (value2.Z - value1.Z) * amount;
            return vector3;
        }

        public static void Lerp(
          ref Vector3 value1,
          ref Vector3 value2,
          float amount,
          out Vector3 result)
        {
            result.X = value1.X + (value2.X - value1.X) * amount;
            result.Y = value1.Y + (value2.Y - value1.Y) * amount;
            result.Z = value1.Z + (value2.Z - value1.Z) * amount;
        }

        public static Vector3 Barycentric(
          Vector3 value1,
          Vector3 value2,
          Vector3 value3,
          float amount1,
          float amount2)
        {
            Vector3 vector3;
            vector3.X = (float)((double)value1.X + (double)amount1 * ((double)value2.X - (double)value1.X) + (double)amount2 * ((double)value3.X - (double)value1.X));
            vector3.Y = (float)((double)value1.Y + (double)amount1 * ((double)value2.Y - (double)value1.Y) + (double)amount2 * ((double)value3.Y - (double)value1.Y));
            vector3.Z = (float)((double)value1.Z + (double)amount1 * ((double)value2.Z - (double)value1.Z) + (double)amount2 * ((double)value3.Z - (double)value1.Z));
            return vector3;
        }

        public static void Barycentric(
          ref Vector3 value1,
          ref Vector3 value2,
          ref Vector3 value3,
          float amount1,
          float amount2,
          out Vector3 result)
        {
            result.X = (float)((double)value1.X + (double)amount1 * ((double)value2.X - (double)value1.X) + (double)amount2 * ((double)value3.X - (double)value1.X));
            result.Y = (float)((double)value1.Y + (double)amount1 * ((double)value2.Y - (double)value1.Y) + (double)amount2 * ((double)value3.Y - (double)value1.Y));
            result.Z = (float)((double)value1.Z + (double)amount1 * ((double)value2.Z - (double)value1.Z) + (double)amount2 * ((double)value3.Z - (double)value1.Z));
        }

        public static Vector3 SmoothStep(Vector3 value1, Vector3 value2, float amount)
        {
            amount = (double)amount > 1.0 ? 1f : ((double)amount < 0.0 ? 0.0f : amount);
            amount = (float)((double)amount * (double)amount * (3.0 - 2.0 * (double)amount));
            Vector3 vector3;
            vector3.X = value1.X + (value2.X - value1.X) * amount;
            vector3.Y = value1.Y + (value2.Y - value1.Y) * amount;
            vector3.Z = value1.Z + (value2.Z - value1.Z) * amount;
            return vector3;
        }

        public static void SmoothStep(
          ref Vector3 value1,
          ref Vector3 value2,
          float amount,
          out Vector3 result)
        {
            amount = (double)amount > 1.0 ? 1f : ((double)amount < 0.0 ? 0.0f : amount);
            amount = (float)((double)amount * (double)amount * (3.0 - 2.0 * (double)amount));
            result.X = value1.X + (value2.X - value1.X) * amount;
            result.Y = value1.Y + (value2.Y - value1.Y) * amount;
            result.Z = value1.Z + (value2.Z - value1.Z) * amount;
        }

        public static Vector3 SmoothDamp(
          Vector3 current,
          Vector3 target,
          ref Vector3 currentVelocity,
          float smoothTime,
          float gameTime)
        {
            smoothTime = MathHelper.Max(0.0001f, smoothTime);
            float num1 = 2f / smoothTime;
            float num2 = num1 * gameTime;
            float num3 = (float)(1.0 / (1.0 + (double)num2 + 0.479999989271164 * (double)num2 * (double)num2 + 0.234999999403954 * (double)num2 * (double)num2 * (double)num2));
            Vector3 vector3_1 = current - target;
            Vector3 vector3_2 = target;
            target = current - vector3_1;
            Vector3 vector3_3 = (currentVelocity + num1 * vector3_1) * gameTime;
            currentVelocity = (currentVelocity - num1 * vector3_3) * num3;
            Vector3 vector3_4 = target + (vector3_1 + vector3_3) * num3;
            if ((double)Vector3.Dot(vector3_2 - current, vector3_4 - vector3_2) > 0.0)
            {
                vector3_4 = vector3_2;
                currentVelocity = (vector3_4 - vector3_2) / gameTime;
            }
            return vector3_4;
        }

        public static Vector3 SmoothDamp(
          Vector3 current,
          Vector3 target,
          ref Vector3 currentVelocity,
          float smoothTime,
          float maxSpeed,
          float gameTime)
        {
            smoothTime = MathHelper.Max(0.0001f, smoothTime);
            float num1 = 2f / smoothTime;
            float num2 = num1 * gameTime;
            float num3 = (float)(1.0 / (1.0 + (double)num2 + 0.479999989271164 * (double)num2 * (double)num2 + 0.234999999403954 * (double)num2 * (double)num2 * (double)num2));
            Vector3 vector = current - target;
            Vector3 vector3_1 = target;
            Vector3 vector3_2 = Vector3.ClampMagnitude(vector, maxSpeed * smoothTime);
            target = current - vector3_2;
            Vector3 vector3_3 = (currentVelocity + num1 * vector3_2) * gameTime;
            currentVelocity = (currentVelocity - num1 * vector3_3) * num3;
            Vector3 vector3_4 = target + (vector3_2 + vector3_3) * num3;
            if ((double)Vector3.Dot(vector3_1 - current, vector3_4 - vector3_1) > 0.0)
            {
                vector3_4 = vector3_1;
                currentVelocity = (vector3_4 - vector3_1) / gameTime;
            }
            return vector3_4;
        }

        public static Vector3 ClampMagnitude(Vector3 vector, float maxLength)
        {
            if ((double)vector.LengthSquared() <= (double)maxLength * (double)maxLength)
                return vector;
            double num = (double)vector.Normalize();
            return vector * maxLength;
        }

        public static Vector3 CatmullRom(
          Vector3 value1,
          Vector3 value2,
          Vector3 value3,
          Vector3 value4,
          float amount)
        {
            float num1 = amount * amount;
            float num2 = amount * num1;
            Vector3 vector3;
            vector3.X = (float)(0.5 * (2.0 * (double)value2.X + (-(double)value1.X + (double)value3.X) * (double)amount + (2.0 * (double)value1.X - 5.0 * (double)value2.X + 4.0 * (double)value3.X - (double)value4.X) * (double)num1 + (-(double)value1.X + 3.0 * (double)value2.X - 3.0 * (double)value3.X + (double)value4.X) * (double)num2));
            vector3.Y = (float)(0.5 * (2.0 * (double)value2.Y + (-(double)value1.Y + (double)value3.Y) * (double)amount + (2.0 * (double)value1.Y - 5.0 * (double)value2.Y + 4.0 * (double)value3.Y - (double)value4.Y) * (double)num1 + (-(double)value1.Y + 3.0 * (double)value2.Y - 3.0 * (double)value3.Y + (double)value4.Y) * (double)num2));
            vector3.Z = (float)(0.5 * (2.0 * (double)value2.Z + (-(double)value1.Z + (double)value3.Z) * (double)amount + (2.0 * (double)value1.Z - 5.0 * (double)value2.Z + 4.0 * (double)value3.Z - (double)value4.Z) * (double)num1 + (-(double)value1.Z + 3.0 * (double)value2.Z - 3.0 * (double)value3.Z + (double)value4.Z) * (double)num2));
            return vector3;
        }

        public static void CatmullRom(
          ref Vector3 value1,
          ref Vector3 value2,
          ref Vector3 value3,
          ref Vector3 value4,
          float amount,
          out Vector3 result)
        {
            float num1 = amount * amount;
            float num2 = amount * num1;
            result.X = (float)(0.5 * (2.0 * (double)value2.X + (-(double)value1.X + (double)value3.X) * (double)amount + (2.0 * (double)value1.X - 5.0 * (double)value2.X + 4.0 * (double)value3.X - (double)value4.X) * (double)num1 + (-(double)value1.X + 3.0 * (double)value2.X - 3.0 * (double)value3.X + (double)value4.X) * (double)num2));
            result.Y = (float)(0.5 * (2.0 * (double)value2.Y + (-(double)value1.Y + (double)value3.Y) * (double)amount + (2.0 * (double)value1.Y - 5.0 * (double)value2.Y + 4.0 * (double)value3.Y - (double)value4.Y) * (double)num1 + (-(double)value1.Y + 3.0 * (double)value2.Y - 3.0 * (double)value3.Y + (double)value4.Y) * (double)num2));
            result.Z = (float)(0.5 * (2.0 * (double)value2.Z + (-(double)value1.Z + (double)value3.Z) * (double)amount + (2.0 * (double)value1.Z - 5.0 * (double)value2.Z + 4.0 * (double)value3.Z - (double)value4.Z) * (double)num1 + (-(double)value1.Z + 3.0 * (double)value2.Z - 3.0 * (double)value3.Z + (double)value4.Z) * (double)num2));
        }

        public static Vector3 Hermite(
          Vector3 value1,
          Vector3 tangent1,
          Vector3 value2,
          Vector3 tangent2,
          float amount)
        {
            float num1 = amount * amount;
            float num2 = amount * num1;
            float num3 = (float)(2.0 * (double)num2 - 3.0 * (double)num1 + 1.0);
            float num4 = (float)(-2.0 * (double)num2 + 3.0 * (double)num1);
            float num5 = num2 - 2f * num1 + amount;
            float num6 = num2 - num1;
            Vector3 vector3;
            vector3.X = (float)((double)value1.X * (double)num3 + (double)value2.X * (double)num4 + (double)tangent1.X * (double)num5 + (double)tangent2.X * (double)num6);
            vector3.Y = (float)((double)value1.Y * (double)num3 + (double)value2.Y * (double)num4 + (double)tangent1.Y * (double)num5 + (double)tangent2.Y * (double)num6);
            vector3.Z = (float)((double)value1.Z * (double)num3 + (double)value2.Z * (double)num4 + (double)tangent1.Z * (double)num5 + (double)tangent2.Z * (double)num6);
            return vector3;
        }

        public static void Hermite(
          ref Vector3 value1,
          ref Vector3 tangent1,
          ref Vector3 value2,
          ref Vector3 tangent2,
          float amount,
          out Vector3 result)
        {
            float num1 = amount * amount;
            float num2 = amount * num1;
            float num3 = (float)(2.0 * (double)num2 - 3.0 * (double)num1 + 1.0);
            float num4 = (float)(-2.0 * (double)num2 + 3.0 * (double)num1);
            float num5 = num2 - 2f * num1 + amount;
            float num6 = num2 - num1;
            result.X = (float)((double)value1.X * (double)num3 + (double)value2.X * (double)num4 + (double)tangent1.X * (double)num5 + (double)tangent2.X * (double)num6);
            result.Y = (float)((double)value1.Y * (double)num3 + (double)value2.Y * (double)num4 + (double)tangent1.Y * (double)num5 + (double)tangent2.Y * (double)num6);
            result.Z = (float)((double)value1.Z * (double)num3 + (double)value2.Z * (double)num4 + (double)tangent1.Z * (double)num5 + (double)tangent2.Z * (double)num6);
        }

        public static Vector3 Transform(Vector3 position, Matrix matrix)
        {
            float num1 = (float)((double)position.X * (double)matrix.M11 + (double)position.Y * (double)matrix.M21 + (double)position.Z * (double)matrix.M31) + matrix.M41;
            float num2 = (float)((double)position.X * (double)matrix.M12 + (double)position.Y * (double)matrix.M22 + (double)position.Z * (double)matrix.M32) + matrix.M42;
            float num3 = (float)((double)position.X * (double)matrix.M13 + (double)position.Y * (double)matrix.M23 + (double)position.Z * (double)matrix.M33) + matrix.M43;
            Vector3 vector3;
            vector3.X = num1;
            vector3.Y = num2;
            vector3.Z = num3;
            return vector3;
        }

        public static void Transform(ref Vector3 position, ref Matrix matrix, out Vector3 result)
        {
            float num1 = (float)((double)position.X * (double)matrix.M11 + (double)position.Y * (double)matrix.M21 + (double)position.Z * (double)matrix.M31) + matrix.M41;
            float num2 = (float)((double)position.X * (double)matrix.M12 + (double)position.Y * (double)matrix.M22 + (double)position.Z * (double)matrix.M32) + matrix.M42;
            float num3 = (float)((double)position.X * (double)matrix.M13 + (double)position.Y * (double)matrix.M23 + (double)position.Z * (double)matrix.M33) + matrix.M43;
            result.X = num1;
            result.Y = num2;
            result.Z = num3;
        }

        //public static unsafe void Transform(Vector3* position, ref Matrix matrix, Vector3* result)
        //{
        //  float num1 = (float) ((double) position->X * (double) matrix.M11 + (double) position->Y * (double) matrix.M21 + (double) position->Z * (double) matrix.M31) + matrix.M41;
        //  float num2 = (float) ((double) position->X * (double) matrix.M12 + (double) position->Y * (double) matrix.M22 + (double) position->Z * (double) matrix.M32) + matrix.M42;
        //  float num3 = (float) ((double) position->X * (double) matrix.M13 + (double) position->Y * (double) matrix.M23 + (double) position->Z * (double) matrix.M33) + matrix.M43;
        //  result->X = num1;
        //  result->Y = num2;
        //  result->Z = num3;
        //}

        public static void Transform(Vector3[] positions, ref Matrix matrix, Vector3[] results)
        {
            for (int index = 0; index < positions.Length; ++index)
                Vector3.Transform(ref positions[index], ref matrix, out results[index]);
        }

        public static Vector3 TransformNormal(Vector3 normal, Matrix matrix)
        {
            float num1 = (float)((double)normal.X * (double)matrix.M11 + (double)normal.Y * (double)matrix.M21 + (double)normal.Z * (double)matrix.M31);
            float num2 = (float)((double)normal.X * (double)matrix.M12 + (double)normal.Y * (double)matrix.M22 + (double)normal.Z * (double)matrix.M32);
            float num3 = (float)((double)normal.X * (double)matrix.M13 + (double)normal.Y * (double)matrix.M23 + (double)normal.Z * (double)matrix.M33);
            Vector3 vector3;
            vector3.X = num1;
            vector3.Y = num2;
            vector3.Z = num3;
            return vector3;
        }

        public static void TransformNormal(ref Vector3 normal, ref Matrix matrix, out Vector3 result)
        {
            float num1 = (float)((double)normal.X * (double)matrix.M11 + (double)normal.Y * (double)matrix.M21 + (double)normal.Z * (double)matrix.M31);
            float num2 = (float)((double)normal.X * (double)matrix.M12 + (double)normal.Y * (double)matrix.M22 + (double)normal.Z * (double)matrix.M32);
            float num3 = (float)((double)normal.X * (double)matrix.M13 + (double)normal.Y * (double)matrix.M23 + (double)normal.Z * (double)matrix.M33);
            result.X = num1;
            result.Y = num2;
            result.Z = num3;
        }

        //public static unsafe void TransformNormal(Vector3* normal, ref Matrix matrix, Vector3* result)
        //{
        //  float num1 = (float) ((double) normal->X * (double) matrix.M11 + (double) normal->Y * (double) matrix.M21 + (double) normal->Z * (double) matrix.M31);
        //  float num2 = (float) ((double) normal->X * (double) matrix.M12 + (double) normal->Y * (double) matrix.M22 + (double) normal->Z * (double) matrix.M32);
        //  float num3 = (float) ((double) normal->X * (double) matrix.M13 + (double) normal->Y * (double) matrix.M23 + (double) normal->Z * (double) matrix.M33);
        //  result->X = num1;
        //  result->Y = num2;
        //  result->Z = num3;
        //}

        public static Vector3 Transform(Vector3 value, Quaternion rotation)
        {
            float num1 = rotation.X + rotation.X;
            float num2 = rotation.Y + rotation.Y;
            float num3 = rotation.Z + rotation.Z;
            float num4 = rotation.W * num1;
            float num5 = rotation.W * num2;
            float num6 = rotation.W * num3;
            float num7 = rotation.X * num1;
            float num8 = rotation.X * num2;
            float num9 = rotation.X * num3;
            float num10 = rotation.Y * num2;
            float num11 = rotation.Y * num3;
            float num12 = rotation.Z * num3;
            float num13 = (float)((double)value.X * (1.0 - (double)num10 - (double)num12) + (double)value.Y * ((double)num8 - (double)num6) + (double)value.Z * ((double)num9 + (double)num5));
            float num14 = (float)((double)value.X * ((double)num8 + (double)num6) + (double)value.Y * (1.0 - (double)num7 - (double)num12) + (double)value.Z * ((double)num11 - (double)num4));
            float num15 = (float)((double)value.X * ((double)num9 - (double)num5) + (double)value.Y * ((double)num11 + (double)num4) + (double)value.Z * (1.0 - (double)num7 - (double)num10));
            Vector3 vector3;
            vector3.X = num13;
            vector3.Y = num14;
            vector3.Z = num15;
            return vector3;
        }

        public static void Transform(ref Vector3 value, ref Quaternion rotation, out Vector3 result)
        {
            float num1 = rotation.X + rotation.X;
            float num2 = rotation.Y + rotation.Y;
            float num3 = rotation.Z + rotation.Z;
            float num4 = rotation.W * num1;
            float num5 = rotation.W * num2;
            float num6 = rotation.W * num3;
            float num7 = rotation.X * num1;
            float num8 = rotation.X * num2;
            float num9 = rotation.X * num3;
            float num10 = rotation.Y * num2;
            float num11 = rotation.Y * num3;
            float num12 = rotation.Z * num3;
            float num13 = (float)((double)value.X * (1.0 - (double)num10 - (double)num12) + (double)value.Y * ((double)num8 - (double)num6) + (double)value.Z * ((double)num9 + (double)num5));
            float num14 = (float)((double)value.X * ((double)num8 + (double)num6) + (double)value.Y * (1.0 - (double)num7 - (double)num12) + (double)value.Z * ((double)num11 - (double)num4));
            float num15 = (float)((double)value.X * ((double)num9 - (double)num5) + (double)value.Y * ((double)num11 + (double)num4) + (double)value.Z * (1.0 - (double)num7 - (double)num10));
            result.X = num13;
            result.Y = num14;
            result.Z = num15;
        }

        public static void TransformCoordinate(
          ref Vector3 coordinate,
          ref Matrix transform,
          out Vector3 result)
        {
            float num = (float)(1.0 / ((double)coordinate.X * (double)transform.M14 + (double)coordinate.Y * (double)transform.M24 + (double)coordinate.Z * (double)transform.M34 + (double)transform.M44));
            result = new Vector3(num * ((float)((double)coordinate.X * (double)transform.M11 + (double)coordinate.Y * (double)transform.M21 + (double)coordinate.Z * (double)transform.M31) + transform.M41), num * ((float)((double)coordinate.X * (double)transform.M12 + (double)coordinate.Y * (double)transform.M22 + (double)coordinate.Z * (double)transform.M32) + transform.M42), num * ((float)((double)coordinate.X * (double)transform.M13 + (double)coordinate.Y * (double)transform.M23 + (double)coordinate.Z * (double)transform.M33) + transform.M43));
        }

        public static Vector3 TransformCoordinate(Vector3 coordinate, Matrix transform)
        {
            Vector3 result;
            Vector3.TransformCoordinate(ref coordinate, ref transform, out result);
            return result;
        }

        public static Vector3 Negate(Vector3 value)
        {
            Vector3 vector3;
            vector3.X = -value.X;
            vector3.Y = -value.Y;
            vector3.Z = -value.Z;
            return vector3;
        }

        public static void Negate(ref Vector3 value, out Vector3 result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
        }

        public static Vector3 Add(Vector3 value1, Vector3 value2)
        {
            Vector3 vector3;
            vector3.X = value1.X + value2.X;
            vector3.Y = value1.Y + value2.Y;
            vector3.Z = value1.Z + value2.Z;
            return vector3;
        }

        public static void Add(ref Vector3 value1, ref Vector3 value2, out Vector3 result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
            result.Z = value1.Z + value2.Z;
        }

        //public static unsafe void Add(Vector3* value1, ref Vector3 value2, Vector3* result)
        //{
        //  result->X = value1->X + value2.X;
        //  result->Y = value1->Y + value2.Y;
        //  result->Z = value1->Z + value2.Z;
        //}

        public static Vector3 Subtract(Vector3 value1, Vector3 value2)
        {
            Vector3 vector3;
            vector3.X = value1.X - value2.X;
            vector3.Y = value1.Y - value2.Y;
            vector3.Z = value1.Z - value2.Z;
            return vector3;
        }

        public static void Subtract(ref Vector3 value1, ref Vector3 value2, out Vector3 result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
            result.Z = value1.Z - value2.Z;
        }

        public static Vector3 Multiply(Vector3 value1, Vector3 value2)
        {
            Vector3 vector3;
            vector3.X = value1.X * value2.X;
            vector3.Y = value1.Y * value2.Y;
            vector3.Z = value1.Z * value2.Z;
            return vector3;
        }

        public static void Multiply(ref Vector3 value1, ref Vector3 value2, out Vector3 result)
        {
            result.X = value1.X * value2.X;
            result.Y = value1.Y * value2.Y;
            result.Z = value1.Z * value2.Z;
        }

        public static Vector3 Multiply(Vector3 value1, float scaleFactor)
        {
            Vector3 vector3;
            vector3.X = value1.X * scaleFactor;
            vector3.Y = value1.Y * scaleFactor;
            vector3.Z = value1.Z * scaleFactor;
            return vector3;
        }

        public static void Multiply(ref Vector3 value1, float scaleFactor, out Vector3 result)
        {
            result.X = value1.X * scaleFactor;
            result.Y = value1.Y * scaleFactor;
            result.Z = value1.Z * scaleFactor;
        }

        //public static unsafe void Multiply(Vector3* value1, float scaleFactor, out Vector3 result)
        //{
        //  result.X = value1->X * scaleFactor;
        //  result.Y = value1->Y * scaleFactor;
        //  result.Z = value1->Z * scaleFactor;
        //}

        public static Vector3 Divide(Vector3 value1, Vector3 value2)
        {
            Vector3 vector3;
            vector3.X = value1.X / value2.X;
            vector3.Y = value1.Y / value2.Y;
            vector3.Z = value1.Z / value2.Z;
            return vector3;
        }

        public static void Divide(ref Vector3 value1, ref Vector3 value2, out Vector3 result)
        {
            result.X = value1.X / value2.X;
            result.Y = value1.Y / value2.Y;
            result.Z = value1.Z / value2.Z;
        }

        public static Vector3 Divide(Vector3 value1, float value2)
        {
            float num = 1f / value2;
            Vector3 vector3;
            vector3.X = value1.X * num;
            vector3.Y = value1.Y * num;
            vector3.Z = value1.Z * num;
            return vector3;
        }

        public static void Divide(ref Vector3 value1, float value2, out Vector3 result)
        {
            float num = 1f / value2;
            result.X = value1.X * num;
            result.Y = value1.Y * num;
            result.Z = value1.Z * num;
        }

        public static float Angle(Vector3 from, Vector3 to)
        {
            return Vector3.Angle(ref from, ref to);
        }

        public static float Angle(ref Vector3 from, ref Vector3 to)
        {
            Vector3 vector1 = from;
            Vector3 vector2 = to;
            double num1 = (double)vector1.Normalize();
            double num2 = (double)vector2.Normalize();
            float result = 0.0f;
            Vector3.Dot(ref vector1, ref vector2, out result);
            return (float)System.Math.Acos((double)MathHelper.Clamp(result, -1f, 1f));
        }

        public static Vector3 RotateAround(Vector3 point, Vector3 pivot, Quaternion rotation)
        {
            Vector3 rotatePoint;
            Vector3.RotateAround(ref point, ref pivot, ref rotation, out rotatePoint);
            return rotatePoint;
        }

        public static void RotateAround(
          ref Vector3 point,
          ref Vector3 pivot,
          ref Quaternion rotation,
          out Vector3 rotatePoint)
        {
            rotatePoint = rotation * (point - pivot) + pivot;
        }

        public static Vector3 operator -(Vector3 value)
        {
            Vector3 vector3;
            vector3.X = -value.X;
            vector3.Y = -value.Y;
            vector3.Z = -value.Z;
            return vector3;
        }

        public static bool operator ==(Vector3 value1, Vector3 value2)
        {
            return value1.Equals(ref value2);
        }

        public static bool operator !=(Vector3 value1, Vector3 value2)
        {
            return !value1.Equals(ref value2);
        }

        public static Vector3 operator +(Vector3 value1, Vector3 value2)
        {
            Vector3 vector3;
            vector3.X = value1.X + value2.X;
            vector3.Y = value1.Y + value2.Y;
            vector3.Z = value1.Z + value2.Z;
            return vector3;
        }

        public static Vector3 operator -(Vector3 value1, Vector3 value2)
        {
            Vector3 vector3;
            vector3.X = value1.X - value2.X;
            vector3.Y = value1.Y - value2.Y;
            vector3.Z = value1.Z - value2.Z;
            return vector3;
        }

        public static Vector3 operator *(Vector3 value1, Vector3 value2)
        {
            Vector3 vector3;
            vector3.X = value1.X * value2.X;
            vector3.Y = value1.Y * value2.Y;
            vector3.Z = value1.Z * value2.Z;
            return vector3;
        }

        public static Vector3 operator *(Vector3 value, float scaleFactor)
        {
            Vector3 vector3;
            vector3.X = value.X * scaleFactor;
            vector3.Y = value.Y * scaleFactor;
            vector3.Z = value.Z * scaleFactor;
            return vector3;
        }

        public static Vector3 operator *(float scaleFactor, Vector3 value)
        {
            Vector3 vector3;
            vector3.X = value.X * scaleFactor;
            vector3.Y = value.Y * scaleFactor;
            vector3.Z = value.Z * scaleFactor;
            return vector3;
        }

        public static Vector3 operator /(Vector3 value1, Vector3 value2)
        {
            Vector3 vector3;
            vector3.X = value1.X / value2.X;
            vector3.Y = value1.Y / value2.Y;
            vector3.Z = value1.Z / value2.Z;
            return vector3;
        }

        public static Vector3 operator /(Vector3 value, float divider)
        {
            float num = 1f / divider;
            Vector3 vector3;
            vector3.X = value.X * num;
            vector3.Y = value.Y * num;
            vector3.Z = value.Z * num;
            return vector3;
        }

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.X;
                    case 1:
                        return this.Y;
                    case 2:
                        return this.Z;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(index), "Indices for Vector3 run from 0 to 2, inclusive.");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.X = value;
                        break;
                    case 1:
                        this.Y = value;
                        break;
                    case 2:
                        this.Z = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(index), "Indices for Vector3 run from 0 to 2, inclusive.");
                }
            }
        }
    }
}
