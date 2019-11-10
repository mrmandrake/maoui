using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace WebGl
{
    [DataContract]
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public struct Vector2 : IEquatable<Vector2>
    {
        private static readonly Vector2 zero = new Vector2();
        private static readonly Vector2 one = new Vector2(1f, 1f);
        private static readonly Vector2 unitX = new Vector2(1f, 0.0f);
        private static readonly Vector2 unitY = new Vector2(0.0f, 1f);
        private static readonly Vector2 center = new Vector2(0.5f, 0.5f);
        [DataMember]
        [FieldOffset(0)]
        public float X;
        [DataMember]
        [FieldOffset(4)]
        public float Y;

        public static Vector2 Zero
        {
            get
            {
                return Vector2.zero;
            }
        }

        public static Vector2 One
        {
            get
            {
                return Vector2.one;
            }
        }

        public static Vector2 UnitX
        {
            get
            {
                return Vector2.unitX;
            }
        }

        public static Vector2 UnitY
        {
            get
            {
                return Vector2.unitY;
            }
        }

        public static Vector2 Center
        {
            get
            {
                return Vector2.center;
            }
        }

        public Vector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public Vector2(float value)
        {
            this.X = this.Y = value;
        }

        public override string ToString()
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            return string.Format((IFormatProvider)currentCulture, "{{X:{0} Y:{1}}}", (object)this.X.ToString((IFormatProvider)currentCulture), (object)this.Y.ToString((IFormatProvider)currentCulture));
        }

        public bool Equals(Vector2 other)
        {
            return this.Equals(ref other);
        }

        public bool Equals(ref Vector2 other)
        {
            if (this.X.Equal(other.X, 0.0001f))
                return this.Y.Equal(other.Y, 0.0001f);
            return false;
        }

        public override bool Equals(object obj)
        {
            bool flag = false;
            if (obj is Vector2)
                flag = this.Equals((Vector2)obj);
            return flag;
        }

        public override int GetHashCode()
        {
            return (17 * 29 + this.X.GetHashCode()) * 29 + this.Y.GetHashCode();
        }

        public float Length()
        {
            return (float)System.Math.Sqrt((double)this.X * (double)this.X + (double)this.Y * (double)this.Y);
        }

        public float LengthSquared()
        {
            return (float)((double)this.X * (double)this.X + (double)this.Y * (double)this.Y);
        }

        public Vector3 ToVector3(float z)
        {
            return new Vector3(this.X, this.Y, z);
        }

        public void ToVector3(float z, out Vector3 result)
        {
            result.X = this.X;
            result.Y = this.Y;
            result.Z = z;
        }

        public static float Distance(Vector2 value1, Vector2 value2)
        {
            float num1 = value1.X - value2.X;
            float num2 = value1.Y - value2.Y;
            return (float)System.Math.Sqrt((double)num1 * (double)num1 + (double)num2 * (double)num2);
        }

        public static void Distance(ref Vector2 value1, ref Vector2 value2, out float result)
        {
            float num1 = value1.X - value2.X;
            float num2 = value1.Y - value2.Y;
            result = (float)System.Math.Sqrt((double)num1 * (double)num1 + (double)num2 * (double)num2);
        }

        public static float DistanceSquared(Vector2 value1, Vector2 value2)
        {
            float num1 = value1.X - value2.X;
            float num2 = value1.Y - value2.Y;
            return (float)((double)num1 * (double)num1 + (double)num2 * (double)num2);
        }

        public static void DistanceSquared(ref Vector2 value1, ref Vector2 value2, out float result)
        {
            float num1 = value1.X - value2.X;
            float num2 = value1.Y - value2.Y;
            result = (float)((double)num1 * (double)num1 + (double)num2 * (double)num2);
        }

        public static float Dot(Vector2 value1, Vector2 value2)
        {
            return (float)((double)value1.X * (double)value2.X + (double)value1.Y * (double)value2.Y);
        }

        public static void Dot(ref Vector2 value1, ref Vector2 value2, out float result)
        {
            result = (float)((double)value1.X * (double)value2.X + (double)value1.Y * (double)value2.Y);
        }

        public static float Cross(Vector2 value1, Vector2 value2)
        {
            return (float)((double)value1.X * (double)value2.Y - (double)value2.X * (double)value1.Y);
        }

        public static void Cross(ref Vector2 value1, ref Vector2 value2, out float result)
        {
            result = (float)((double)value1.X * (double)value2.Y - (double)value2.X * (double)value1.Y);
        }

        public Vector2 Cross(float s)
        {
            return new Vector2(s * this.Y, -s * this.X);
        }

        public float Normalize()
        {
            float num = (float)System.Math.Sqrt((double)this.X * (double)this.X + (double)this.Y * (double)this.Y);
            if ((double)num > 0.0)
            {
                this.X /= num;
                this.Y /= num;
            }
            return num;
        }

        public static Vector2 Normalize(Vector2 value)
        {
            float num = (float)System.Math.Sqrt((double)value.X * (double)value.X + (double)value.Y * (double)value.Y);
            Vector2 vector2;
            if ((double)num > 0.0)
            {
                vector2.X = value.X / num;
                vector2.Y = value.Y / num;
            }
            else
            {
                vector2.X = value.X;
                vector2.Y = value.Y;
            }
            return vector2;
        }

        public static float Normalize(ref Vector2 value, out Vector2 result)
        {
            float num = (float)System.Math.Sqrt((double)value.X * (double)value.X + (double)value.Y * (double)value.Y);
            if ((double)num > 0.0)
            {
                result.X = value.X / num;
                result.Y = value.Y / num;
            }
            else
            {
                result.X = value.X;
                result.Y = value.Y;
            }
            return num;
        }

        public static Vector2 Reflect(Vector2 vector, Vector2 normal)
        {
            float num = (float)((double)vector.X * (double)normal.X + (double)vector.Y * (double)normal.Y);
            Vector2 vector2;
            vector2.X = vector.X - 2f * num * normal.X;
            vector2.Y = vector.Y - 2f * num * normal.Y;
            return vector2;
        }

        public static void Reflect(ref Vector2 vector, ref Vector2 normal, out Vector2 result)
        {
            float num = (float)((double)vector.X * (double)normal.X + (double)vector.Y * (double)normal.Y);
            result.X = vector.X - 2f * num * normal.X;
            result.Y = vector.Y - 2f * num * normal.Y;
        }

        public static Vector2 Rotate(Vector2 vector, float angle)
        {
            Vector2 vector2;
            if ((double)angle == 0.0)
                vector2 = vector;
            else if ((double)angle == 3.14159297943115)
            {
                vector2 = -vector;
            }
            else
            {
                float num1 = (float)System.Math.Cos((double)angle);
                float num2 = (float)System.Math.Sin((double)angle);
                vector2.X = (float)((double)vector.X * (double)num1 - (double)vector.Y * (double)num2);
                vector2.Y = (float)((double)vector.X * (double)num2 + (double)vector.Y * (double)num1);
            }
            return vector2;
        }

        public static void Rotate(ref Vector2 vector, float angle, out Vector2 result)
        {
            if ((double)angle == 0.0)
                result = vector;
            else if ((double)angle == 3.14159297943115)
            {
                result = -vector;
            }
            else
            {
                float num1 = (float)System.Math.Cos((double)angle);
                float num2 = (float)System.Math.Sin((double)angle);
                result = new Vector2((float)((double)vector.X * (double)num1 - (double)vector.Y * (double)num2), (float)((double)vector.X * (double)num2 + (double)vector.Y * (double)num1));
            }
        }

        public static Vector2 Min(Vector2 value1, Vector2 value2)
        {
            Vector2 vector2;
            vector2.X = (double)value1.X < (double)value2.X ? value1.X : value2.X;
            vector2.Y = (double)value1.Y < (double)value2.Y ? value1.Y : value2.Y;
            return vector2;
        }

        public static void Min(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = (double)value1.X < (double)value2.X ? value1.X : value2.X;
            result.Y = (double)value1.Y < (double)value2.Y ? value1.Y : value2.Y;
        }

        public static Vector2 Max(Vector2 value1, Vector2 value2)
        {
            Vector2 vector2;
            vector2.X = (double)value1.X > (double)value2.X ? value1.X : value2.X;
            vector2.Y = (double)value1.Y > (double)value2.Y ? value1.Y : value2.Y;
            return vector2;
        }

        public static void Max(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = (double)value1.X > (double)value2.X ? value1.X : value2.X;
            result.Y = (double)value1.Y > (double)value2.Y ? value1.Y : value2.Y;
        }

        public static Vector2 Abs(Vector2 value)
        {
            Vector2 vector2;
            vector2.X = System.Math.Abs(value.X);
            vector2.Y = System.Math.Abs(value.Y);
            return vector2;
        }

        public static void Abs(ref Vector2 value, out Vector2 result)
        {
            result.X = System.Math.Abs(value.X);
            result.Y = System.Math.Abs(value.Y);
        }

        public static Vector2 Clamp(Vector2 value1, Vector2 min, Vector2 max)
        {
            float x = value1.X;
            float num1 = (double)x > (double)max.X ? max.X : x;
            float num2 = (double)num1 < (double)min.X ? min.X : num1;
            float y = value1.Y;
            float num3 = (double)y > (double)max.Y ? max.Y : y;
            float num4 = (double)num3 < (double)min.Y ? min.Y : num3;
            Vector2 vector2;
            vector2.X = num2;
            vector2.Y = num4;
            return vector2;
        }

        public static void Clamp(
          ref Vector2 value1,
          ref Vector2 min,
          ref Vector2 max,
          out Vector2 result)
        {
            float x = value1.X;
            float num1 = (double)x > (double)max.X ? max.X : x;
            float num2 = (double)num1 < (double)min.X ? min.X : num1;
            float y = value1.Y;
            float num3 = (double)y > (double)max.Y ? max.Y : y;
            float num4 = (double)num3 < (double)min.Y ? min.Y : num3;
            result.X = num2;
            result.Y = num4;
        }

        public static Vector2 Lerp(Vector2 value1, Vector2 value2, float amount)
        {
            Vector2 vector2;
            vector2.X = value1.X + (value2.X - value1.X) * amount;
            vector2.Y = value1.Y + (value2.Y - value1.Y) * amount;
            return vector2;
        }

        public static void Lerp(
          ref Vector2 value1,
          ref Vector2 value2,
          float amount,
          out Vector2 result)
        {
            result.X = value1.X + (value2.X - value1.X) * amount;
            result.Y = value1.Y + (value2.Y - value1.Y) * amount;
        }

        public static Vector2 Barycentric(
          Vector2 value1,
          Vector2 value2,
          Vector2 value3,
          float amount1,
          float amount2)
        {
            Vector2 vector2;
            vector2.X = (float)((double)value1.X + (double)amount1 * ((double)value2.X - (double)value1.X) + (double)amount2 * ((double)value3.X - (double)value1.X));
            vector2.Y = (float)((double)value1.Y + (double)amount1 * ((double)value2.Y - (double)value1.Y) + (double)amount2 * ((double)value3.Y - (double)value1.Y));
            return vector2;
        }

        public static void Barycentric(
          ref Vector2 value1,
          ref Vector2 value2,
          ref Vector2 value3,
          float amount1,
          float amount2,
          out Vector2 result)
        {
            result.X = (float)((double)value1.X + (double)amount1 * ((double)value2.X - (double)value1.X) + (double)amount2 * ((double)value3.X - (double)value1.X));
            result.Y = (float)((double)value1.Y + (double)amount1 * ((double)value2.Y - (double)value1.Y) + (double)amount2 * ((double)value3.Y - (double)value1.Y));
        }

        public static Vector2 SmoothStep(Vector2 value1, Vector2 value2, float amount)
        {
            amount = (double)amount > 1.0 ? 1f : ((double)amount < 0.0 ? 0.0f : amount);
            amount = (float)((double)amount * (double)amount * (3.0 - 2.0 * (double)amount));
            Vector2 vector2;
            vector2.X = value1.X + (value2.X - value1.X) * amount;
            vector2.Y = value1.Y + (value2.Y - value1.Y) * amount;
            return vector2;
        }

        public static void SmoothStep(
          ref Vector2 value1,
          ref Vector2 value2,
          float amount,
          out Vector2 result)
        {
            amount = (double)amount > 1.0 ? 1f : ((double)amount < 0.0 ? 0.0f : amount);
            amount = (float)((double)amount * (double)amount * (3.0 - 2.0 * (double)amount));
            result.X = value1.X + (value2.X - value1.X) * amount;
            result.Y = value1.Y + (value2.Y - value1.Y) * amount;
        }

        public static Vector2 SmoothDamp(
          Vector2 current,
          Vector2 target,
          ref Vector2 currentVelocity,
          float smoothTime,
          float gameTime)
        {
            smoothTime = MathHelper.Max(0.0001f, smoothTime);
            float num1 = 2f / smoothTime;
            float num2 = num1 * gameTime;
            float num3 = (float)(1.0 / (1.0 + (double)num2 + 0.479999989271164 * (double)num2 * (double)num2 + 0.234999999403954 * (double)num2 * (double)num2 * (double)num2));
            Vector2 vector2_1 = current - target;
            Vector2 vector2_2 = target;
            target = current - vector2_1;
            Vector2 vector2_3 = (currentVelocity + num1 * vector2_1) * gameTime;
            currentVelocity = (currentVelocity - num1 * vector2_3) * num3;
            Vector2 vector2_4 = target + (vector2_1 + vector2_3) * num3;
            if ((double)Vector2.Dot(vector2_2 - current, vector2_4 - vector2_2) > 0.0)
            {
                vector2_4 = vector2_2;
                currentVelocity = (vector2_4 - vector2_2) / gameTime;
            }
            return vector2_4;
        }

        public static Vector2 SmoothDamp(
          Vector2 current,
          Vector2 target,
          ref Vector2 currentVelocity,
          float smoothTime,
          float maxSpeed,
          float gameTime)
        {
            smoothTime = MathHelper.Max(0.0001f, smoothTime);
            float num1 = 2f / smoothTime;
            float num2 = num1 * gameTime;
            float num3 = (float)(1.0 / (1.0 + (double)num2 + 0.479999989271164 * (double)num2 * (double)num2 + 0.234999999403954 * (double)num2 * (double)num2 * (double)num2));
            Vector2 vector = current - target;
            Vector2 vector2_1 = target;
            Vector2 vector2_2 = Vector2.ClampMagnitude(vector, maxSpeed * smoothTime);
            target = current - vector2_2;
            Vector2 vector2_3 = (currentVelocity + num1 * vector2_2) * gameTime;
            currentVelocity = (currentVelocity - num1 * vector2_3) * num3;
            Vector2 vector2_4 = target + (vector2_2 + vector2_3) * num3;
            if ((double)Vector2.Dot(vector2_1 - current, vector2_4 - vector2_1) > 0.0)
            {
                vector2_4 = vector2_1;
                currentVelocity = (vector2_4 - vector2_1) / gameTime;
            }
            return vector2_4;
        }

        public static Vector2 ClampMagnitude(Vector2 vector, float maxLength)
        {
            if ((double)vector.LengthSquared() <= (double)maxLength * (double)maxLength)
                return vector;
            double num = (double)vector.Normalize();
            return vector * maxLength;
        }

        public static Vector2 CatmullRom(
          Vector2 value1,
          Vector2 value2,
          Vector2 value3,
          Vector2 value4,
          float amount)
        {
            float num1 = amount * amount;
            float num2 = amount * num1;
            Vector2 vector2;
            vector2.X = (float)(0.5 * (2.0 * (double)value2.X + (-(double)value1.X + (double)value3.X) * (double)amount + (2.0 * (double)value1.X - 5.0 * (double)value2.X + 4.0 * (double)value3.X - (double)value4.X) * (double)num1 + (-(double)value1.X + 3.0 * (double)value2.X - 3.0 * (double)value3.X + (double)value4.X) * (double)num2));
            vector2.Y = (float)(0.5 * (2.0 * (double)value2.Y + (-(double)value1.Y + (double)value3.Y) * (double)amount + (2.0 * (double)value1.Y - 5.0 * (double)value2.Y + 4.0 * (double)value3.Y - (double)value4.Y) * (double)num1 + (-(double)value1.Y + 3.0 * (double)value2.Y - 3.0 * (double)value3.Y + (double)value4.Y) * (double)num2));
            return vector2;
        }

        public static void CatmullRom(
          ref Vector2 value1,
          ref Vector2 value2,
          ref Vector2 value3,
          ref Vector2 value4,
          float amount,
          out Vector2 result)
        {
            float num1 = amount * amount;
            float num2 = amount * num1;
            result.X = (float)(0.5 * (2.0 * (double)value2.X + (-(double)value1.X + (double)value3.X) * (double)amount + (2.0 * (double)value1.X - 5.0 * (double)value2.X + 4.0 * (double)value3.X - (double)value4.X) * (double)num1 + (-(double)value1.X + 3.0 * (double)value2.X - 3.0 * (double)value3.X + (double)value4.X) * (double)num2));
            result.Y = (float)(0.5 * (2.0 * (double)value2.Y + (-(double)value1.Y + (double)value3.Y) * (double)amount + (2.0 * (double)value1.Y - 5.0 * (double)value2.Y + 4.0 * (double)value3.Y - (double)value4.Y) * (double)num1 + (-(double)value1.Y + 3.0 * (double)value2.Y - 3.0 * (double)value3.Y + (double)value4.Y) * (double)num2));
        }

        public static Vector2 Hermite(
          Vector2 value1,
          Vector2 tangent1,
          Vector2 value2,
          Vector2 tangent2,
          float amount)
        {
            float num1 = amount * amount;
            float num2 = amount * num1;
            float num3 = (float)(2.0 * (double)num2 - 3.0 * (double)num1 + 1.0);
            float num4 = (float)(-2.0 * (double)num2 + 3.0 * (double)num1);
            float num5 = num2 - 2f * num1 + amount;
            float num6 = num2 - num1;
            Vector2 vector2;
            vector2.X = (float)((double)value1.X * (double)num3 + (double)value2.X * (double)num4 + (double)tangent1.X * (double)num5 + (double)tangent2.X * (double)num6);
            vector2.Y = (float)((double)value1.Y * (double)num3 + (double)value2.Y * (double)num4 + (double)tangent1.Y * (double)num5 + (double)tangent2.Y * (double)num6);
            return vector2;
        }

        public static void Hermite(
          ref Vector2 value1,
          ref Vector2 tangent1,
          ref Vector2 value2,
          ref Vector2 tangent2,
          float amount,
          out Vector2 result)
        {
            float num1 = amount * amount;
            float num2 = amount * num1;
            float num3 = (float)(2.0 * (double)num2 - 3.0 * (double)num1 + 1.0);
            float num4 = (float)(-2.0 * (double)num2 + 3.0 * (double)num1);
            float num5 = num2 - 2f * num1 + amount;
            float num6 = num2 - num1;
            result.X = (float)((double)value1.X * (double)num3 + (double)value2.X * (double)num4 + (double)tangent1.X * (double)num5 + (double)tangent2.X * (double)num6);
            result.Y = (float)((double)value1.Y * (double)num3 + (double)value2.Y * (double)num4 + (double)tangent1.Y * (double)num5 + (double)tangent2.Y * (double)num6);
        }

        public static Vector2 Transform(Vector2 position, Matrix matrix)
        {
            float num1 = (float)((double)position.X * (double)matrix.M11 + (double)position.Y * (double)matrix.M21) + matrix.M41;
            float num2 = (float)((double)position.X * (double)matrix.M12 + (double)position.Y * (double)matrix.M22) + matrix.M42;
            Vector2 vector2;
            vector2.X = num1;
            vector2.Y = num2;
            return vector2;
        }

        public static void Transform(ref Vector2 position, ref Matrix matrix, out Vector2 result)
        {
            float num1 = (float)((double)position.X * (double)matrix.M11 + (double)position.Y * (double)matrix.M21) + matrix.M41;
            float num2 = (float)((double)position.X * (double)matrix.M12 + (double)position.Y * (double)matrix.M22) + matrix.M42;
            result.X = num1;
            result.Y = num2;
        }

        public static void Transform(
          Vector2[] sourceArray,
          ref Matrix matrix,
          Vector2[] destinationArray)
        {
            if (sourceArray == null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (destinationArray == null)
                throw new ArgumentNullException(nameof(destinationArray));
            if (destinationArray.Length < sourceArray.Length)
                throw new ArgumentException("NotEnoughTargetSize");
            for (int index = 0; index < sourceArray.Length; ++index)
            {
                float x = sourceArray[index].X;
                float y = sourceArray[index].Y;
                destinationArray[index].X = (float)((double)x * (double)matrix.M11 + (double)y * (double)matrix.M21) + matrix.M41;
                destinationArray[index].Y = (float)((double)x * (double)matrix.M12 + (double)y * (double)matrix.M22) + matrix.M42;
            }
        }

        public static void Transform(
          Vector2[] sourceArray,
          int sourceIndex,
          ref Matrix matrix,
          Vector2[] destinationArray,
          int destinationIndex,
          int length)
        {
            if (sourceArray == null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (destinationArray == null)
                throw new ArgumentNullException(nameof(destinationArray));
            if (sourceArray.Length < sourceIndex + length)
                throw new ArgumentException("NotEnoughSourceSize");
            if (destinationArray.Length < destinationIndex + length)
                throw new ArgumentException("NotEnoughTargetSize");
            for (; length > 0; --length)
            {
                float x = sourceArray[sourceIndex].X;
                float y = sourceArray[sourceIndex].Y;
                destinationArray[destinationIndex].X = (float)((double)x * (double)matrix.M11 + (double)y * (double)matrix.M21) + matrix.M41;
                destinationArray[destinationIndex].Y = (float)((double)x * (double)matrix.M12 + (double)y * (double)matrix.M22) + matrix.M42;
                ++sourceIndex;
                ++destinationIndex;
            }
        }

        public static Vector2 TransformNormal(Vector2 normal, Matrix matrix)
        {
            float num1 = (float)((double)normal.X * (double)matrix.M11 + (double)normal.Y * (double)matrix.M21);
            float num2 = (float)((double)normal.X * (double)matrix.M12 + (double)normal.Y * (double)matrix.M22);
            Vector2 vector2;
            vector2.X = num1;
            vector2.Y = num2;
            return vector2;
        }

        public static void TransformNormal(ref Vector2 normal, ref Matrix matrix, out Vector2 result)
        {
            float num1 = (float)((double)normal.X * (double)matrix.M11 + (double)normal.Y * (double)matrix.M21);
            float num2 = (float)((double)normal.X * (double)matrix.M12 + (double)normal.Y * (double)matrix.M22);
            result.X = num1;
            result.Y = num2;
        }

        public static Vector2 Transform(Vector2 value, Quaternion rotation)
        {
            float num1 = rotation.X + rotation.X;
            float num2 = rotation.Y + rotation.Y;
            float num3 = rotation.Z + rotation.Z;
            float num4 = rotation.W * num3;
            float num5 = rotation.X * num1;
            float num6 = rotation.X * num2;
            float num7 = rotation.Y * num2;
            float num8 = rotation.Z * num3;
            float num9 = (float)((double)value.X * (1.0 - (double)num7 - (double)num8) + (double)value.Y * ((double)num6 - (double)num4));
            float num10 = (float)((double)value.X * ((double)num6 + (double)num4) + (double)value.Y * (1.0 - (double)num5 - (double)num8));
            Vector2 vector2;
            vector2.X = num9;
            vector2.Y = num10;
            return vector2;
        }

        public static void Transform(ref Vector2 value, ref Quaternion rotation, out Vector2 result)
        {
            float num1 = rotation.X + rotation.X;
            float num2 = rotation.Y + rotation.Y;
            float num3 = rotation.Z + rotation.Z;
            float num4 = rotation.W * num3;
            float num5 = rotation.X * num1;
            float num6 = rotation.X * num2;
            float num7 = rotation.Y * num2;
            float num8 = rotation.Z * num3;
            float num9 = (float)((double)value.X * (1.0 - (double)num7 - (double)num8) + (double)value.Y * ((double)num6 - (double)num4));
            float num10 = (float)((double)value.X * ((double)num6 + (double)num4) + (double)value.Y * (1.0 - (double)num5 - (double)num8));
            result.X = num9;
            result.Y = num10;
        }

        public static Vector2 Negate(Vector2 value)
        {
            Vector2 vector2;
            vector2.X = -value.X;
            vector2.Y = -value.Y;
            return vector2;
        }

        public static void Negate(ref Vector2 value, out Vector2 result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
        }

        public static Vector2 Add(Vector2 value1, Vector2 value2)
        {
            Vector2 vector2;
            vector2.X = value1.X + value2.X;
            vector2.Y = value1.Y + value2.Y;
            return vector2;
        }

        public static void Add(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
        }

        public static Vector2 Subtract(Vector2 value1, Vector2 value2)
        {
            Vector2 vector2;
            vector2.X = value1.X - value2.X;
            vector2.Y = value1.Y - value2.Y;
            return vector2;
        }

        public static void Subtract(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
        }

        public static Vector2 Multiply(Vector2 value1, Vector2 value2)
        {
            Vector2 vector2;
            vector2.X = value1.X * value2.X;
            vector2.Y = value1.Y * value2.Y;
            return vector2;
        }

        public static void Multiply(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X * value2.X;
            result.Y = value1.Y * value2.Y;
        }

        public static Vector2 Multiply(Vector2 value1, float scaleFactor)
        {
            Vector2 vector2;
            vector2.X = value1.X * scaleFactor;
            vector2.Y = value1.Y * scaleFactor;
            return vector2;
        }

        public static void Multiply(ref Vector2 value1, float scaleFactor, out Vector2 result)
        {
            result.X = value1.X * scaleFactor;
            result.Y = value1.Y * scaleFactor;
        }

        public static Vector2 Divide(Vector2 value1, Vector2 value2)
        {
            Vector2 vector2;
            vector2.X = value1.X / value2.X;
            vector2.Y = value1.Y / value2.Y;
            return vector2;
        }

        public static void Divide(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X / value2.X;
            result.Y = value1.Y / value2.Y;
        }

        public static Vector2 Divide(Vector2 value1, float divider)
        {
            float num = 1f / divider;
            Vector2 vector2;
            vector2.X = value1.X * num;
            vector2.Y = value1.Y * num;
            return vector2;
        }

        public static void Divide(ref Vector2 value1, float divider, out Vector2 result)
        {
            float num = 1f / divider;
            result.X = value1.X * num;
            result.Y = value1.Y * num;
        }

        public static float Angle(Vector2 from, Vector2 to)
        {
            return Vector2.Angle(ref from, ref to);
        }

        public static bool Collinear(ref Vector2 a, ref Vector2 b, ref Vector2 c)
        {
            return Vector2.Collinear(ref a, ref b, ref c, 0.0f);
        }

        public static bool Collinear(ref Vector2 a, ref Vector2 b, ref Vector2 c, float tolerance)
        {
            return MathHelper.FloatInRange(MathHelper.Area(ref a, ref b, ref c), -tolerance, tolerance);
        }

        public static float Angle(ref Vector2 from, ref Vector2 to)
        {
            double num1 = (double)from.Normalize();
            double num2 = (double)to.Normalize();
            float num3 = Vector2.Dot(from, to) / (from.Length() * to.Length());
            if ((double)num3 > 1.0)
                num3 = 1f;
            else if ((double)num3 < -1.0)
                num3 = -1f;
            float num4 = (float)System.Math.Acos((double)num3);
            if ((double)from.X * (double)to.Y - (double)to.X * (double)from.Y > 0.0)
                num4 = -num4;
            return num4;
        }

        public static Vector2 operator -(Vector2 value)
        {
            Vector2 vector2;
            vector2.X = -value.X;
            vector2.Y = -value.Y;
            return vector2;
        }

        public static bool operator ==(Vector2 value1, Vector2 value2)
        {
            return value1.Equals(ref value2);
        }

        public static bool operator !=(Vector2 value1, Vector2 value2)
        {
            return !value1.Equals(ref value2);
        }

        public static Vector2 operator +(Vector2 value1, Vector2 value2)
        {
            Vector2 vector2;
            vector2.X = value1.X + value2.X;
            vector2.Y = value1.Y + value2.Y;
            return vector2;
        }

        public static Vector2 operator -(Vector2 value1, Vector2 value2)
        {
            Vector2 vector2;
            vector2.X = value1.X - value2.X;
            vector2.Y = value1.Y - value2.Y;
            return vector2;
        }

        public static Vector2 operator *(Vector2 value1, Vector2 value2)
        {
            Vector2 vector2;
            vector2.X = value1.X * value2.X;
            vector2.Y = value1.Y * value2.Y;
            return vector2;
        }

        public static Vector2 operator *(Vector2 value, float scaleFactor)
        {
            Vector2 vector2;
            vector2.X = value.X * scaleFactor;
            vector2.Y = value.Y * scaleFactor;
            return vector2;
        }

        public static Vector2 operator *(float scaleFactor, Vector2 value)
        {
            Vector2 vector2;
            vector2.X = value.X * scaleFactor;
            vector2.Y = value.Y * scaleFactor;
            return vector2;
        }

        public static Vector2 operator /(Vector2 value1, Vector2 value2)
        {
            Vector2 vector2;
            vector2.X = value1.X / value2.X;
            vector2.Y = value1.Y / value2.Y;
            return vector2;
        }

        public static Vector2 operator /(Vector2 value1, float divider)
        {
            float num = 1f / divider;
            Vector2 vector2;
            vector2.X = value1.X * num;
            vector2.Y = value1.Y * num;
            return vector2;
        }
    }
}
