using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace WebGl
{
    [DataContract(Namespace = "WaveEngine.Common.Math")]
    public struct Quaternion : IEquatable<Quaternion>
    {
        private static readonly Quaternion identity = new Quaternion(0.0f, 0.0f, 0.0f, 1f);
        [DataMember]
        public float X;
        [DataMember]
        public float Y;
        [DataMember]
        public float Z;
        [DataMember]
        public float W;

        public static Quaternion Identity
        {
            get
            {
                return Quaternion.identity;
            }
        }

        public Quaternion(float x, float y, float z, float w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        public Quaternion(Vector3 vectorPart, float scalarPart)
        {
            this.X = vectorPart.X;
            this.Y = vectorPart.Y;
            this.Z = vectorPart.Z;
            this.W = scalarPart;
        }

        public override string ToString()
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            return string.Format((IFormatProvider)currentCulture, "{{X:{0} Y:{1} Z:{2} W:{3}}}", (object)this.X.ToString((IFormatProvider)currentCulture), (object)this.Y.ToString((IFormatProvider)currentCulture), (object)this.Z.ToString((IFormatProvider)currentCulture), (object)this.W.ToString((IFormatProvider)currentCulture));
        }

        public bool Equals(Quaternion other)
        {
            return this.Equals(ref other);
        }

        public bool Equals(ref Quaternion other)
        {
            if (this.X.Equal(other.X, 0.0001f) && this.Y.Equal(other.Y, 0.0001f) && this.Z.Equal(other.Z, 0.0001f))
                return this.W.Equal(other.W, 0.0001f);
            return false;
        }

        public override bool Equals(object obj)
        {
            bool flag = false;
            if (obj is Quaternion)
                flag = this.Equals((Quaternion)obj);
            return flag;
        }

        public override int GetHashCode()
        {
            return (((17 * 29 + this.X.GetHashCode()) * 29 + this.Y.GetHashCode()) * 29 + this.Z.GetHashCode()) * 29 + this.W.GetHashCode();
        }

        public float LengthSquared()
        {
            return (float)((double)this.X * (double)this.X + (double)this.Y * (double)this.Y + (double)this.Z * (double)this.Z + (double)this.W * (double)this.W);
        }

        public float Length()
        {
            return (float)System.Math.Sqrt((double)this.X * (double)this.X + (double)this.Y * (double)this.Y + (double)this.Z * (double)this.Z + (double)this.W * (double)this.W);
        }

        public void Normalize()
        {
            float num = 1f / (float)System.Math.Sqrt((double)this.X * (double)this.X + (double)this.Y * (double)this.Y + (double)this.Z * (double)this.Z + (double)this.W * (double)this.W);
            this.X *= num;
            this.Y *= num;
            this.Z *= num;
            this.W *= num;
        }

        public static Quaternion Normalize(Quaternion quaternion)
        {
            Quaternion result;
            Quaternion.Normalize(ref quaternion, out result);
            return result;
        }

        public static void Normalize(ref Quaternion quaternion, out Quaternion result)
        {
            float num = 1f / (float)System.Math.Sqrt((double)quaternion.X * (double)quaternion.X + (double)quaternion.Y * (double)quaternion.Y + (double)quaternion.Z * (double)quaternion.Z + (double)quaternion.W * (double)quaternion.W);
            result.X = quaternion.X * num;
            result.Y = quaternion.Y * num;
            result.Z = quaternion.Z * num;
            result.W = quaternion.W * num;
        }

        public void Conjugate()
        {
            this.X = -this.X;
            this.Y = -this.Y;
            this.Z = -this.Z;
        }

        public static Quaternion Conjugate(Quaternion value)
        {
            Quaternion result;
            Quaternion.Conjugate(ref value, out result);
            return result;
        }

        public static void Conjugate(ref Quaternion value, out Quaternion result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
            result.W = value.W;
        }

        public static Quaternion Inverse(Quaternion quaternion)
        {
            Quaternion result;
            Quaternion.Inverse(ref quaternion, out result);
            return result;
        }

        public static void Inverse(ref Quaternion quaternion, out Quaternion result)
        {
            float num = 1f / (float)((double)quaternion.X * (double)quaternion.X + (double)quaternion.Y * (double)quaternion.Y + (double)quaternion.Z * (double)quaternion.Z + (double)quaternion.W * (double)quaternion.W);
            result.X = -quaternion.X * num;
            result.Y = -quaternion.Y * num;
            result.Z = -quaternion.Z * num;
            result.W = quaternion.W * num;
        }

        public static Quaternion CreateFromAxisAngle(Vector3 axis, float angle)
        {
            Quaternion result;
            Quaternion.CreateFromAxisAngle(ref axis, angle, out result);
            return result;
        }

        public static void CreateFromAxisAngle(ref Vector3 axis, float angle, out Quaternion result)
        {
            float num1 = angle * 0.5f;
            float num2 = (float)System.Math.Sin((double)num1);
            float num3 = (float)System.Math.Cos((double)num1);
            result.X = axis.X * num2;
            result.Y = axis.Y * num2;
            result.Z = axis.Z * num2;
            result.W = num3;
        }

        public static Quaternion CreateFromYawPitchRoll(float yaw, float pitch, float roll)
        {
            Quaternion result;
            Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll, out result);
            return result;
        }

        public static void CreateFromYawPitchRoll(
          float yaw,
          float pitch,
          float roll,
          out Quaternion result)
        {
            float num1 = roll * 0.5f;
            float num2 = (float)System.Math.Sin((double)num1);
            float num3 = (float)System.Math.Cos((double)num1);
            float num4 = pitch * 0.5f;
            float num5 = (float)System.Math.Sin((double)num4);
            float num6 = (float)System.Math.Cos((double)num4);
            float num7 = yaw * 0.5f;
            float num8 = (float)System.Math.Sin((double)num7);
            float num9 = (float)System.Math.Cos((double)num7);
            result.X = (float)((double)num9 * (double)num5 * (double)num3 + (double)num8 * (double)num6 * (double)num2);
            result.Y = (float)((double)num8 * (double)num6 * (double)num3 - (double)num9 * (double)num5 * (double)num2);
            result.Z = (float)((double)num9 * (double)num6 * (double)num2 - (double)num8 * (double)num5 * (double)num3);
            result.W = (float)((double)num9 * (double)num6 * (double)num3 + (double)num8 * (double)num5 * (double)num2);
        }

        public static void CreateFromEuler(ref Vector3 euler, out Quaternion result)
        {
            Quaternion.CreateFromYawPitchRoll(euler.Y, euler.X, euler.Z, out result);
        }

        public static Quaternion CreateFromEuler(Vector3 euler)
        {
            return Quaternion.CreateFromYawPitchRoll(euler.Y, euler.X, euler.Z);
        }

        public static Quaternion CreateFromTwoVectors(Vector3 u, Vector3 v)
        {
            double num1 = (double)u.Normalize();
            double num2 = (double)v.Normalize();
            float result1;
            Vector3.Dot(ref u, ref v, out result1);
            Vector3 result2;
            Vector3.Cross(ref u, ref v, out result2);
            double num3 = (double)result2.Normalize();
            float angle = (float)System.Math.Acos((double)result1);
            Quaternion result3;
            Quaternion.CreateFromAxisAngle(ref result2, angle, out result3);
            return result3;
        }

        public static void CreateFromTwoVectors(ref Vector3 lookAt, out Quaternion result)
        {
            Vector3 up = Vector3.Up;
            Quaternion.CreateFromLookAt(ref lookAt, ref up, out result);
        }

        public static void CreateFromLookAt(ref Vector3 lookAt, ref Vector3 up, out Quaternion result)
        {
            double num1 = (double)lookAt.Normalize();
            Vector3 result1 = up;
            Vector3 result2;
            Vector3.Cross(ref result1, ref lookAt, out result2);
            if ((double)result2.LengthSquared() == 0.0)
            {
                result1.X = up.X;
                result1.Y = up.Z;
                result1.Z = -up.Y;
                Vector3.Cross(ref result1, ref lookAt, out result2);
            }
            Vector3.Cross(ref lookAt, ref result2, out result1);
            double num2 = (double)result1.Normalize();
            double num3 = (double)result2.Normalize();
            Matrix matrix = new Matrix(result2.X, result2.Y, result2.Z, 0.0f, result1.X, result1.Y, result1.Z, 0.0f, lookAt.X, lookAt.Y, lookAt.Z, 0.0f, 0.0f, 0.0f, 0.0f, 1f);
            Quaternion.CreateFromRotationMatrix(ref matrix, out result);
        }

        public static Quaternion CreateFromRotationMatrix(Matrix matrix)
        {
            Quaternion quaternion = new Quaternion();
            Quaternion.CreateFromRotationMatrix(ref matrix, out quaternion);
            return quaternion;
        }

        public static void CreateFromRotationMatrix(ref Matrix matrix, out Quaternion quaternion)
        {
            float num1 = (float)System.Math.Sqrt((double)matrix.M11 * (double)matrix.M11 + (double)matrix.M12 * (double)matrix.M12 + (double)matrix.M13 * (double)matrix.M13);
            float num2 = (float)System.Math.Sqrt((double)matrix.M21 * (double)matrix.M21 + (double)matrix.M22 * (double)matrix.M22 + (double)matrix.M23 * (double)matrix.M23);
            float num3 = (float)System.Math.Sqrt((double)matrix.M31 * (double)matrix.M31 + (double)matrix.M32 * (double)matrix.M32 + (double)matrix.M33 * (double)matrix.M33);
            float m11 = matrix.M11;
            float m12 = matrix.M12;
            float m13 = matrix.M13;
            float m21 = matrix.M21;
            float m22 = matrix.M22;
            float m23 = matrix.M23;
            float m31 = matrix.M31;
            float m32 = matrix.M32;
            float m33 = matrix.M33;
            if ((double)System.Math.Abs(num1) > 0.0001 && (double)System.Math.Abs(num2) > 0.0001 && (double)System.Math.Abs(num3) > 0.0001)
            {
                m11 /= num1;
                m12 /= num1;
                m13 /= num1;
                m21 /= num2;
                m22 /= num2;
                m23 /= num2;
                m31 /= num3;
                m32 /= num3;
                m33 /= num3;
            }
            float num4 = m11 + m22 + m33;
            if ((double)num4 > 0.0)
            {
                float num5 = (float)System.Math.Sqrt((double)num4 + 1.0);
                float num6 = 0.5f / num5;
                quaternion.X = (m23 - m32) * num6;
                quaternion.Y = (m31 - m13) * num6;
                quaternion.Z = (m12 - m21) * num6;
                quaternion.W = num5 * 0.5f;
            }
            else if ((double)m11 >= (double)m22 && (double)m11 >= (double)m33)
            {
                float num5 = (float)System.Math.Sqrt(1.0 + (double)m11 - (double)m22 - (double)m33);
                float num6 = 0.5f / num5;
                quaternion.X = num5 * 0.5f;
                quaternion.Y = (m12 + m21) * num6;
                quaternion.Z = (m13 + m31) * num6;
                quaternion.W = (m23 - m32) * num6;
            }
            else if ((double)m22 > (double)m33)
            {
                float num5 = (float)System.Math.Sqrt(1.0 + (double)m22 - (double)m11 - (double)m33);
                float num6 = 0.5f / num5;
                quaternion.X = (m21 + m12) * num6;
                quaternion.Y = num5 * 0.5f;
                quaternion.Z = (m32 + m23) * num6;
                quaternion.W = (m31 - m13) * num6;
            }
            else
            {
                float num5 = (float)System.Math.Sqrt(1.0 + (double)m33 - (double)m11 - (double)m22);
                float num6 = 0.5f / num5;
                quaternion.X = (m31 + m13) * num6;
                quaternion.Y = (m32 + m23) * num6;
                quaternion.Z = num5 * 0.5f;
                quaternion.W = (m12 - m21) * num6;
            }
        }

        public static float Dot(Quaternion quaternion1, Quaternion quaternion2)
        {
            return (float)((double)quaternion1.X * (double)quaternion2.X + (double)quaternion1.Y * (double)quaternion2.Y + (double)quaternion1.Z * (double)quaternion2.Z + (double)quaternion1.W * (double)quaternion2.W);
        }

        public static Quaternion Slerp(
          Quaternion quaternion1,
          Quaternion quaternion2,
          float amount)
        {
            Quaternion result;
            Quaternion.Slerp(ref quaternion1, ref quaternion2, amount, out result);
            return result;
        }

        public static void Slerp(
          ref Quaternion quaternion1,
          ref Quaternion quaternion2,
          float amount,
          out Quaternion result)
        {
            float num1 = amount;
            float num2 = (float)((double)quaternion1.X * (double)quaternion2.X + (double)quaternion1.Y * (double)quaternion2.Y + (double)quaternion1.Z * (double)quaternion2.Z + (double)quaternion1.W * (double)quaternion2.W);
            bool flag = false;
            if ((double)num2 < 0.0)
            {
                flag = true;
                num2 = -num2;
            }
            float num3;
            float num4;
            if ((double)num2 > 0.999998986721039)
            {
                num3 = 1f - num1;
                num4 = flag ? -num1 : num1;
            }
            else
            {
                float num5 = (float)System.Math.Acos((double)num2);
                float num6 = (float)(1.0 / System.Math.Sin((double)num5));
                num3 = (float)System.Math.Sin((1.0 - (double)num1) * (double)num5) * num6;
                num4 = flag ? (float)-System.Math.Sin((double)num1 * (double)num5) * num6 : (float)System.Math.Sin((double)num1 * (double)num5) * num6;
            }
            result.X = (float)((double)num3 * (double)quaternion1.X + (double)num4 * (double)quaternion2.X);
            result.Y = (float)((double)num3 * (double)quaternion1.Y + (double)num4 * (double)quaternion2.Y);
            result.Z = (float)((double)num3 * (double)quaternion1.Z + (double)num4 * (double)quaternion2.Z);
            result.W = (float)((double)num3 * (double)quaternion1.W + (double)num4 * (double)quaternion2.W);
        }

        public static Quaternion Lerp(
          Quaternion quaternion1,
          Quaternion quaternion2,
          float amount)
        {
            Quaternion result;
            Quaternion.Lerp(ref quaternion1, ref quaternion2, amount, out result);
            return result;
        }

        public static void Lerp(
          ref Quaternion quaternion1,
          ref Quaternion quaternion2,
          float amount,
          out Quaternion result)
        {
            float num1 = amount;
            float num2 = 1f - num1;
            if ((double)quaternion1.X * (double)quaternion2.X + (double)quaternion1.Y * (double)quaternion2.Y + (double)quaternion1.Z * (double)quaternion2.Z + (double)quaternion1.W * (double)quaternion2.W >= 0.0)
            {
                result.X = (float)((double)num2 * (double)quaternion1.X + (double)num1 * (double)quaternion2.X);
                result.Y = (float)((double)num2 * (double)quaternion1.Y + (double)num1 * (double)quaternion2.Y);
                result.Z = (float)((double)num2 * (double)quaternion1.Z + (double)num1 * (double)quaternion2.Z);
                result.W = (float)((double)num2 * (double)quaternion1.W + (double)num1 * (double)quaternion2.W);
            }
            else
            {
                result.X = (float)((double)num2 * (double)quaternion1.X - (double)num1 * (double)quaternion2.X);
                result.Y = (float)((double)num2 * (double)quaternion1.Y - (double)num1 * (double)quaternion2.Y);
                result.Z = (float)((double)num2 * (double)quaternion1.Z - (double)num1 * (double)quaternion2.Z);
                result.W = (float)((double)num2 * (double)quaternion1.W - (double)num1 * (double)quaternion2.W);
            }
            float num3 = 1f / (float)System.Math.Sqrt((double)result.X * (double)result.X + (double)result.Y * (double)result.Y + (double)result.Z * (double)result.Z + (double)result.W * (double)result.W);
            result.X *= num3;
            result.Y *= num3;
            result.Z *= num3;
            result.W *= num3;
        }

        public static Quaternion SmoothDamp(
          Quaternion current,
          Quaternion target,
          ref Quaternion deriv,
          float smoothTime,
          float gameTime)
        {
            float num1 = (double)Quaternion.Dot(current, target) > 0.0 ? 1f : -1f;
            target.X *= num1;
            target.Y *= num1;
            target.Z *= num1;
            target.W *= num1;
            Vector4 vector4 = new Vector4(MathHelper.SmoothDamp(current.X, target.X, ref deriv.X, smoothTime, gameTime), MathHelper.SmoothDamp(current.Y, target.Y, ref deriv.Y, smoothTime, gameTime), MathHelper.SmoothDamp(current.Z, target.Z, ref deriv.Z, smoothTime, gameTime), MathHelper.SmoothDamp(current.W, target.W, ref deriv.W, smoothTime, gameTime));
            double num2 = (double)vector4.Normalize();
            float num3 = 1f / smoothTime;
            deriv.X = (vector4.X - current.X) * num3;
            deriv.Y = (vector4.Y - current.Y) * num3;
            deriv.Z = (vector4.Z - current.Z) * num3;
            deriv.W = (vector4.W - current.W) * num3;
            return new Quaternion(vector4.X, vector4.Y, vector4.Z, vector4.W);
        }

        public static Quaternion Concatenate(Quaternion value1, Quaternion value2)
        {
            Quaternion result;
            Quaternion.Concatenate(ref value1, ref value2, out result);
            return result;
        }

        public static void Concatenate(
          ref Quaternion value1,
          ref Quaternion value2,
          out Quaternion result)
        {
            float x1 = value2.X;
            float y1 = value2.Y;
            float z1 = value2.Z;
            float w1 = value2.W;
            float x2 = value1.X;
            float y2 = value1.Y;
            float z2 = value1.Z;
            float w2 = value1.W;
            float num1 = (float)((double)y1 * (double)z2 - (double)z1 * (double)y2);
            float num2 = (float)((double)z1 * (double)x2 - (double)x1 * (double)z2);
            float num3 = (float)((double)x1 * (double)y2 - (double)y1 * (double)x2);
            float num4 = (float)((double)x1 * (double)x2 + (double)y1 * (double)y2 + (double)z1 * (double)z2);
            result.X = (float)((double)x1 * (double)w2 + (double)x2 * (double)w1) + num1;
            result.Y = (float)((double)y1 * (double)w2 + (double)y2 * (double)w1) + num2;
            result.Z = (float)((double)z1 * (double)w2 + (double)z2 * (double)w1) + num3;
            result.W = w1 * w2 - num4;
        }

        public static Quaternion Negate(Quaternion quaternion)
        {
            Quaternion result;
            Quaternion.Negate(ref quaternion, out result);
            return result;
        }

        public static void Negate(ref Quaternion quaternion, out Quaternion result)
        {
            result.X = -quaternion.X;
            result.Y = -quaternion.Y;
            result.Z = -quaternion.Z;
            result.W = -quaternion.W;
        }

        public static Quaternion Add(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion result;
            Quaternion.Add(ref quaternion1, ref quaternion2, out result);
            return result;
        }

        public static void Add(
          ref Quaternion quaternion1,
          ref Quaternion quaternion2,
          out Quaternion result)
        {
            result.X = quaternion1.X + quaternion2.X;
            result.Y = quaternion1.Y + quaternion2.Y;
            result.Z = quaternion1.Z + quaternion2.Z;
            result.W = quaternion1.W + quaternion2.W;
        }

        public static Quaternion Subtract(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion result;
            Quaternion.Subtract(ref quaternion1, ref quaternion2, out result);
            return result;
        }

        public static void Subtract(
          ref Quaternion quaternion1,
          ref Quaternion quaternion2,
          out Quaternion result)
        {
            result.X = quaternion1.X - quaternion2.X;
            result.Y = quaternion1.Y - quaternion2.Y;
            result.Z = quaternion1.Z - quaternion2.Z;
            result.W = quaternion1.W - quaternion2.W;
        }

        public static Quaternion Multiply(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion result;
            Quaternion.Multiply(ref quaternion1, ref quaternion2, out result);
            return result;
        }

        public static void Multiply(
          ref Quaternion quaternion1,
          ref Quaternion quaternion2,
          out Quaternion result)
        {
            float x1 = quaternion1.X;
            float y1 = quaternion1.Y;
            float z1 = quaternion1.Z;
            float w1 = quaternion1.W;
            float x2 = quaternion2.X;
            float y2 = quaternion2.Y;
            float z2 = quaternion2.Z;
            float w2 = quaternion2.W;
            float num1 = (float)((double)y1 * (double)z2 - (double)z1 * (double)y2);
            float num2 = (float)((double)z1 * (double)x2 - (double)x1 * (double)z2);
            float num3 = (float)((double)x1 * (double)y2 - (double)y1 * (double)x2);
            float num4 = (float)((double)x1 * (double)x2 + (double)y1 * (double)y2 + (double)z1 * (double)z2);
            result.X = (float)((double)x1 * (double)w2 + (double)x2 * (double)w1) + num1;
            result.Y = (float)((double)y1 * (double)w2 + (double)y2 * (double)w1) + num2;
            result.Z = (float)((double)z1 * (double)w2 + (double)z2 * (double)w1) + num3;
            result.W = w1 * w2 - num4;
        }

        public static Quaternion Multiply(Quaternion quaternion1, float scaleFactor)
        {
            Quaternion result;
            Quaternion.Multiply(ref quaternion1, scaleFactor, out result);
            return result;
        }

        public static void Multiply(
          ref Quaternion quaternion1,
          float scaleFactor,
          out Quaternion result)
        {
            result.X = quaternion1.X * scaleFactor;
            result.Y = quaternion1.Y * scaleFactor;
            result.Z = quaternion1.Z * scaleFactor;
            result.W = quaternion1.W * scaleFactor;
        }

        public static Quaternion Divide(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion result;
            Quaternion.Divide(ref quaternion1, ref quaternion2, out result);
            return result;
        }

        public static void Divide(
          ref Quaternion quaternion1,
          ref Quaternion quaternion2,
          out Quaternion result)
        {
            float x = quaternion1.X;
            float y = quaternion1.Y;
            float z = quaternion1.Z;
            float w = quaternion1.W;
            float num1 = 1f / (float)((double)quaternion2.X * (double)quaternion2.X + (double)quaternion2.Y * (double)quaternion2.Y + (double)quaternion2.Z * (double)quaternion2.Z + (double)quaternion2.W * (double)quaternion2.W);
            float num2 = -quaternion2.X * num1;
            float num3 = -quaternion2.Y * num1;
            float num4 = -quaternion2.Z * num1;
            float num5 = quaternion2.W * num1;
            float num6 = (float)((double)y * (double)num4 - (double)z * (double)num3);
            float num7 = (float)((double)z * (double)num2 - (double)x * (double)num4);
            float num8 = (float)((double)x * (double)num3 - (double)y * (double)num2);
            float num9 = (float)((double)x * (double)num2 + (double)y * (double)num3 + (double)z * (double)num4);
            result.X = (float)((double)x * (double)num5 + (double)num2 * (double)w) + num6;
            result.Y = (float)((double)y * (double)num5 + (double)num3 * (double)w) + num7;
            result.Z = (float)((double)z * (double)num5 + (double)num4 * (double)w) + num8;
            result.W = w * num5 - num9;
        }

        public static Quaternion operator -(Quaternion quaternion)
        {
            Quaternion quaternion1;
            quaternion1.X = -quaternion.X;
            quaternion1.Y = -quaternion.Y;
            quaternion1.Z = -quaternion.Z;
            quaternion1.W = -quaternion.W;
            return quaternion1;
        }

        public static bool operator ==(Quaternion quaternion1, Quaternion quaternion2)
        {
            return quaternion1.Equals(ref quaternion2);
        }

        public static bool operator !=(Quaternion quaternion1, Quaternion quaternion2)
        {
            return !quaternion1.Equals(ref quaternion2);
        }

        public static Quaternion operator +(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion quaternion;
            quaternion.X = quaternion1.X + quaternion2.X;
            quaternion.Y = quaternion1.Y + quaternion2.Y;
            quaternion.Z = quaternion1.Z + quaternion2.Z;
            quaternion.W = quaternion1.W + quaternion2.W;
            return quaternion;
        }

        public static Quaternion operator -(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion quaternion;
            quaternion.X = quaternion1.X - quaternion2.X;
            quaternion.Y = quaternion1.Y - quaternion2.Y;
            quaternion.Z = quaternion1.Z - quaternion2.Z;
            quaternion.W = quaternion1.W - quaternion2.W;
            return quaternion;
        }

        public static Quaternion operator *(Quaternion quaternion1, Quaternion quaternion2)
        {
            float x1 = quaternion1.X;
            float y1 = quaternion1.Y;
            float z1 = quaternion1.Z;
            float w1 = quaternion1.W;
            float x2 = quaternion2.X;
            float y2 = quaternion2.Y;
            float z2 = quaternion2.Z;
            float w2 = quaternion2.W;
            float num1 = (float)((double)y1 * (double)z2 - (double)z1 * (double)y2);
            float num2 = (float)((double)z1 * (double)x2 - (double)x1 * (double)z2);
            float num3 = (float)((double)x1 * (double)y2 - (double)y1 * (double)x2);
            float num4 = (float)((double)x1 * (double)x2 + (double)y1 * (double)y2 + (double)z1 * (double)z2);
            Quaternion quaternion;
            quaternion.X = (float)((double)x1 * (double)w2 + (double)x2 * (double)w1) + num1;
            quaternion.Y = (float)((double)y1 * (double)w2 + (double)y2 * (double)w1) + num2;
            quaternion.Z = (float)((double)z1 * (double)w2 + (double)z2 * (double)w1) + num3;
            quaternion.W = w1 * w2 - num4;
            return quaternion;
        }

        public static Quaternion operator *(Quaternion quaternion1, float scaleFactor)
        {
            Quaternion quaternion;
            quaternion.X = quaternion1.X * scaleFactor;
            quaternion.Y = quaternion1.Y * scaleFactor;
            quaternion.Z = quaternion1.Z * scaleFactor;
            quaternion.W = quaternion1.W * scaleFactor;
            return quaternion;
        }

        public static Vector3 operator *(Quaternion rotation, Vector3 point)
        {
            float num1 = rotation.X * 2f;
            float num2 = rotation.Y * 2f;
            float num3 = rotation.Z * 2f;
            float num4 = rotation.X * num1;
            float num5 = rotation.Y * num2;
            float num6 = rotation.Z * num3;
            float num7 = rotation.X * num2;
            float num8 = rotation.X * num3;
            float num9 = rotation.Y * num3;
            float num10 = rotation.W * num1;
            float num11 = rotation.W * num2;
            float num12 = rotation.W * num3;
            Vector3 vector3;
            vector3.X = (float)((1.0 - ((double)num5 + (double)num6)) * (double)point.X + ((double)num7 - (double)num12) * (double)point.Y + ((double)num8 + (double)num11) * (double)point.Z);
            vector3.Y = (float)(((double)num7 + (double)num12) * (double)point.X + (1.0 - ((double)num4 + (double)num6)) * (double)point.Y + ((double)num9 - (double)num10) * (double)point.Z);
            vector3.Z = (float)(((double)num8 - (double)num11) * (double)point.X + ((double)num9 + (double)num10) * (double)point.Y + (1.0 - ((double)num4 + (double)num5)) * (double)point.Z);
            return vector3;
        }

        public static Quaternion operator /(Quaternion quaternion1, Quaternion quaternion2)
        {
            float x = quaternion1.X;
            float y = quaternion1.Y;
            float z = quaternion1.Z;
            float w = quaternion1.W;
            float num1 = 1f / (float)((double)quaternion2.X * (double)quaternion2.X + (double)quaternion2.Y * (double)quaternion2.Y + (double)quaternion2.Z * (double)quaternion2.Z + (double)quaternion2.W * (double)quaternion2.W);
            float num2 = -quaternion2.X * num1;
            float num3 = -quaternion2.Y * num1;
            float num4 = -quaternion2.Z * num1;
            float num5 = quaternion2.W * num1;
            float num6 = (float)((double)y * (double)num4 - (double)z * (double)num3);
            float num7 = (float)((double)z * (double)num2 - (double)x * (double)num4);
            float num8 = (float)((double)x * (double)num3 - (double)y * (double)num2);
            float num9 = (float)((double)x * (double)num2 + (double)y * (double)num3 + (double)z * (double)num4);
            Quaternion quaternion;
            quaternion.X = (float)((double)x * (double)num5 + (double)num2 * (double)w) + num6;
            quaternion.Y = (float)((double)y * (double)num5 + (double)num3 * (double)w) + num7;
            quaternion.Z = (float)((double)z * (double)num5 + (double)num4 * (double)w) + num8;
            quaternion.W = w * num5 - num9;
            return quaternion;
        }

        public static Vector3 ToEuler(Quaternion orientation)
        {
            Vector3 rotationaxes;
            Quaternion.ToEuler(ref orientation, out rotationaxes);
            return rotationaxes;
        }

        public static void ToEuler(ref Quaternion orientation, out Vector3 rotationaxes)
        {
            Vector3 location = Vector3.Transform(Vector3.Forward, orientation);
            Vector3 position = Vector3.Transform(Vector3.Up, orientation);
            rotationaxes = Quaternion.AngleTo(new Vector3(), location);
            if ((double)rotationaxes.X == 1.57079601287842)
            {
                rotationaxes.Y = Quaternion.ArcTanAngle(position.Z, position.X);
                rotationaxes.Z = 0.0f;
            }
            else if ((double)rotationaxes.X == -1.57079601287842)
            {
                rotationaxes.Y = Quaternion.ArcTanAngle(-position.Z, -position.X);
                rotationaxes.Z = 0.0f;
            }
            else
            {
                Vector3 vector3 = Vector3.Transform(Vector3.Transform(position, Matrix.CreateRotationY(-rotationaxes.Y)), Matrix.CreateRotationX(-rotationaxes.X));
                rotationaxes.Z = Quaternion.ArcTanAngle(vector3.Y, -vector3.X);
            }
        }

        public static void ToAngleAxis(ref Quaternion orientation, out Vector3 axis, out float angle)
        {
            if ((double)orientation.LengthSquared() > 0.0)
            {
                angle = (float)(2.0 * System.Math.Acos((double)orientation.W));
                axis.X = orientation.X;
                axis.Y = orientation.Y;
                axis.Z = orientation.Z;
                double num = (double)axis.Normalize();
            }
            else
            {
                angle = 0.0f;
                axis.X = 1f;
                axis.Y = 0.0f;
                axis.Z = 0.0f;
            }
        }

        private static float ArcTanAngle(float x, float y)
        {
            if ((double)x == 0.0)
                return (double)y == 1.0 ? 1.570796f : -1.570796f;
            if ((double)x > 0.0)
                return (float)System.Math.Atan((double)y / (double)x);
            if ((double)x >= 0.0)
                return 0.0f;
            if ((double)y > 0.0)
                return (float)System.Math.Atan((double)y / (double)x) + 3.141593f;
            return (float)System.Math.Atan((double)y / (double)x) - 3.141593f;
        }

        private static Vector3 AngleTo(Vector3 from, Vector3 location)
        {
            Vector3 vector3_1 = new Vector3();
            Vector3 vector3_2 = Vector3.Normalize(location - from);
            vector3_1.X = (float)System.Math.Asin((double)vector3_2.Y);
            vector3_1.Y = Quaternion.ArcTanAngle(-vector3_2.Z, -vector3_2.X);
            return vector3_1;
        }
    }
}
