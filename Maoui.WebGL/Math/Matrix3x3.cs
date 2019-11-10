using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace WebGl
{
    [DataContract]
    public struct Matrix3x3 : IEquatable<Matrix3x3>
    {
        private static readonly Matrix3x3 identity = new Matrix3x3(1f, 0.0f, 0.0f, 0.0f, 1f, 0.0f, 0.0f, 0.0f, 1f);
        [DataMember]
        public float M11;
        [DataMember]
        public float M12;
        [DataMember]
        public float M13;
        [DataMember]
        public float M21;
        [DataMember]
        public float M22;
        [DataMember]
        public float M23;
        [DataMember]
        public float M31;
        [DataMember]
        public float M32;
        [DataMember]
        public float M33;

        public static Matrix3x3 Identity
        {
            get
            {
                return Matrix3x3.identity;
            }
        }

        public Vector2 Up
        {
            get
            {
                Vector2 vector2;
                vector2.X = this.M21;
                vector2.Y = this.M22;
                return vector2;
            }
            set
            {
                this.M21 = value.X;
                this.M22 = value.Y;
            }
        }

        public Vector2 Down
        {
            get
            {
                Vector2 vector2;
                vector2.X = -this.M21;
                vector2.Y = -this.M22;
                return vector2;
            }
            set
            {
                this.M21 = -value.X;
                this.M22 = -value.Y;
            }
        }

        public Vector2 Right
        {
            get
            {
                Vector2 vector2;
                vector2.X = this.M11;
                vector2.Y = this.M12;
                return vector2;
            }
            set
            {
                this.M11 = value.X;
                this.M12 = value.Y;
            }
        }

        public Vector2 Left
        {
            get
            {
                Vector2 vector2;
                vector2.X = -this.M11;
                vector2.Y = -this.M12;
                return vector2;
            }
            set
            {
                this.M11 = -value.X;
                this.M12 = -value.Y;
            }
        }

        public Vector2 Translation
        {
            get
            {
                Vector2 vector2;
                vector2.X = this.M31;
                vector2.Y = this.M32;
                return vector2;
            }
            set
            {
                this.M31 = value.X;
                this.M32 = value.Y;
            }
        }

        public float Rotation
        {
            get
            {
                return (float)System.Math.Atan2((double)this.M12, (double)this.M22);
            }
        }

        public Vector2 Scale
        {
            get
            {
                return new Vector2((float)System.Math.Sqrt((double)this.M11 * (double)this.M11 + (double)this.M12 * (double)this.M12 + (double)this.M13 * (double)this.M13), (float)System.Math.Sqrt((double)this.M21 * (double)this.M21 + (double)this.M22 * (double)this.M22 + (double)this.M23 * (double)this.M23));
            }
        }

        public Matrix3x3(
          float m11,
          float m12,
          float m13,
          float m21,
          float m22,
          float m23,
          float m31,
          float m32,
          float m33)
        {
            this.M11 = m11;
            this.M12 = m12;
            this.M13 = m13;
            this.M21 = m21;
            this.M22 = m22;
            this.M23 = m23;
            this.M31 = m31;
            this.M32 = m32;
            this.M33 = m33;
        }

        public static Matrix3x3 CreateTranslation(Vector2 position)
        {
            Matrix3x3 result;
            Matrix3x3.CreateTranslation(ref position, out result);
            return result;
        }

        public static void CreateTranslation(ref Vector2 position, out Matrix3x3 result)
        {
            result.M11 = 1f;
            result.M12 = 0.0f;
            result.M13 = 0.0f;
            result.M21 = 0.0f;
            result.M22 = 1f;
            result.M23 = 0.0f;
            result.M31 = position.X;
            result.M32 = position.Y;
            result.M33 = 1f;
        }

        public static Matrix3x3 CreateTranslation(float xPosition, float yPosition)
        {
            Matrix3x3 result;
            Matrix3x3.CreateTranslation(xPosition, yPosition, out result);
            return result;
        }

        public static void CreateTranslation(float xPosition, float yPosition, out Matrix3x3 result)
        {
            result.M11 = 1f;
            result.M12 = 0.0f;
            result.M13 = 0.0f;
            result.M21 = 0.0f;
            result.M22 = 1f;
            result.M23 = 0.0f;
            result.M31 = xPosition;
            result.M32 = yPosition;
            result.M33 = 1f;
        }

        public static Matrix3x3 CreateRotation(float angle)
        {
            float num1 = (float)System.Math.Cos((double)angle);
            float num2 = (float)System.Math.Sin((double)angle);
            Matrix3x3 matrix3x3;
            matrix3x3.M11 = num1;
            matrix3x3.M12 = -num2;
            matrix3x3.M13 = 0.0f;
            matrix3x3.M21 = num2;
            matrix3x3.M22 = num1;
            matrix3x3.M23 = 0.0f;
            matrix3x3.M31 = 0.0f;
            matrix3x3.M32 = 0.0f;
            matrix3x3.M33 = 1f;
            return matrix3x3;
        }

        public static Matrix3x3 CreateRotation(Vector2 translate, float angle)
        {
            float num1 = (float)System.Math.Cos((double)angle);
            float num2 = (float)System.Math.Sin((double)angle);
            Matrix3x3 matrix3x3;
            matrix3x3.M11 = num1;
            matrix3x3.M12 = -num2;
            matrix3x3.M13 = (float)(-(double)translate.X * (double)num1 + (double)translate.Y * (double)num2) + translate.X;
            matrix3x3.M21 = num2;
            matrix3x3.M22 = num1;
            matrix3x3.M23 = (float)(-(double)translate.X * (double)num2 - (double)translate.Y * (double)num1) + translate.Y;
            matrix3x3.M31 = 0.0f;
            matrix3x3.M32 = 0.0f;
            matrix3x3.M33 = 1f;
            return matrix3x3;
        }

        public static Matrix3x3 CreateScale(float xScale, float yScale)
        {
            Matrix3x3 result;
            Matrix3x3.CreateScale(xScale, yScale, out result);
            return result;
        }

        public static void CreateScale(float xScale, float yScale, out Matrix3x3 result)
        {
            result.M11 = xScale;
            result.M12 = 0.0f;
            result.M13 = 0.0f;
            result.M21 = 0.0f;
            result.M22 = yScale;
            result.M23 = 0.0f;
            result.M31 = 0.0f;
            result.M32 = 0.0f;
            result.M33 = 1f;
        }

        public static Matrix3x3 CreateScale(Vector2 scales)
        {
            Matrix3x3 result;
            Matrix3x3.CreateScale(ref scales, out result);
            return result;
        }

        public static void CreateScale(ref Vector2 scales, out Matrix3x3 result)
        {
            result.M11 = scales.X;
            result.M12 = 0.0f;
            result.M13 = 0.0f;
            result.M21 = 0.0f;
            result.M22 = scales.Y;
            result.M23 = 0.0f;
            result.M31 = 0.0f;
            result.M32 = 0.0f;
            result.M33 = 1f;
        }

        public static Matrix3x3 CreateScale(float scale)
        {
            Matrix3x3 result;
            Matrix3x3.CreateScale(scale, out result);
            return result;
        }

        public static void CreateScale(float scale, out Matrix3x3 result)
        {
            result.M11 = scale;
            result.M12 = 0.0f;
            result.M13 = 0.0f;
            result.M21 = 0.0f;
            result.M22 = scale;
            result.M23 = 0.0f;
            result.M31 = 0.0f;
            result.M32 = 0.0f;
            result.M33 = 1f;
        }

        public static Matrix3x3 CreateTransform(Vector2 translate, float angle, Vector2 scale)
        {
            float num1 = (float)System.Math.Cos((double)angle);
            float num2 = (float)System.Math.Sin((double)angle);
            Matrix3x3 matrix3x3;
            matrix3x3.M11 = scale.X * num1;
            matrix3x3.M12 = -num2 * scale.X;
            matrix3x3.M13 = (float)((double)translate.X * (double)scale.X * (double)num1 - (double)num2 * (double)scale.X * (double)translate.Y);
            matrix3x3.M21 = scale.X * num2;
            matrix3x3.M22 = scale.Y * num1;
            matrix3x3.M23 = (float)((double)translate.X * (double)scale.X * (double)num2 + (double)translate.Y * (double)scale.Y * (double)num1);
            matrix3x3.M31 = 0.0f;
            matrix3x3.M32 = 0.0f;
            matrix3x3.M33 = 1f;
            return matrix3x3;
        }

        public static Matrix3x3 CreateTransform(Vector2 translate, float angle, float scale)
        {
            float num1 = (float)System.Math.Cos((double)angle);
            float num2 = (float)System.Math.Sin((double)angle);
            Matrix3x3 matrix3x3;
            matrix3x3.M11 = scale * num1;
            matrix3x3.M12 = -num2 * scale;
            matrix3x3.M13 = (float)((double)translate.X * (double)scale * (double)num1 - (double)num2 * (double)scale * (double)translate.Y);
            matrix3x3.M21 = scale * num2;
            matrix3x3.M22 = scale * num1;
            matrix3x3.M23 = (float)((double)translate.X * (double)scale * (double)num2 + (double)translate.Y * (double)scale * (double)num1);
            matrix3x3.M31 = 0.0f;
            matrix3x3.M32 = 0.0f;
            matrix3x3.M33 = 1f;
            return matrix3x3;
        }

        public static Matrix3x3 CreateFromQuaternion(Quaternion quaternion)
        {
            Matrix3x3 matrix;
            Matrix3x3.CreateFromQuaternion(ref quaternion, out matrix);
            return matrix;
        }

        public static void CreateFromQuaternion(ref Quaternion quaternion, out Matrix3x3 matrix)
        {
            float num1 = quaternion.Z * quaternion.Z;
            float num2 = quaternion.Z * quaternion.W;
            matrix.M11 = (float)(1.0 - 2.0 * (double)num1);
            matrix.M12 = 2f * num2;
            matrix.M13 = 0.0f;
            matrix.M21 = 2f * num2;
            matrix.M22 = (float)(1.0 - 2.0 * (double)num1);
            matrix.M23 = 0.0f;
            matrix.M31 = 0.0f;
            matrix.M32 = 0.0f;
            matrix.M33 = 1f;
        }

        public override string ToString()
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            return "{ " + string.Format((IFormatProvider)currentCulture, "{{M11:{0} M12:{1} M13:{2} M14:{3}}} ", (object)this.M11.ToString((IFormatProvider)currentCulture), (object)this.M12.ToString((IFormatProvider)currentCulture), (object)this.M13.ToString((IFormatProvider)currentCulture)) + string.Format((IFormatProvider)currentCulture, "{{M21:{0} M22:{1} M23:{2} M24:{3}}} ", (object)this.M21.ToString((IFormatProvider)currentCulture), (object)this.M22.ToString((IFormatProvider)currentCulture), (object)this.M23.ToString((IFormatProvider)currentCulture)) + string.Format((IFormatProvider)currentCulture, "{{M31:{0} M32:{1} M33:{2} M34:{3}}} ", (object)this.M31.ToString((IFormatProvider)currentCulture), (object)this.M32.ToString((IFormatProvider)currentCulture), (object)this.M33.ToString((IFormatProvider)currentCulture)) + "}";
        }

        public bool Equals(Matrix3x3 other)
        {
            return this.Equals(ref other);
        }

        public bool Equals(ref Matrix3x3 other)
        {
            if (this.M11.Equal(other.M11, 0.0001f) && this.M22.Equal(other.M22, 0.0001f) && (this.M33.Equal(other.M33, 0.0001f) && this.M12.Equal(other.M12, 0.0001f)) && (this.M13.Equal(other.M13, 0.0001f) && this.M21.Equal(other.M21, 0.0001f) && (this.M23.Equal(other.M23, 0.0001f) && this.M31.Equal(other.M31, 0.0001f))))
                return this.M32.Equal(other.M32, 0.0001f);
            return false;
        }

        public override bool Equals(object obj)
        {
            bool flag = false;
            if (obj is Matrix)
                flag = this.Equals((object)(Matrix)obj);
            return flag;
        }

        public override int GetHashCode()
        {
            return this.M11.GetHashCode() + this.M12.GetHashCode() + this.M13.GetHashCode() + this.M21.GetHashCode() + this.M22.GetHashCode() + this.M23.GetHashCode() + this.M31.GetHashCode() + this.M32.GetHashCode() + this.M33.GetHashCode();
        }

        public static Matrix3x3 Transpose(Matrix3x3 matrix)
        {
            Matrix3x3 result;
            Matrix3x3.Transpose(ref matrix, out result);
            return result;
        }

        public static void Transpose(ref Matrix3x3 matrix, out Matrix3x3 result)
        {
            float m11 = matrix.M11;
            float m12 = matrix.M12;
            float m13 = matrix.M13;
            float m21 = matrix.M21;
            float m22 = matrix.M22;
            float m23 = matrix.M23;
            float m31 = matrix.M31;
            float m32 = matrix.M32;
            float m33 = matrix.M33;
            result.M11 = m11;
            result.M12 = m21;
            result.M13 = m31;
            result.M21 = m12;
            result.M22 = m22;
            result.M23 = m32;
            result.M31 = m13;
            result.M32 = m23;
            result.M33 = m33;
        }

        public void SetSameDiagonal(float d)
        {
            this.M11 = this.M22 = this.M33 = d;
        }

        public float Determinant()
        {
            return (float)((double)this.M11 * (double)this.M22 * (double)this.M33 + (double)this.M12 * (double)this.M23 * (double)this.M31 + (double)this.M13 * (double)this.M21 * (double)this.M32 - (double)this.M31 * (double)this.M22 * (double)this.M13 - (double)this.M32 * (double)this.M23 * (double)this.M11 - (double)this.M33 * (double)this.M21 * (double)this.M12);
        }

        public static Matrix3x3 Invert(Matrix3x3 matrix)
        {
            Matrix3x3 result;
            Matrix3x3.Invert(ref matrix, out result);
            return result;
        }

        public static void Invert(ref Matrix3x3 matrix, out Matrix3x3 result)
        {
            float num1 = 1f / matrix.Determinant();
            float num2 = (float)((double)matrix.M22 * (double)matrix.M33 - (double)matrix.M23 * (double)matrix.M32) * num1;
            float num3 = (float)((double)matrix.M13 * (double)matrix.M32 - (double)matrix.M33 * (double)matrix.M12) * num1;
            float num4 = (float)((double)matrix.M12 * (double)matrix.M23 - (double)matrix.M22 * (double)matrix.M13) * num1;
            float num5 = (float)((double)matrix.M23 * (double)matrix.M31 - (double)matrix.M21 * (double)matrix.M33) * num1;
            float num6 = (float)((double)matrix.M11 * (double)matrix.M33 - (double)matrix.M13 * (double)matrix.M31) * num1;
            float num7 = (float)((double)matrix.M13 * (double)matrix.M21 - (double)matrix.M11 * (double)matrix.M23) * num1;
            float num8 = (float)((double)matrix.M21 * (double)matrix.M32 - (double)matrix.M22 * (double)matrix.M31) * num1;
            float num9 = (float)((double)matrix.M12 * (double)matrix.M31 - (double)matrix.M11 * (double)matrix.M32) * num1;
            float num10 = (float)((double)matrix.M11 * (double)matrix.M22 - (double)matrix.M12 * (double)matrix.M21) * num1;
            result.M11 = num2;
            result.M12 = num3;
            result.M13 = num4;
            result.M21 = num5;
            result.M22 = num6;
            result.M23 = num7;
            result.M31 = num8;
            result.M32 = num9;
            result.M33 = num10;
        }

        public static Matrix3x3 Lerp(Matrix3x3 matrix1, Matrix3x3 matrix2, float amount)
        {
            Matrix3x3 result;
            Matrix3x3.Lerp(ref matrix1, ref matrix2, amount, out result);
            return result;
        }

        public static void Lerp(
          ref Matrix3x3 matrix1,
          ref Matrix3x3 matrix2,
          float amount,
          out Matrix3x3 result)
        {
            result.M11 = matrix1.M11 + (matrix2.M11 - matrix1.M11) * amount;
            result.M12 = matrix1.M12 + (matrix2.M12 - matrix1.M12) * amount;
            result.M13 = matrix1.M13 + (matrix2.M13 - matrix1.M13) * amount;
            result.M21 = matrix1.M21 + (matrix2.M21 - matrix1.M21) * amount;
            result.M22 = matrix1.M22 + (matrix2.M22 - matrix1.M22) * amount;
            result.M23 = matrix1.M23 + (matrix2.M23 - matrix1.M23) * amount;
            result.M31 = matrix1.M31 + (matrix2.M31 - matrix1.M31) * amount;
            result.M32 = matrix1.M32 + (matrix2.M32 - matrix1.M32) * amount;
            result.M33 = matrix1.M33 + (matrix2.M33 - matrix1.M33) * amount;
        }

        public static Matrix3x3 Negate(Matrix3x3 matrix)
        {
            Matrix3x3 result;
            Matrix3x3.Negate(ref matrix, out result);
            return result;
        }

        public static void Negate(ref Matrix3x3 matrix, out Matrix3x3 result)
        {
            result.M11 = -matrix.M11;
            result.M12 = -matrix.M12;
            result.M13 = -matrix.M13;
            result.M21 = -matrix.M21;
            result.M22 = -matrix.M22;
            result.M23 = -matrix.M23;
            result.M31 = -matrix.M31;
            result.M32 = -matrix.M32;
            result.M33 = -matrix.M33;
        }

        public static Matrix3x3 Add(Matrix3x3 matrix1, Matrix3x3 matrix2)
        {
            Matrix3x3 result;
            Matrix3x3.Add(ref matrix1, ref matrix2, out result);
            return result;
        }

        public static void Add(ref Matrix3x3 matrix1, ref Matrix3x3 matrix2, out Matrix3x3 result)
        {
            result.M11 = matrix1.M11 + matrix2.M11;
            result.M12 = matrix1.M12 + matrix2.M12;
            result.M13 = matrix1.M13 + matrix2.M13;
            result.M21 = matrix1.M21 + matrix2.M21;
            result.M22 = matrix1.M22 + matrix2.M22;
            result.M23 = matrix1.M23 + matrix2.M23;
            result.M31 = matrix1.M31 + matrix2.M31;
            result.M32 = matrix1.M32 + matrix2.M32;
            result.M33 = matrix1.M33 + matrix2.M33;
        }

        public static Matrix3x3 Subtract(Matrix3x3 matrix1, Matrix3x3 matrix2)
        {
            Matrix3x3 result;
            Matrix3x3.Subtract(ref matrix1, ref matrix2, out result);
            return result;
        }

        public static void Subtract(ref Matrix3x3 matrix1, ref Matrix3x3 matrix2, out Matrix3x3 result)
        {
            result.M11 = matrix1.M11 - matrix2.M11;
            result.M12 = matrix1.M12 - matrix2.M12;
            result.M13 = matrix1.M13 - matrix2.M13;
            result.M21 = matrix1.M21 - matrix2.M21;
            result.M22 = matrix1.M22 - matrix2.M22;
            result.M23 = matrix1.M23 - matrix2.M23;
            result.M31 = matrix1.M31 - matrix2.M31;
            result.M32 = matrix1.M32 - matrix2.M32;
            result.M33 = matrix1.M33 - matrix2.M33;
        }

        public static Matrix3x3 Multiply(Matrix3x3 matrix1, Matrix3x3 matrix2)
        {
            Matrix3x3 result;
            Matrix3x3.Multiply(ref matrix1, ref matrix2, out result);
            return result;
        }

        public static void Multiply(ref Matrix3x3 matrix1, ref Matrix3x3 matrix2, out Matrix3x3 result)
        {
            float num1 = (float)((double)matrix1.M11 * (double)matrix2.M11 + (double)matrix1.M12 * (double)matrix2.M21 + (double)matrix1.M13 * (double)matrix2.M31);
            float num2 = (float)((double)matrix1.M11 * (double)matrix2.M12 + (double)matrix1.M12 * (double)matrix2.M22 + (double)matrix1.M13 * (double)matrix2.M32);
            float num3 = (float)((double)matrix1.M11 * (double)matrix2.M13 + (double)matrix1.M12 * (double)matrix2.M23 + (double)matrix1.M13 * (double)matrix2.M33);
            float num4 = (float)((double)matrix1.M21 * (double)matrix2.M11 + (double)matrix1.M22 * (double)matrix2.M21 + (double)matrix1.M23 * (double)matrix2.M31);
            float num5 = (float)((double)matrix1.M21 * (double)matrix2.M12 + (double)matrix1.M22 * (double)matrix2.M22 + (double)matrix1.M23 * (double)matrix2.M32);
            float num6 = (float)((double)matrix1.M21 * (double)matrix2.M13 + (double)matrix1.M22 * (double)matrix2.M23 + (double)matrix1.M23 * (double)matrix2.M33);
            float num7 = (float)((double)matrix1.M31 * (double)matrix2.M11 + (double)matrix1.M32 * (double)matrix2.M21 + (double)matrix1.M33 * (double)matrix2.M31);
            float num8 = (float)((double)matrix1.M31 * (double)matrix2.M12 + (double)matrix1.M32 * (double)matrix2.M22 + (double)matrix1.M33 * (double)matrix2.M32);
            float num9 = (float)((double)matrix1.M31 * (double)matrix2.M13 + (double)matrix1.M32 * (double)matrix2.M23 + (double)matrix1.M33 * (double)matrix2.M33);
            result.M11 = num1;
            result.M12 = num2;
            result.M13 = num3;
            result.M21 = num4;
            result.M22 = num5;
            result.M23 = num6;
            result.M31 = num7;
            result.M32 = num8;
            result.M33 = num9;
        }

        public static Matrix3x3 Multiply(Matrix3x3 matrix1, float scaleFactor)
        {
            Matrix3x3 result;
            Matrix3x3.Multiply(ref matrix1, scaleFactor, out result);
            return result;
        }

        public static void Multiply(ref Matrix3x3 matrix1, float scaleFactor, out Matrix3x3 result)
        {
            result.M11 = matrix1.M11 * scaleFactor;
            result.M12 = matrix1.M12 * scaleFactor;
            result.M13 = matrix1.M13 * scaleFactor;
            result.M21 = matrix1.M21 * scaleFactor;
            result.M22 = matrix1.M22 * scaleFactor;
            result.M23 = matrix1.M23 * scaleFactor;
            result.M31 = matrix1.M31 * scaleFactor;
            result.M32 = matrix1.M32 * scaleFactor;
            result.M33 = matrix1.M33 * scaleFactor;
        }

        public static Matrix3x3 Divide(Matrix3x3 matrix1, Matrix3x3 matrix2)
        {
            Matrix3x3 result;
            Matrix3x3.Divide(ref matrix1, ref matrix2, out result);
            return result;
        }

        public static void Divide(ref Matrix3x3 matrix1, ref Matrix3x3 matrix2, out Matrix3x3 result)
        {
            result.M11 = matrix1.M11 / matrix2.M11;
            result.M12 = matrix1.M12 / matrix2.M12;
            result.M13 = matrix1.M13 / matrix2.M13;
            result.M21 = matrix1.M21 / matrix2.M21;
            result.M22 = matrix1.M22 / matrix2.M22;
            result.M23 = matrix1.M23 / matrix2.M23;
            result.M31 = matrix1.M31 / matrix2.M31;
            result.M32 = matrix1.M32 / matrix2.M32;
            result.M33 = matrix1.M33 / matrix2.M33;
        }

        public static Matrix3x3 Divide(Matrix3x3 matrix1, float divider)
        {
            Matrix3x3 result;
            Matrix3x3.Divide(ref matrix1, divider, out result);
            return result;
        }

        public static void Divide(ref Matrix3x3 matrix1, float divider, out Matrix3x3 result)
        {
            float num = 1f / divider;
            result.M11 = matrix1.M11 * num;
            result.M12 = matrix1.M12 * num;
            result.M13 = matrix1.M13 * num;
            result.M21 = matrix1.M21 * num;
            result.M22 = matrix1.M22 * num;
            result.M23 = matrix1.M23 * num;
            result.M31 = matrix1.M31 * num;
            result.M32 = matrix1.M32 * num;
            result.M33 = matrix1.M33 * num;
        }

        public static Matrix ToMatrix4x4(Matrix3x3 a)
        {
            Matrix b;
            Matrix3x3.ToMatrix4x4(ref a, out b);
            return b;
        }

        public static void ToMatrix4x4(ref Matrix3x3 a, out Matrix b)
        {
            b.M11 = a.M11;
            b.M12 = a.M12;
            b.M13 = a.M13;
            b.M14 = 0.0f;
            b.M21 = a.M21;
            b.M22 = a.M22;
            b.M23 = a.M23;
            b.M24 = 0.0f;
            b.M31 = a.M31;
            b.M32 = a.M32;
            b.M33 = a.M33;
            b.M34 = 0.0f;
            b.M41 = 0.0f;
            b.M42 = 0.0f;
            b.M43 = 0.0f;
            b.M44 = 1f;
        }

        public static Matrix3x3 operator -(Matrix3x3 matrix1)
        {
            Matrix3x3 matrix3x3;
            matrix3x3.M11 = -matrix1.M11;
            matrix3x3.M12 = -matrix1.M12;
            matrix3x3.M13 = -matrix1.M13;
            matrix3x3.M21 = -matrix1.M21;
            matrix3x3.M22 = -matrix1.M22;
            matrix3x3.M23 = -matrix1.M23;
            matrix3x3.M31 = -matrix1.M31;
            matrix3x3.M32 = -matrix1.M32;
            matrix3x3.M33 = -matrix1.M33;
            return matrix3x3;
        }

        public static bool operator ==(Matrix3x3 matrix1, Matrix3x3 matrix2)
        {
            return matrix1.Equals(ref matrix2);
        }

        public static bool operator !=(Matrix3x3 matrix1, Matrix3x3 matrix2)
        {
            return !matrix1.Equals(ref matrix2);
        }

        public static Matrix3x3 operator +(Matrix3x3 matrix1, Matrix3x3 matrix2)
        {
            Matrix3x3 matrix3x3;
            matrix3x3.M11 = matrix1.M11 + matrix2.M11;
            matrix3x3.M12 = matrix1.M12 + matrix2.M12;
            matrix3x3.M13 = matrix1.M13 + matrix2.M13;
            matrix3x3.M21 = matrix1.M21 + matrix2.M21;
            matrix3x3.M22 = matrix1.M22 + matrix2.M22;
            matrix3x3.M23 = matrix1.M23 + matrix2.M23;
            matrix3x3.M31 = matrix1.M31 + matrix2.M31;
            matrix3x3.M32 = matrix1.M32 + matrix2.M32;
            matrix3x3.M33 = matrix1.M33 + matrix2.M33;
            return matrix3x3;
        }

        public static Matrix3x3 operator -(Matrix3x3 matrix1, Matrix3x3 matrix2)
        {
            Matrix3x3 matrix3x3;
            matrix3x3.M11 = matrix1.M11 - matrix2.M11;
            matrix3x3.M12 = matrix1.M12 - matrix2.M12;
            matrix3x3.M13 = matrix1.M13 - matrix2.M13;
            matrix3x3.M21 = matrix1.M21 - matrix2.M21;
            matrix3x3.M22 = matrix1.M22 - matrix2.M22;
            matrix3x3.M23 = matrix1.M23 - matrix2.M23;
            matrix3x3.M31 = matrix1.M31 - matrix2.M31;
            matrix3x3.M32 = matrix1.M32 - matrix2.M32;
            matrix3x3.M33 = matrix1.M33 - matrix2.M33;
            return matrix3x3;
        }

        public static Matrix3x3 operator *(Matrix3x3 matrix1, Matrix3x3 matrix2)
        {
            Matrix3x3 matrix3x3;
            matrix3x3.M11 = (float)((double)matrix1.M11 * (double)matrix2.M11 + (double)matrix1.M12 * (double)matrix2.M21 + (double)matrix1.M13 * (double)matrix2.M31);
            matrix3x3.M12 = (float)((double)matrix1.M11 * (double)matrix2.M12 + (double)matrix1.M12 * (double)matrix2.M22 + (double)matrix1.M13 * (double)matrix2.M32);
            matrix3x3.M13 = (float)((double)matrix1.M11 * (double)matrix2.M13 + (double)matrix1.M12 * (double)matrix2.M23 + (double)matrix1.M13 * (double)matrix2.M33);
            matrix3x3.M21 = (float)((double)matrix1.M21 * (double)matrix2.M11 + (double)matrix1.M22 * (double)matrix2.M21 + (double)matrix1.M23 * (double)matrix2.M31);
            matrix3x3.M22 = (float)((double)matrix1.M21 * (double)matrix2.M12 + (double)matrix1.M22 * (double)matrix2.M22 + (double)matrix1.M23 * (double)matrix2.M32);
            matrix3x3.M23 = (float)((double)matrix1.M21 * (double)matrix2.M13 + (double)matrix1.M22 * (double)matrix2.M23 + (double)matrix1.M23 * (double)matrix2.M33);
            matrix3x3.M31 = (float)((double)matrix1.M31 * (double)matrix2.M11 + (double)matrix1.M32 * (double)matrix2.M21 + (double)matrix1.M33 * (double)matrix2.M31);
            matrix3x3.M32 = (float)((double)matrix1.M31 * (double)matrix2.M12 + (double)matrix1.M32 * (double)matrix2.M22 + (double)matrix1.M33 * (double)matrix2.M32);
            matrix3x3.M33 = (float)((double)matrix1.M31 * (double)matrix2.M13 + (double)matrix1.M32 * (double)matrix2.M23 + (double)matrix1.M33 * (double)matrix2.M33);
            return matrix3x3;
        }

        public static Matrix3x3 operator *(Matrix3x3 matrix, float scaleFactor)
        {
            Matrix3x3 matrix3x3;
            matrix3x3.M11 = matrix.M11 * scaleFactor;
            matrix3x3.M12 = matrix.M12 * scaleFactor;
            matrix3x3.M13 = matrix.M13 * scaleFactor;
            matrix3x3.M21 = matrix.M21 * scaleFactor;
            matrix3x3.M22 = matrix.M22 * scaleFactor;
            matrix3x3.M23 = matrix.M23 * scaleFactor;
            matrix3x3.M31 = matrix.M31 * scaleFactor;
            matrix3x3.M32 = matrix.M32 * scaleFactor;
            matrix3x3.M33 = matrix.M33 * scaleFactor;
            return matrix3x3;
        }

        public static Matrix3x3 operator *(float scaleFactor, Matrix3x3 matrix)
        {
            Matrix3x3 matrix3x3;
            matrix3x3.M11 = matrix.M11 * scaleFactor;
            matrix3x3.M12 = matrix.M12 * scaleFactor;
            matrix3x3.M13 = matrix.M13 * scaleFactor;
            matrix3x3.M21 = matrix.M21 * scaleFactor;
            matrix3x3.M22 = matrix.M22 * scaleFactor;
            matrix3x3.M23 = matrix.M23 * scaleFactor;
            matrix3x3.M31 = matrix.M31 * scaleFactor;
            matrix3x3.M32 = matrix.M32 * scaleFactor;
            matrix3x3.M33 = matrix.M33 * scaleFactor;
            return matrix3x3;
        }

        public static Matrix3x3 operator /(Matrix3x3 matrix1, Matrix3x3 matrix2)
        {
            Matrix3x3 matrix3x3;
            matrix3x3.M11 = matrix1.M11 / matrix2.M11;
            matrix3x3.M12 = matrix1.M12 / matrix2.M12;
            matrix3x3.M13 = matrix1.M13 / matrix2.M13;
            matrix3x3.M21 = matrix1.M21 / matrix2.M21;
            matrix3x3.M22 = matrix1.M22 / matrix2.M22;
            matrix3x3.M23 = matrix1.M23 / matrix2.M23;
            matrix3x3.M31 = matrix1.M31 / matrix2.M31;
            matrix3x3.M32 = matrix1.M32 / matrix2.M32;
            matrix3x3.M33 = matrix1.M33 / matrix2.M33;
            return matrix3x3;
        }

        public static Matrix3x3 operator /(Matrix3x3 matrix1, float divider)
        {
            float num = 1f / divider;
            Matrix3x3 matrix3x3;
            matrix3x3.M11 = matrix1.M11 * num;
            matrix3x3.M12 = matrix1.M12 * num;
            matrix3x3.M13 = matrix1.M13 * num;
            matrix3x3.M21 = matrix1.M21 * num;
            matrix3x3.M22 = matrix1.M22 * num;
            matrix3x3.M23 = matrix1.M23 * num;
            matrix3x3.M31 = matrix1.M31 * num;
            matrix3x3.M32 = matrix1.M32 * num;
            matrix3x3.M33 = matrix1.M33 * num;
            return matrix3x3;
        }
    }
}
