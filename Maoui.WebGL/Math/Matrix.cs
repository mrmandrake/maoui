using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace WebGl
{
    [DataContract]
    public struct Matrix : IEquatable<Matrix>
    {
        private static readonly Matrix identity = new Matrix(1f, 0.0f, 0.0f, 0.0f, 0.0f, 1f, 0.0f, 0.0f, 0.0f, 0.0f, 1f, 0.0f, 0.0f, 0.0f, 0.0f, 1f);
        [DataMember]
        public float M11;
        [DataMember]
        public float M12;
        [DataMember]
        public float M13;
        [DataMember]
        public float M14;
        [DataMember]
        public float M21;
        [DataMember]
        public float M22;
        [DataMember]
        public float M23;
        [DataMember]
        public float M24;
        [DataMember]
        public float M31;
        [DataMember]
        public float M32;
        [DataMember]
        public float M33;
        [DataMember]
        public float M34;
        [DataMember]
        public float M41;
        [DataMember]
        public float M42;
        [DataMember]
        public float M43;
        [DataMember]
        public float M44;

        public static Matrix Identity
        {
            get
            {
                return Matrix.identity;
            }
        }

        public Vector3 Up
        {
            get
            {
                Vector3 vector3;
                vector3.X = this.M21;
                vector3.Y = this.M22;
                vector3.Z = this.M23;
                return vector3;
            }
            set
            {
                this.M21 = value.X;
                this.M22 = value.Y;
                this.M23 = value.Z;
            }
        }

        public Vector3 Down
        {
            get
            {
                Vector3 vector3;
                vector3.X = -this.M21;
                vector3.Y = -this.M22;
                vector3.Z = -this.M23;
                return vector3;
            }
            set
            {
                this.M21 = -value.X;
                this.M22 = -value.Y;
                this.M23 = -value.Z;
            }
        }

        public Vector3 Right
        {
            get
            {
                Vector3 vector3;
                vector3.X = this.M11;
                vector3.Y = this.M12;
                vector3.Z = this.M13;
                return vector3;
            }
            set
            {
                this.M11 = value.X;
                this.M12 = value.Y;
                this.M13 = value.Z;
            }
        }

        public Vector3 Left
        {
            get
            {
                Vector3 vector3;
                vector3.X = -this.M11;
                vector3.Y = -this.M12;
                vector3.Z = -this.M13;
                return vector3;
            }
            set
            {
                this.M11 = -value.X;
                this.M12 = -value.Y;
                this.M13 = -value.Z;
            }
        }

        public Vector3 Forward
        {
            get
            {
                Vector3 vector3;
                vector3.X = -this.M31;
                vector3.Y = -this.M32;
                vector3.Z = -this.M33;
                return vector3;
            }
            set
            {
                this.M31 = -value.X;
                this.M32 = -value.Y;
                this.M33 = -value.Z;
            }
        }

        public Vector3 Backward
        {
            get
            {
                Vector3 vector3;
                vector3.X = this.M31;
                vector3.Y = this.M32;
                vector3.Z = this.M33;
                return vector3;
            }
            set
            {
                this.M31 = value.X;
                this.M32 = value.Y;
                this.M33 = value.Z;
            }
        }

        public Vector3 Translation
        {
            get
            {
                Vector3 vector3;
                vector3.X = this.M41;
                vector3.Y = this.M42;
                vector3.Z = this.M43;
                return vector3;
            }
            set
            {
                this.M41 = value.X;
                this.M42 = value.Y;
                this.M43 = value.Z;
            }
        }

        public Vector3 Rotation
        {
            get
            {
                return new Vector3((float)System.Math.Asin(-(double)this.M32), (float)System.Math.Atan2((double)this.M31, (double)this.M33), (float)System.Math.Atan2((double)this.M12, (double)this.M22));
            }
        }

        public Quaternion Orientation
        {
            get
            {
                Quaternion quaternion;
                Quaternion.CreateFromRotationMatrix(ref this, out quaternion);
                return quaternion;
            }
        }

        public Vector3 Scale
        {
            get
            {
                return new Vector3((float)System.Math.Sqrt((double)this.M11 * (double)this.M11 + (double)this.M12 * (double)this.M12 + (double)this.M13 * (double)this.M13), (float)System.Math.Sqrt((double)this.M21 * (double)this.M21 + (double)this.M22 * (double)this.M22 + (double)this.M23 * (double)this.M23), (float)System.Math.Sqrt((double)this.M31 * (double)this.M31 + (double)this.M32 * (double)this.M32 + (double)this.M33 * (double)this.M33));
            }
        }

        public Matrix Basis
        {
            get
            {
                return new Matrix(this.M11, this.M12, this.M13, 0.0f, this.M21, this.M22, this.M23, 0.0f, this.M31, this.M32, this.M33, 0.0f, 0.0f, 0.0f, 0.0f, 1f);
            }
            set
            {
                this.M11 = value.M11;
                this.M12 = value.M12;
                this.M13 = value.M13;
                this.M21 = value.M21;
                this.M22 = value.M22;
                this.M23 = value.M23;
                this.M31 = value.M31;
                this.M32 = value.M32;
                this.M33 = value.M33;
            }
        }

        public Vector4 Row1
        {
            get
            {
                return new Vector4(this.M11, this.M12, this.M13, this.M14);
            }
            set
            {
                this.M11 = value.X;
                this.M12 = value.Y;
                this.M13 = value.Z;
                this.M14 = value.W;
            }
        }

        public Vector4 Row2
        {
            get
            {
                return new Vector4(this.M21, this.M22, this.M23, this.M24);
            }
            set
            {
                this.M21 = value.X;
                this.M22 = value.Y;
                this.M23 = value.Z;
                this.M24 = value.W;
            }
        }

        public Vector4 Row3
        {
            get
            {
                return new Vector4(this.M31, this.M32, this.M33, this.M34);
            }
            set
            {
                this.M31 = value.X;
                this.M32 = value.Y;
                this.M33 = value.Z;
                this.M34 = value.W;
            }
        }

        public Vector4 Row4
        {
            get
            {
                return new Vector4(this.M41, this.M42, this.M43, this.M44);
            }
            set
            {
                this.M41 = value.X;
                this.M42 = value.Y;
                this.M43 = value.Z;
                this.M44 = value.W;
            }
        }

        public Vector4 Column1
        {
            get
            {
                return new Vector4(this.M11, this.M21, this.M31, this.M41);
            }
            set
            {
                this.M11 = value.X;
                this.M21 = value.Y;
                this.M31 = value.Z;
                this.M41 = value.W;
            }
        }

        public Vector4 Column2
        {
            get
            {
                return new Vector4(this.M12, this.M22, this.M32, this.M42);
            }
            set
            {
                this.M12 = value.X;
                this.M22 = value.Y;
                this.M32 = value.Z;
                this.M42 = value.W;
            }
        }

        public Vector4 Column3
        {
            get
            {
                return new Vector4(this.M13, this.M23, this.M33, this.M43);
            }
            set
            {
                this.M13 = value.X;
                this.M23 = value.Y;
                this.M33 = value.Z;
                this.M43 = value.W;
            }
        }

        public Vector4 Column4
        {
            get
            {
                return new Vector4(this.M14, this.M24, this.M34, this.M44);
            }
            set
            {
                this.M14 = value.X;
                this.M24 = value.Y;
                this.M34 = value.Z;
                this.M44 = value.W;
            }
        }

        public Matrix(
          float m11,
          float m12,
          float m13,
          float m14,
          float m21,
          float m22,
          float m23,
          float m24,
          float m31,
          float m32,
          float m33,
          float m34,
          float m41,
          float m42,
          float m43,
          float m44)
        {
            this.M11 = m11;
            this.M12 = m12;
            this.M13 = m13;
            this.M14 = m14;
            this.M21 = m21;
            this.M22 = m22;
            this.M23 = m23;
            this.M24 = m24;
            this.M31 = m31;
            this.M32 = m32;
            this.M33 = m33;
            this.M34 = m34;
            this.M41 = m41;
            this.M42 = m42;
            this.M43 = m43;
            this.M44 = m44;
        }

        public Matrix(float[] values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            if (values.Length != 16)
                throw new ArgumentOutOfRangeException(nameof(values), "There must be sixteen and only sixteen input values for Matrix.");
            this.M11 = values[0];
            this.M12 = values[1];
            this.M13 = values[2];
            this.M14 = values[3];
            this.M21 = values[4];
            this.M22 = values[5];
            this.M23 = values[6];
            this.M24 = values[7];
            this.M31 = values[8];
            this.M32 = values[9];
            this.M33 = values[10];
            this.M34 = values[11];
            this.M41 = values[12];
            this.M42 = values[13];
            this.M43 = values[14];
            this.M44 = values[15];
        }

        public static Matrix CreateBillboard(
          Vector3 objectPosition,
          Vector3 cameraPosition,
          Vector3 cameraUpVector,
          Vector3? cameraForwardVector)
        {
            Matrix result;
            Matrix.CreateBillboard(ref objectPosition, ref cameraPosition, ref cameraUpVector, cameraForwardVector, out result);
            return result;
        }

        public static void CreateBillboard(
          ref Vector3 objectPosition,
          ref Vector3 cameraPosition,
          ref Vector3 cameraUpVector,
          Vector3? cameraForwardVector,
          out Matrix result)
        {
            Vector3 result1;
            result1.X = objectPosition.X - cameraPosition.X;
            result1.Y = objectPosition.Y - cameraPosition.Y;
            result1.Z = objectPosition.Z - cameraPosition.Z;
            float num1 = result1.LengthSquared();
            if ((double)num1 < 9.99999974737875E-05)
                result1 = cameraForwardVector.HasValue ? -cameraForwardVector.Value : Vector3.Forward;
            else
                Vector3.Multiply(ref result1, 1f / (float)System.Math.Sqrt((double)num1), out result1);
            Vector3 result2;
            Vector3.Cross(ref cameraUpVector, ref result1, out result2);
            double num2 = (double)result2.Normalize();
            Vector3 result3;
            Vector3.Cross(ref result1, ref result2, out result3);
            result.M11 = result2.X;
            result.M12 = result2.Y;
            result.M13 = result2.Z;
            result.M14 = 0.0f;
            result.M21 = result3.X;
            result.M22 = result3.Y;
            result.M23 = result3.Z;
            result.M24 = 0.0f;
            result.M31 = result1.X;
            result.M32 = result1.Y;
            result.M33 = result1.Z;
            result.M34 = 0.0f;
            result.M41 = objectPosition.X;
            result.M42 = objectPosition.Y;
            result.M43 = objectPosition.Z;
            result.M44 = 1f;
        }

        public static Matrix CreateConstrainedBillboard(
          Vector3 objectPosition,
          Vector3 cameraPosition,
          Vector3 rotateAxis,
          Vector3? cameraForwardVector,
          Vector3? objectForwardVector)
        {
            Matrix result;
            Matrix.CreateConstrainedBillboard(ref objectPosition, ref cameraPosition, ref rotateAxis, cameraForwardVector, objectForwardVector, out result);
            return result;
        }

        public static void CreateConstrainedBillboard(
          ref Vector3 objectPosition,
          ref Vector3 cameraPosition,
          ref Vector3 rotateAxis,
          Vector3? cameraForwardVector,
          Vector3? objectForwardVector,
          out Matrix result)
        {
            Vector3 result1;
            result1.X = objectPosition.X - cameraPosition.X;
            result1.Y = objectPosition.Y - cameraPosition.Y;
            result1.Z = objectPosition.Z - cameraPosition.Z;
            float num1 = result1.LengthSquared();
            if ((double)num1 < 9.99999974737875E-05)
                result1 = cameraForwardVector.HasValue ? -cameraForwardVector.Value : Vector3.Forward;
            else
                Vector3.Multiply(ref result1, 1f / (float)System.Math.Sqrt((double)num1), out result1);
            Vector3 vector2 = rotateAxis;
            float result2;
            Vector3.Dot(ref rotateAxis, ref result1, out result2);
            Vector3 result3;
            Vector3 result4;
            if ((double)System.Math.Abs(result2) > 0.998254716396332)
            {
                if (objectForwardVector.HasValue)
                {
                    result3 = objectForwardVector.Value;
                    Vector3.Dot(ref rotateAxis, ref result3, out result2);
                    if ((double)System.Math.Abs(result2) > 0.998254716396332)
                        result3 = (double)System.Math.Abs((float)((double)rotateAxis.X * (double)Vector3.Forward.X + (double)rotateAxis.Y * (double)Vector3.Forward.Y + (double)rotateAxis.Z * (double)Vector3.Forward.Z)) > 0.998254716396332 ? Vector3.Right : Vector3.Forward;
                }
                else
                    result3 = (double)System.Math.Abs((float)((double)rotateAxis.X * (double)Vector3.Forward.X + (double)rotateAxis.Y * (double)Vector3.Forward.Y + (double)rotateAxis.Z * (double)Vector3.Forward.Z)) > 0.998254716396332 ? Vector3.Right : Vector3.Forward;
                Vector3.Cross(ref rotateAxis, ref result3, out result4);
                double num2 = (double)result4.Normalize();
                Vector3.Cross(ref result4, ref rotateAxis, out result3);
                double num3 = (double)result3.Normalize();
            }
            else
            {
                Vector3.Cross(ref rotateAxis, ref result1, out result4);
                double num2 = (double)result4.Normalize();
                Vector3.Cross(ref result4, ref vector2, out result3);
                double num3 = (double)result3.Normalize();
            }
            result.M11 = result4.X;
            result.M12 = result4.Y;
            result.M13 = result4.Z;
            result.M14 = 0.0f;
            result.M21 = vector2.X;
            result.M22 = vector2.Y;
            result.M23 = vector2.Z;
            result.M24 = 0.0f;
            result.M31 = result3.X;
            result.M32 = result3.Y;
            result.M33 = result3.Z;
            result.M34 = 0.0f;
            result.M41 = objectPosition.X;
            result.M42 = objectPosition.Y;
            result.M43 = objectPosition.Z;
            result.M44 = 1f;
        }

        public static Matrix CreateTranslation(Vector3 position)
        {
            Matrix result;
            Matrix.CreateTranslation(ref position, out result);
            return result;
        }

        public static void CreateTranslation(ref Vector3 position, out Matrix result)
        {
            result.M11 = 1f;
            result.M12 = 0.0f;
            result.M13 = 0.0f;
            result.M14 = 0.0f;
            result.M21 = 0.0f;
            result.M22 = 1f;
            result.M23 = 0.0f;
            result.M24 = 0.0f;
            result.M31 = 0.0f;
            result.M32 = 0.0f;
            result.M33 = 1f;
            result.M34 = 0.0f;
            result.M41 = position.X;
            result.M42 = position.Y;
            result.M43 = position.Z;
            result.M44 = 1f;
        }

        public static Matrix CreateTranslation(float xPosition, float yPosition, float zPosition)
        {
            Matrix result;
            Matrix.CreateTranslation(xPosition, yPosition, zPosition, out result);
            return result;
        }

        public static void CreateTranslation(
          float xPosition,
          float yPosition,
          float zPosition,
          out Matrix result)
        {
            result.M11 = 1f;
            result.M12 = 0.0f;
            result.M13 = 0.0f;
            result.M14 = 0.0f;
            result.M21 = 0.0f;
            result.M22 = 1f;
            result.M23 = 0.0f;
            result.M24 = 0.0f;
            result.M31 = 0.0f;
            result.M32 = 0.0f;
            result.M33 = 1f;
            result.M34 = 0.0f;
            result.M41 = xPosition;
            result.M42 = yPosition;
            result.M43 = zPosition;
            result.M44 = 1f;
        }

        public static Matrix CreateScale(float xScale, float yScale, float zScale)
        {
            Matrix result;
            Matrix.CreateScale(xScale, yScale, zScale, out result);
            return result;
        }

        public static void CreateScale(float xScale, float yScale, float zScale, out Matrix result)
        {
            result.M11 = xScale;
            result.M12 = 0.0f;
            result.M13 = 0.0f;
            result.M14 = 0.0f;
            result.M21 = 0.0f;
            result.M22 = yScale;
            result.M23 = 0.0f;
            result.M24 = 0.0f;
            result.M31 = 0.0f;
            result.M32 = 0.0f;
            result.M33 = zScale;
            result.M34 = 0.0f;
            result.M41 = 0.0f;
            result.M42 = 0.0f;
            result.M43 = 0.0f;
            result.M44 = 1f;
        }

        public static Matrix CreateScale(Vector3 scales)
        {
            Matrix result;
            Matrix.CreateScale(ref scales, out result);
            return result;
        }

        public static void CreateScale(ref Vector3 scales, out Matrix result)
        {
            result.M11 = scales.X;
            result.M12 = 0.0f;
            result.M13 = 0.0f;
            result.M14 = 0.0f;
            result.M21 = 0.0f;
            result.M22 = scales.Y;
            result.M23 = 0.0f;
            result.M24 = 0.0f;
            result.M31 = 0.0f;
            result.M32 = 0.0f;
            result.M33 = scales.Z;
            result.M34 = 0.0f;
            result.M41 = 0.0f;
            result.M42 = 0.0f;
            result.M43 = 0.0f;
            result.M44 = 1f;
        }

        public static Matrix CreateScale(float scale)
        {
            Matrix result;
            Matrix.CreateScale(scale, out result);
            return result;
        }

        public static void CreateScale(float scale, out Matrix result)
        {
            result.M11 = scale;
            result.M12 = 0.0f;
            result.M13 = 0.0f;
            result.M14 = 0.0f;
            result.M21 = 0.0f;
            result.M22 = scale;
            result.M23 = 0.0f;
            result.M24 = 0.0f;
            result.M31 = 0.0f;
            result.M32 = 0.0f;
            result.M33 = scale;
            result.M34 = 0.0f;
            result.M41 = 0.0f;
            result.M42 = 0.0f;
            result.M43 = 0.0f;
            result.M44 = 1f;
        }

        public static Matrix CreateRotationX(float radians)
        {
            Matrix result;
            Matrix.CreateRotationX(radians, out result);
            return result;
        }

        public static void CreateRotationX(float radians, out Matrix result)
        {
            float num1 = (float)System.Math.Cos((double)radians);
            float num2 = (float)System.Math.Sin((double)radians);
            result.M11 = 1f;
            result.M12 = 0.0f;
            result.M13 = 0.0f;
            result.M14 = 0.0f;
            result.M21 = 0.0f;
            result.M22 = num1;
            result.M23 = num2;
            result.M24 = 0.0f;
            result.M31 = 0.0f;
            result.M32 = -num2;
            result.M33 = num1;
            result.M34 = 0.0f;
            result.M41 = 0.0f;
            result.M42 = 0.0f;
            result.M43 = 0.0f;
            result.M44 = 1f;
        }

        public static Matrix CreateRotationY(float radians)
        {
            Matrix result;
            Matrix.CreateRotationY(radians, out result);
            return result;
        }

        public static void CreateRotationY(float radians, out Matrix result)
        {
            float num1 = (float)System.Math.Cos((double)radians);
            float num2 = (float)System.Math.Sin((double)radians);
            result.M11 = num1;
            result.M12 = 0.0f;
            result.M13 = -num2;
            result.M14 = 0.0f;
            result.M21 = 0.0f;
            result.M22 = 1f;
            result.M23 = 0.0f;
            result.M24 = 0.0f;
            result.M31 = num2;
            result.M32 = 0.0f;
            result.M33 = num1;
            result.M34 = 0.0f;
            result.M41 = 0.0f;
            result.M42 = 0.0f;
            result.M43 = 0.0f;
            result.M44 = 1f;
        }

        public static Matrix CreateRotationZ(float radians)
        {
            Matrix result;
            Matrix.CreateRotationZ(radians, out result);
            return result;
        }

        public static void CreateRotationZ(float radians, out Matrix result)
        {
            float num1 = (float)System.Math.Cos((double)radians);
            float num2 = (float)System.Math.Sin((double)radians);
            result.M11 = num1;
            result.M12 = num2;
            result.M13 = 0.0f;
            result.M14 = 0.0f;
            result.M21 = -num2;
            result.M22 = num1;
            result.M23 = 0.0f;
            result.M24 = 0.0f;
            result.M31 = 0.0f;
            result.M32 = 0.0f;
            result.M33 = 1f;
            result.M34 = 0.0f;
            result.M41 = 0.0f;
            result.M42 = 0.0f;
            result.M43 = 0.0f;
            result.M44 = 1f;
        }

        public static Matrix CreateFromTRS(Vector3 translate, Vector3 rotation, Vector3 scale)
        {
            Matrix matrix;
            Matrix.CreateFromTRS(ref translate, ref rotation, ref scale, out matrix);
            return matrix;
        }

        public static void CreateFromTRS(
          ref Vector3 translate,
          ref Vector3 rotation,
          ref Vector3 scale,
          out Matrix matrix)
        {
            Quaternion result;
            Quaternion.CreateFromYawPitchRoll(rotation.Y, rotation.X, rotation.Z, out result);
            Matrix.CreateFromTRS(ref translate, ref result, ref scale, out matrix);
        }

        public static Matrix CreateFromTRS(
          Vector3 translate,
          Quaternion orientation,
          Vector3 scale)
        {
            Matrix matrix;
            Matrix.CreateFromTRS(ref translate, ref orientation, ref scale, out matrix);
            return matrix;
        }

        public static void CreateFromTRS(
          ref Vector3 translate,
          ref Quaternion orientation,
          ref Vector3 scale,
          out Matrix matrix)
        {
            float num1 = orientation.X * orientation.X;
            float num2 = orientation.Y * orientation.Y;
            float num3 = orientation.Z * orientation.Z;
            float num4 = orientation.X * orientation.Y;
            float num5 = orientation.Z * orientation.W;
            float num6 = orientation.Z * orientation.X;
            float num7 = orientation.Y * orientation.W;
            float num8 = orientation.Y * orientation.Z;
            float num9 = orientation.X * orientation.W;
            matrix.M11 = scale.X * (float)(1.0 - 2.0 * ((double)num2 + (double)num3));
            matrix.M12 = scale.X * (float)(2.0 * ((double)num4 + (double)num5));
            matrix.M13 = scale.X * (float)(2.0 * ((double)num6 - (double)num7));
            matrix.M14 = 0.0f;
            matrix.M21 = scale.Y * (float)(2.0 * ((double)num4 - (double)num5));
            matrix.M22 = scale.Y * (float)(1.0 - 2.0 * ((double)num3 + (double)num1));
            matrix.M23 = scale.Y * (float)(2.0 * ((double)num8 + (double)num9));
            matrix.M24 = 0.0f;
            matrix.M31 = scale.Z * (float)(2.0 * ((double)num6 + (double)num7));
            matrix.M32 = scale.Z * (float)(2.0 * ((double)num8 - (double)num9));
            matrix.M33 = scale.Z * (float)(1.0 - 2.0 * ((double)num2 + (double)num1));
            matrix.M34 = 0.0f;
            matrix.M41 = translate.X;
            matrix.M42 = translate.Y;
            matrix.M43 = translate.Z;
            matrix.M44 = 1f;
        }

        public static Matrix CreateFromAxisAngle(Vector3 axis, float angle)
        {
            Matrix result;
            Matrix.CreateFromAxisAngle(ref axis, angle, out result);
            return result;
        }

        public static void CreateFromAxisAngle(ref Vector3 axis, float angle, out Matrix result)
        {
            float x = axis.X;
            float y = axis.Y;
            float z = axis.Z;
            float num1 = (float)System.Math.Sin((double)angle);
            float num2 = (float)System.Math.Cos((double)angle);
            float num3 = x * x;
            float num4 = y * y;
            float num5 = z * z;
            float num6 = x * y;
            float num7 = x * z;
            float num8 = y * z;
            result.M11 = num3 + num2 * (1f - num3);
            result.M12 = (float)((double)num6 - (double)num2 * (double)num6 + (double)num1 * (double)z);
            result.M13 = (float)((double)num7 - (double)num2 * (double)num7 - (double)num1 * (double)y);
            result.M14 = 0.0f;
            result.M21 = (float)((double)num6 - (double)num2 * (double)num6 - (double)num1 * (double)z);
            result.M22 = num4 + num2 * (1f - num4);
            result.M23 = (float)((double)num8 - (double)num2 * (double)num8 + (double)num1 * (double)x);
            result.M24 = 0.0f;
            result.M31 = (float)((double)num7 - (double)num2 * (double)num7 + (double)num1 * (double)y);
            result.M32 = (float)((double)num8 - (double)num2 * (double)num8 - (double)num1 * (double)x);
            result.M33 = num5 + num2 * (1f - num5);
            result.M34 = 0.0f;
            result.M41 = 0.0f;
            result.M42 = 0.0f;
            result.M43 = 0.0f;
            result.M44 = 1f;
        }

        public static Matrix CreatePerspectiveFieldOfView(
          float fieldOfView,
          float aspectRatio,
          float nearPlaneDistance,
          float farPlaneDistance)
        {
            Matrix result;
            Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearPlaneDistance, farPlaneDistance, out result);
            return result;
        }

        public static void CreatePerspectiveFieldOfView(
          float fieldOfView,
          float aspectRatio,
          float nearPlaneDistance,
          float farPlaneDistance,
          out Matrix result)
        {
            if ((double)fieldOfView <= 0.0 || (double)fieldOfView >= 3.14159297943115)
                throw new ArgumentOutOfRangeException(nameof(fieldOfView), string.Format((IFormatProvider)CultureInfo.CurrentCulture, "FrameworkResources.OutRangeFieldOfView", (object)nameof(fieldOfView)));
            if ((double)nearPlaneDistance <= 0.0)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance), string.Format((IFormatProvider)CultureInfo.CurrentCulture, "FrameworkResources.NegativePlaneDistance", (object)nameof(nearPlaneDistance)));
            if ((double)farPlaneDistance <= 0.0)
                throw new ArgumentOutOfRangeException(nameof(farPlaneDistance), string.Format((IFormatProvider)CultureInfo.CurrentCulture, "FrameworkResources.NegativePlaneDistance", (object)nameof(farPlaneDistance)));
            if ((double)nearPlaneDistance >= (double)farPlaneDistance)
                farPlaneDistance = nearPlaneDistance + 0.1f;
            float num1 = 1f / (float)System.Math.Tan((double)fieldOfView * 0.5);
            float num2 = num1 / aspectRatio;
            result.M11 = num2;
            result.M12 = result.M13 = result.M14 = 0.0f;
            result.M22 = num1;
            result.M21 = result.M23 = result.M24 = 0.0f;
            result.M31 = result.M32 = 0.0f;
            result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
            result.M34 = -1f;
            result.M41 = result.M42 = result.M44 = 0.0f;
            result.M43 = (float)((double)nearPlaneDistance * (double)farPlaneDistance / ((double)nearPlaneDistance - (double)farPlaneDistance));
        }

        public static Matrix CreatePerspective(
          float width,
          float height,
          float nearPlaneDistance,
          float farPlaneDistance)
        {
            Matrix result;
            Matrix.CreatePerspective(width, height, nearPlaneDistance, farPlaneDistance, out result);
            return result;
        }

        public static void CreatePerspective(
          float width,
          float height,
          float nearPlaneDistance,
          float farPlaneDistance,
          out Matrix result)
        {
            if ((double)nearPlaneDistance <= 0.0)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance), string.Format((IFormatProvider)CultureInfo.CurrentCulture, "FrameworkResources.NegativePlaneDistance", (object)nameof(nearPlaneDistance)));
            if ((double)farPlaneDistance <= 0.0)
                throw new ArgumentOutOfRangeException(nameof(farPlaneDistance), string.Format((IFormatProvider)CultureInfo.CurrentCulture, "FrameworkResources.NegativePlaneDistance", (object)nameof(farPlaneDistance)));
            if ((double)nearPlaneDistance >= (double)farPlaneDistance)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance), "FrameworkResources.OppositePlanes");
            result.M11 = 2f * nearPlaneDistance / width;
            result.M12 = result.M13 = result.M14 = 0.0f;
            result.M22 = 2f * nearPlaneDistance / height;
            result.M21 = result.M23 = result.M24 = 0.0f;
            result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
            result.M31 = result.M32 = 0.0f;
            result.M34 = -1f;
            result.M41 = result.M42 = result.M44 = 0.0f;
            result.M43 = (float)((double)nearPlaneDistance * (double)farPlaneDistance / ((double)nearPlaneDistance - (double)farPlaneDistance));
        }

        public static Matrix CreatePerspectiveOffCenter(
          float left,
          float right,
          float bottom,
          float top,
          float nearPlaneDistance,
          float farPlaneDistance)
        {
            Matrix result;
            Matrix.CreatePerspectiveOffCenter(left, right, bottom, top, nearPlaneDistance, farPlaneDistance, out result);
            return result;
        }

        public static void CreatePerspectiveOffCenter(
          float left,
          float right,
          float bottom,
          float top,
          float nearPlaneDistance,
          float farPlaneDistance,
          out Matrix result)
        {
            if ((double)nearPlaneDistance <= 0.0)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance), string.Format((IFormatProvider)CultureInfo.CurrentCulture, "FrameworkResources.NegativePlaneDistance", (object)nameof(nearPlaneDistance)));
            if ((double)farPlaneDistance <= 0.0)
                throw new ArgumentOutOfRangeException(nameof(farPlaneDistance), string.Format((IFormatProvider)CultureInfo.CurrentCulture, "FrameworkResources.NegativePlaneDistance", (object)nameof(farPlaneDistance)));
            if ((double)nearPlaneDistance >= (double)farPlaneDistance)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance), "FrameworkResources.OppositePlanes");
            result.M11 = (float)(2.0 * (double)nearPlaneDistance / ((double)right - (double)left));
            result.M12 = result.M13 = result.M14 = 0.0f;
            result.M22 = (float)(2.0 * (double)nearPlaneDistance / ((double)top - (double)bottom));
            result.M21 = result.M23 = result.M24 = 0.0f;
            result.M31 = (float)(((double)left + (double)right) / ((double)right - (double)left));
            result.M32 = (float)(((double)top + (double)bottom) / ((double)top - (double)bottom));
            result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
            result.M34 = -1f;
            result.M43 = (float)((double)nearPlaneDistance * (double)farPlaneDistance / ((double)nearPlaneDistance - (double)farPlaneDistance));
            result.M41 = result.M42 = result.M44 = 0.0f;
        }

        public static Matrix CreateOrthographic(
          float width,
          float height,
          float zNearPlane,
          float zFarPlane)
        {
            Matrix result;
            Matrix.CreateOrthographic(width, height, zNearPlane, zFarPlane, out result);
            return result;
        }

        public static void CreateOrthographic(
          float width,
          float height,
          float zNearPlane,
          float zFarPlane,
          out Matrix result)
        {
            result.M11 = 2f / width;
            result.M12 = result.M13 = result.M14 = 0.0f;
            result.M22 = 2f / height;
            result.M21 = result.M23 = result.M24 = 0.0f;
            result.M33 = (float)(1.0 / ((double)zNearPlane - (double)zFarPlane));
            result.M31 = result.M32 = result.M34 = 0.0f;
            result.M41 = result.M42 = 0.0f;
            result.M43 = zNearPlane / (zNearPlane - zFarPlane);
            result.M44 = 1f;
        }

        public static Matrix CreateOrthographicOffCenter(
          float left,
          float right,
          float bottom,
          float top,
          float zNearPlane,
          float zFarPlane)
        {
            Matrix result;
            Matrix.CreateOrthographicOffCenter(left, right, bottom, top, zNearPlane, zFarPlane, out result);
            return result;
        }

        public static void CreateOrthographicOffCenter(
          float left,
          float right,
          float bottom,
          float top,
          float zNearPlane,
          float zFarPlane,
          out Matrix result)
        {
            result.M11 = (float)(2.0 / ((double)right - (double)left));
            result.M12 = result.M13 = result.M14 = 0.0f;
            result.M22 = (float)(2.0 / ((double)top - (double)bottom));
            result.M21 = result.M23 = result.M24 = 0.0f;
            result.M33 = (float)(1.0 / ((double)zNearPlane - (double)zFarPlane));
            result.M31 = result.M32 = result.M34 = 0.0f;
            result.M41 = (float)(((double)left + (double)right) / ((double)left - (double)right));
            result.M42 = (float)(((double)top + (double)bottom) / ((double)bottom - (double)top));
            result.M43 = zNearPlane / (zNearPlane - zFarPlane);
            result.M44 = 1f;
        }

        public static Matrix CreateLookAt(Vector3 position, Vector3 target, Vector3 upVector)
        {
            Matrix result;
            Matrix.CreateLookAt(ref position, ref target, ref upVector, out result);
            return result;
        }

        public static void CreateLookAt(
          ref Vector3 position,
          ref Vector3 target,
          ref Vector3 upVector,
          out Matrix result)
        {
            Vector3 vector3_1 = Vector3.Normalize(position - target);
            Vector3 vector3_2 = Vector3.Normalize(Vector3.Cross(upVector, vector3_1));
            Vector3 vector1 = Vector3.Cross(vector3_1, vector3_2);
            result.M11 = vector3_2.X;
            result.M12 = vector1.X;
            result.M13 = vector3_1.X;
            result.M14 = 0.0f;
            result.M21 = vector3_2.Y;
            result.M22 = vector1.Y;
            result.M23 = vector3_1.Y;
            result.M24 = 0.0f;
            result.M31 = vector3_2.Z;
            result.M32 = vector1.Z;
            result.M33 = vector3_1.Z;
            result.M34 = 0.0f;
            result.M41 = -Vector3.Dot(vector3_2, position);
            result.M42 = -Vector3.Dot(vector1, position);
            result.M43 = -Vector3.Dot(vector3_1, position);
            result.M44 = 1f;
        }

        public static Matrix CreateWorld(Vector3 position, Vector3 forward, Vector3 up)
        {
            Matrix result;
            Matrix.CreateWorld(ref position, ref forward, ref up, out result);
            return result;
        }

        public static void CreateWorld(
          ref Vector3 position,
          ref Vector3 forward,
          ref Vector3 up,
          out Matrix result)
        {
            Vector3 vector3_1 = Vector3.Normalize(-forward);
            Vector3 vector2 = Vector3.Normalize(Vector3.Cross(up, vector3_1));
            Vector3 vector3_2 = Vector3.Cross(vector3_1, vector2);
            result.M11 = vector2.X;
            result.M12 = vector2.Y;
            result.M13 = vector2.Z;
            result.M14 = 0.0f;
            result.M21 = vector3_2.X;
            result.M22 = vector3_2.Y;
            result.M23 = vector3_2.Z;
            result.M24 = 0.0f;
            result.M31 = vector3_1.X;
            result.M32 = vector3_1.Y;
            result.M33 = vector3_1.Z;
            result.M34 = 0.0f;
            result.M41 = position.X;
            result.M42 = position.Y;
            result.M43 = position.Z;
            result.M44 = 1f;
        }

        public static Matrix CreateFromQuaternion(Quaternion quaternion)
        {
            Matrix result;
            Matrix.CreateFromQuaternion(ref quaternion, out result);
            return result;
        }

        public static void CreateFromQuaternion(ref Quaternion quaternion, out Matrix result)
        {
            float num1 = quaternion.X * quaternion.X;
            float num2 = quaternion.Y * quaternion.Y;
            float num3 = quaternion.Z * quaternion.Z;
            float num4 = quaternion.X * quaternion.Y;
            float num5 = quaternion.Z * quaternion.W;
            float num6 = quaternion.Z * quaternion.X;
            float num7 = quaternion.Y * quaternion.W;
            float num8 = quaternion.Y * quaternion.Z;
            float num9 = quaternion.X * quaternion.W;
            result.M11 = (float)(1.0 - 2.0 * ((double)num2 + (double)num3));
            result.M12 = (float)(2.0 * ((double)num4 + (double)num5));
            result.M13 = (float)(2.0 * ((double)num6 - (double)num7));
            result.M14 = 0.0f;
            result.M21 = (float)(2.0 * ((double)num4 - (double)num5));
            result.M22 = (float)(1.0 - 2.0 * ((double)num3 + (double)num1));
            result.M23 = (float)(2.0 * ((double)num8 + (double)num9));
            result.M24 = 0.0f;
            result.M31 = (float)(2.0 * ((double)num6 + (double)num7));
            result.M32 = (float)(2.0 * ((double)num8 - (double)num9));
            result.M33 = (float)(1.0 - 2.0 * ((double)num2 + (double)num1));
            result.M34 = 0.0f;
            result.M41 = 0.0f;
            result.M42 = 0.0f;
            result.M43 = 0.0f;
            result.M44 = 1f;
        }

        public static Matrix CreateFromYawPitchRoll(float yaw, float pitch, float roll)
        {
            Matrix result;
            Matrix.CreateFromYawPitchRoll(yaw, pitch, roll, out result);
            return result;
        }

        public static void CreateFromYawPitchRoll(
          float yaw,
          float pitch,
          float roll,
          out Matrix result)
        {
            Quaternion result1;
            Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll, out result1);
            Matrix.CreateFromQuaternion(ref result1, out result);
        }

        public static Matrix Transform(Matrix value, Quaternion rotation)
        {
            Matrix result;
            Matrix.Transform(ref value, ref rotation, out result);
            return result;
        }

        public static void Transform(ref Matrix value, ref Quaternion rotation, out Matrix result)
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
            float num13 = 1f - num10 - num12;
            float num14 = num8 - num6;
            float num15 = num9 + num5;
            float num16 = num8 + num6;
            float num17 = 1f - num7 - num12;
            float num18 = num11 - num4;
            float num19 = num9 - num5;
            float num20 = num11 + num4;
            float num21 = 1f - num7 - num10;
            float num22 = (float)((double)value.M11 * (double)num13 + (double)value.M12 * (double)num14 + (double)value.M13 * (double)num15);
            float num23 = (float)((double)value.M11 * (double)num16 + (double)value.M12 * (double)num17 + (double)value.M13 * (double)num18);
            float num24 = (float)((double)value.M11 * (double)num19 + (double)value.M12 * (double)num20 + (double)value.M13 * (double)num21);
            float m14 = value.M14;
            float num25 = (float)((double)value.M21 * (double)num13 + (double)value.M22 * (double)num14 + (double)value.M23 * (double)num15);
            float num26 = (float)((double)value.M21 * (double)num16 + (double)value.M22 * (double)num17 + (double)value.M23 * (double)num18);
            float num27 = (float)((double)value.M21 * (double)num19 + (double)value.M22 * (double)num20 + (double)value.M23 * (double)num21);
            float m24 = value.M24;
            float num28 = (float)((double)value.M31 * (double)num13 + (double)value.M32 * (double)num14 + (double)value.M33 * (double)num15);
            float num29 = (float)((double)value.M31 * (double)num16 + (double)value.M32 * (double)num17 + (double)value.M33 * (double)num18);
            float num30 = (float)((double)value.M31 * (double)num19 + (double)value.M32 * (double)num20 + (double)value.M33 * (double)num21);
            float m34 = value.M34;
            float num31 = (float)((double)value.M41 * (double)num13 + (double)value.M42 * (double)num14 + (double)value.M43 * (double)num15);
            float num32 = (float)((double)value.M41 * (double)num16 + (double)value.M42 * (double)num17 + (double)value.M43 * (double)num18);
            float num33 = (float)((double)value.M41 * (double)num19 + (double)value.M42 * (double)num20 + (double)value.M43 * (double)num21);
            float m44 = value.M44;
            result.M11 = num22;
            result.M12 = num23;
            result.M13 = num24;
            result.M14 = m14;
            result.M21 = num25;
            result.M22 = num26;
            result.M23 = num27;
            result.M24 = m24;
            result.M31 = num28;
            result.M32 = num29;
            result.M33 = num30;
            result.M34 = m34;
            result.M41 = num31;
            result.M42 = num32;
            result.M43 = num33;
            result.M44 = m44;
        }

        public override string ToString()
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            return "{ " + string.Format((IFormatProvider)currentCulture, "{{M11:{0} M12:{1} M13:{2} M14:{3}}} ", (object)this.M11.ToString((IFormatProvider)currentCulture), (object)this.M12.ToString((IFormatProvider)currentCulture), (object)this.M13.ToString((IFormatProvider)currentCulture), (object)this.M14.ToString((IFormatProvider)currentCulture)) + string.Format((IFormatProvider)currentCulture, "{{M21:{0} M22:{1} M23:{2} M24:{3}}} ", (object)this.M21.ToString((IFormatProvider)currentCulture), (object)this.M22.ToString((IFormatProvider)currentCulture), (object)this.M23.ToString((IFormatProvider)currentCulture), (object)this.M24.ToString((IFormatProvider)currentCulture)) + string.Format((IFormatProvider)currentCulture, "{{M31:{0} M32:{1} M33:{2} M34:{3}}} ", (object)this.M31.ToString((IFormatProvider)currentCulture), (object)this.M32.ToString((IFormatProvider)currentCulture), (object)this.M33.ToString((IFormatProvider)currentCulture), (object)this.M34.ToString((IFormatProvider)currentCulture)) + string.Format((IFormatProvider)currentCulture, "{{M41:{0} M42:{1} M43:{2} M44:{3}}} ", (object)this.M41.ToString((IFormatProvider)currentCulture), (object)this.M42.ToString((IFormatProvider)currentCulture), (object)this.M43.ToString((IFormatProvider)currentCulture), (object)this.M44.ToString((IFormatProvider)currentCulture)) + "}";
        }

        public bool Equals(Matrix other)
        {
            return this.Equals(ref other);
        }

        public bool Equals(ref Matrix other)
        {
            if (this.M11.Equal(other.M11, 0.0001f) && this.M22.Equal(other.M22, 0.0001f) && (this.M33.Equal(other.M33, 0.0001f) && this.M44.Equal(other.M44, 0.0001f)) && (this.M12.Equal(other.M12, 0.0001f) && this.M13.Equal(other.M13, 0.0001f) && (this.M14.Equal(other.M14, 0.0001f) && this.M21.Equal(other.M21, 0.0001f))) && (this.M23.Equal(other.M23, 0.0001f) && this.M24.Equal(other.M24, 0.0001f) && (this.M31.Equal(other.M31, 0.0001f) && this.M32.Equal(other.M32, 0.0001f)) && (this.M34.Equal(other.M34, 0.0001f) && this.M41.Equal(other.M41, 0.0001f) && this.M42.Equal(other.M42, 0.0001f))))
                return this.M43.Equal(other.M43, 0.0001f);
            return false;
        }

        public override bool Equals(object obj)
        {
            bool flag = false;
            if (obj is Matrix)
                flag = this.Equals((Matrix)obj);
            return flag;
        }

        public override int GetHashCode()
        {
            return this.M11.GetHashCode() + this.M12.GetHashCode() + this.M13.GetHashCode() + this.M14.GetHashCode() + this.M21.GetHashCode() + this.M22.GetHashCode() + this.M23.GetHashCode() + this.M24.GetHashCode() + this.M31.GetHashCode() + this.M32.GetHashCode() + this.M33.GetHashCode() + this.M34.GetHashCode() + this.M41.GetHashCode() + this.M42.GetHashCode() + this.M43.GetHashCode() + this.M44.GetHashCode();
        }

        public static Matrix Transpose(Matrix matrix)
        {
            Matrix result;
            Matrix.Transpose(ref matrix, out result);
            return result;
        }

        public static void Transpose(ref Matrix matrix, out Matrix result)
        {
            float m11 = matrix.M11;
            float m12 = matrix.M12;
            float m13 = matrix.M13;
            float m14 = matrix.M14;
            float m21 = matrix.M21;
            float m22 = matrix.M22;
            float m23 = matrix.M23;
            float m24 = matrix.M24;
            float m31 = matrix.M31;
            float m32 = matrix.M32;
            float m33 = matrix.M33;
            float m34 = matrix.M34;
            float m41 = matrix.M41;
            float m42 = matrix.M42;
            float m43 = matrix.M43;
            float m44 = matrix.M44;
            result.M11 = m11;
            result.M12 = m21;
            result.M13 = m31;
            result.M14 = m41;
            result.M21 = m12;
            result.M22 = m22;
            result.M23 = m32;
            result.M24 = m42;
            result.M31 = m13;
            result.M32 = m23;
            result.M33 = m33;
            result.M34 = m43;
            result.M41 = m14;
            result.M42 = m24;
            result.M43 = m34;
            result.M44 = m44;
        }

        public float Determinant()
        {
            float m11 = this.M11;
            float m12 = this.M12;
            float m13 = this.M13;
            float m14 = this.M14;
            float m21 = this.M21;
            float m22 = this.M22;
            float m23 = this.M23;
            float m24 = this.M24;
            float m31 = this.M31;
            float m32 = this.M32;
            float m33 = this.M33;
            float m34 = this.M34;
            float m41 = this.M41;
            float m42 = this.M42;
            float m43 = this.M43;
            float m44 = this.M44;
            float num1 = (float)((double)m33 * (double)m44 - (double)m34 * (double)m43);
            float num2 = (float)((double)m32 * (double)m44 - (double)m34 * (double)m42);
            float num3 = (float)((double)m32 * (double)m43 - (double)m33 * (double)m42);
            float num4 = (float)((double)m31 * (double)m44 - (double)m34 * (double)m41);
            float num5 = (float)((double)m31 * (double)m43 - (double)m33 * (double)m41);
            float num6 = (float)((double)m31 * (double)m42 - (double)m32 * (double)m41);
            return (float)((double)m11 * ((double)m22 * (double)num1 - (double)m23 * (double)num2 + (double)m24 * (double)num3) - (double)m12 * ((double)m21 * (double)num1 - (double)m23 * (double)num4 + (double)m24 * (double)num5) + (double)m13 * ((double)m21 * (double)num2 - (double)m22 * (double)num4 + (double)m24 * (double)num6) - (double)m14 * ((double)m21 * (double)num3 - (double)m22 * (double)num5 + (double)m23 * (double)num6));
        }

        public void Invert()
        {
            Matrix.Invert(ref this, out this);
        }

        public static Matrix Invert(Matrix matrix)
        {
            Matrix result;
            Matrix.Invert(ref matrix, out result);
            return result;
        }

        public static void Invert(ref Matrix matrix, out Matrix result)
        {
            float m11 = matrix.M11;
            float m12 = matrix.M12;
            float m13 = matrix.M13;
            float m14 = matrix.M14;
            float m21 = matrix.M21;
            float m22 = matrix.M22;
            float m23 = matrix.M23;
            float m24 = matrix.M24;
            float m31 = matrix.M31;
            float m32 = matrix.M32;
            float m33 = matrix.M33;
            float m34 = matrix.M34;
            float m41 = matrix.M41;
            float m42 = matrix.M42;
            float m43 = matrix.M43;
            float m44 = matrix.M44;
            float num1 = (float)((double)m33 * (double)m44 - (double)m34 * (double)m43);
            float num2 = (float)((double)m32 * (double)m44 - (double)m34 * (double)m42);
            float num3 = (float)((double)m32 * (double)m43 - (double)m33 * (double)m42);
            float num4 = (float)((double)m31 * (double)m44 - (double)m34 * (double)m41);
            float num5 = (float)((double)m31 * (double)m43 - (double)m33 * (double)m41);
            float num6 = (float)((double)m31 * (double)m42 - (double)m32 * (double)m41);
            float num7 = (float)((double)m22 * (double)num1 - (double)m23 * (double)num2 + (double)m24 * (double)num3);
            float num8 = (float)-((double)m21 * (double)num1 - (double)m23 * (double)num4 + (double)m24 * (double)num5);
            float num9 = (float)((double)m21 * (double)num2 - (double)m22 * (double)num4 + (double)m24 * (double)num6);
            float num10 = (float)-((double)m21 * (double)num3 - (double)m22 * (double)num5 + (double)m23 * (double)num6);
            float num11 = (float)(1.0 / ((double)m11 * (double)num7 + (double)m12 * (double)num8 + (double)m13 * (double)num9 + (double)m14 * (double)num10));
            result.M11 = num7 * num11;
            result.M21 = num8 * num11;
            result.M31 = num9 * num11;
            result.M41 = num10 * num11;
            result.M12 = (float)-((double)m12 * (double)num1 - (double)m13 * (double)num2 + (double)m14 * (double)num3) * num11;
            result.M22 = (float)((double)m11 * (double)num1 - (double)m13 * (double)num4 + (double)m14 * (double)num5) * num11;
            result.M32 = (float)-((double)m11 * (double)num2 - (double)m12 * (double)num4 + (double)m14 * (double)num6) * num11;
            result.M42 = (float)((double)m11 * (double)num3 - (double)m12 * (double)num5 + (double)m13 * (double)num6) * num11;
            float num12 = (float)((double)m23 * (double)m44 - (double)m24 * (double)m43);
            float num13 = (float)((double)m22 * (double)m44 - (double)m24 * (double)m42);
            float num14 = (float)((double)m22 * (double)m43 - (double)m23 * (double)m42);
            float num15 = (float)((double)m21 * (double)m44 - (double)m24 * (double)m41);
            float num16 = (float)((double)m21 * (double)m43 - (double)m23 * (double)m41);
            float num17 = (float)((double)m21 * (double)m42 - (double)m22 * (double)m41);
            result.M13 = (float)((double)m12 * (double)num12 - (double)m13 * (double)num13 + (double)m14 * (double)num14) * num11;
            result.M23 = (float)-((double)m11 * (double)num12 - (double)m13 * (double)num15 + (double)m14 * (double)num16) * num11;
            result.M33 = (float)((double)m11 * (double)num13 - (double)m12 * (double)num15 + (double)m14 * (double)num17) * num11;
            result.M43 = (float)-((double)m11 * (double)num14 - (double)m12 * (double)num16 + (double)m13 * (double)num17) * num11;
            float num18 = (float)((double)m23 * (double)m34 - (double)m24 * (double)m33);
            float num19 = (float)((double)m22 * (double)m34 - (double)m24 * (double)m32);
            float num20 = (float)((double)m22 * (double)m33 - (double)m23 * (double)m32);
            float num21 = (float)((double)m21 * (double)m34 - (double)m24 * (double)m31);
            float num22 = (float)((double)m21 * (double)m33 - (double)m23 * (double)m31);
            float num23 = (float)((double)m21 * (double)m32 - (double)m22 * (double)m31);
            result.M14 = (float)-((double)m12 * (double)num18 - (double)m13 * (double)num19 + (double)m14 * (double)num20) * num11;
            result.M24 = (float)((double)m11 * (double)num18 - (double)m13 * (double)num21 + (double)m14 * (double)num22) * num11;
            result.M34 = (float)-((double)m11 * (double)num19 - (double)m12 * (double)num21 + (double)m14 * (double)num23) * num11;
            result.M44 = (float)((double)m11 * (double)num20 - (double)m12 * (double)num22 + (double)m13 * (double)num23) * num11;
        }

        public static Matrix Lerp(Matrix matrix1, Matrix matrix2, float amount)
        {
            Matrix result;
            Matrix.Lerp(ref matrix1, ref matrix2, amount, out result);
            return result;
        }

        public static void Lerp(
          ref Matrix matrix1,
          ref Matrix matrix2,
          float amount,
          out Matrix result)
        {
            result.M11 = matrix1.M11 + (matrix2.M11 - matrix1.M11) * amount;
            result.M12 = matrix1.M12 + (matrix2.M12 - matrix1.M12) * amount;
            result.M13 = matrix1.M13 + (matrix2.M13 - matrix1.M13) * amount;
            result.M14 = matrix1.M14 + (matrix2.M14 - matrix1.M14) * amount;
            result.M21 = matrix1.M21 + (matrix2.M21 - matrix1.M21) * amount;
            result.M22 = matrix1.M22 + (matrix2.M22 - matrix1.M22) * amount;
            result.M23 = matrix1.M23 + (matrix2.M23 - matrix1.M23) * amount;
            result.M24 = matrix1.M24 + (matrix2.M24 - matrix1.M24) * amount;
            result.M31 = matrix1.M31 + (matrix2.M31 - matrix1.M31) * amount;
            result.M32 = matrix1.M32 + (matrix2.M32 - matrix1.M32) * amount;
            result.M33 = matrix1.M33 + (matrix2.M33 - matrix1.M33) * amount;
            result.M34 = matrix1.M34 + (matrix2.M34 - matrix1.M34) * amount;
            result.M41 = matrix1.M41 + (matrix2.M41 - matrix1.M41) * amount;
            result.M42 = matrix1.M42 + (matrix2.M42 - matrix1.M42) * amount;
            result.M43 = matrix1.M43 + (matrix2.M43 - matrix1.M43) * amount;
            result.M44 = matrix1.M44 + (matrix2.M44 - matrix1.M44) * amount;
        }

        public static Matrix Negate(Matrix matrix)
        {
            Matrix result;
            Matrix.Negate(ref matrix, out result);
            return result;
        }

        public static void Negate(ref Matrix matrix, out Matrix result)
        {
            result.M11 = -matrix.M11;
            result.M12 = -matrix.M12;
            result.M13 = -matrix.M13;
            result.M14 = -matrix.M14;
            result.M21 = -matrix.M21;
            result.M22 = -matrix.M22;
            result.M23 = -matrix.M23;
            result.M24 = -matrix.M24;
            result.M31 = -matrix.M31;
            result.M32 = -matrix.M32;
            result.M33 = -matrix.M33;
            result.M34 = -matrix.M34;
            result.M41 = -matrix.M41;
            result.M42 = -matrix.M42;
            result.M43 = -matrix.M43;
            result.M44 = -matrix.M44;
        }

        public static Matrix Add(Matrix matrix1, Matrix matrix2)
        {
            Matrix result;
            Matrix.Add(ref matrix1, ref matrix2, out result);
            return result;
        }

        public static void Add(ref Matrix matrix1, ref Matrix matrix2, out Matrix result)
        {
            result.M11 = matrix1.M11 + matrix2.M11;
            result.M12 = matrix1.M12 + matrix2.M12;
            result.M13 = matrix1.M13 + matrix2.M13;
            result.M14 = matrix1.M14 + matrix2.M14;
            result.M21 = matrix1.M21 + matrix2.M21;
            result.M22 = matrix1.M22 + matrix2.M22;
            result.M23 = matrix1.M23 + matrix2.M23;
            result.M24 = matrix1.M24 + matrix2.M24;
            result.M31 = matrix1.M31 + matrix2.M31;
            result.M32 = matrix1.M32 + matrix2.M32;
            result.M33 = matrix1.M33 + matrix2.M33;
            result.M34 = matrix1.M34 + matrix2.M34;
            result.M41 = matrix1.M41 + matrix2.M41;
            result.M42 = matrix1.M42 + matrix2.M42;
            result.M43 = matrix1.M43 + matrix2.M43;
            result.M44 = matrix1.M44 + matrix2.M44;
        }

        public static Matrix Subtract(Matrix matrix1, Matrix matrix2)
        {
            Matrix result;
            Matrix.Subtract(ref matrix1, ref matrix2, out result);
            return result;
        }

        public static void Subtract(ref Matrix matrix1, ref Matrix matrix2, out Matrix result)
        {
            result.M11 = matrix1.M11 - matrix2.M11;
            result.M12 = matrix1.M12 - matrix2.M12;
            result.M13 = matrix1.M13 - matrix2.M13;
            result.M14 = matrix1.M14 - matrix2.M14;
            result.M21 = matrix1.M21 - matrix2.M21;
            result.M22 = matrix1.M22 - matrix2.M22;
            result.M23 = matrix1.M23 - matrix2.M23;
            result.M24 = matrix1.M24 - matrix2.M24;
            result.M31 = matrix1.M31 - matrix2.M31;
            result.M32 = matrix1.M32 - matrix2.M32;
            result.M33 = matrix1.M33 - matrix2.M33;
            result.M34 = matrix1.M34 - matrix2.M34;
            result.M41 = matrix1.M41 - matrix2.M41;
            result.M42 = matrix1.M42 - matrix2.M42;
            result.M43 = matrix1.M43 - matrix2.M43;
            result.M44 = matrix1.M44 - matrix2.M44;
        }

        public static Matrix Multiply(Matrix matrix1, Matrix matrix2)
        {
            Matrix result;
            Matrix.Multiply(ref matrix1, ref matrix2, out result);
            return result;
        }

        public static void Multiply(ref Matrix matrix1, ref Matrix matrix2, out Matrix result)
        {
            float num1 = (float)((double)matrix1.M11 * (double)matrix2.M11 + (double)matrix1.M12 * (double)matrix2.M21 + (double)matrix1.M13 * (double)matrix2.M31 + (double)matrix1.M14 * (double)matrix2.M41);
            float num2 = (float)((double)matrix1.M11 * (double)matrix2.M12 + (double)matrix1.M12 * (double)matrix2.M22 + (double)matrix1.M13 * (double)matrix2.M32 + (double)matrix1.M14 * (double)matrix2.M42);
            float num3 = (float)((double)matrix1.M11 * (double)matrix2.M13 + (double)matrix1.M12 * (double)matrix2.M23 + (double)matrix1.M13 * (double)matrix2.M33 + (double)matrix1.M14 * (double)matrix2.M43);
            float num4 = (float)((double)matrix1.M11 * (double)matrix2.M14 + (double)matrix1.M12 * (double)matrix2.M24 + (double)matrix1.M13 * (double)matrix2.M34 + (double)matrix1.M14 * (double)matrix2.M44);
            float num5 = (float)((double)matrix1.M21 * (double)matrix2.M11 + (double)matrix1.M22 * (double)matrix2.M21 + (double)matrix1.M23 * (double)matrix2.M31 + (double)matrix1.M24 * (double)matrix2.M41);
            float num6 = (float)((double)matrix1.M21 * (double)matrix2.M12 + (double)matrix1.M22 * (double)matrix2.M22 + (double)matrix1.M23 * (double)matrix2.M32 + (double)matrix1.M24 * (double)matrix2.M42);
            float num7 = (float)((double)matrix1.M21 * (double)matrix2.M13 + (double)matrix1.M22 * (double)matrix2.M23 + (double)matrix1.M23 * (double)matrix2.M33 + (double)matrix1.M24 * (double)matrix2.M43);
            float num8 = (float)((double)matrix1.M21 * (double)matrix2.M14 + (double)matrix1.M22 * (double)matrix2.M24 + (double)matrix1.M23 * (double)matrix2.M34 + (double)matrix1.M24 * (double)matrix2.M44);
            float num9 = (float)((double)matrix1.M31 * (double)matrix2.M11 + (double)matrix1.M32 * (double)matrix2.M21 + (double)matrix1.M33 * (double)matrix2.M31 + (double)matrix1.M34 * (double)matrix2.M41);
            float num10 = (float)((double)matrix1.M31 * (double)matrix2.M12 + (double)matrix1.M32 * (double)matrix2.M22 + (double)matrix1.M33 * (double)matrix2.M32 + (double)matrix1.M34 * (double)matrix2.M42);
            float num11 = (float)((double)matrix1.M31 * (double)matrix2.M13 + (double)matrix1.M32 * (double)matrix2.M23 + (double)matrix1.M33 * (double)matrix2.M33 + (double)matrix1.M34 * (double)matrix2.M43);
            float num12 = (float)((double)matrix1.M31 * (double)matrix2.M14 + (double)matrix1.M32 * (double)matrix2.M24 + (double)matrix1.M33 * (double)matrix2.M34 + (double)matrix1.M34 * (double)matrix2.M44);
            float num13 = (float)((double)matrix1.M41 * (double)matrix2.M11 + (double)matrix1.M42 * (double)matrix2.M21 + (double)matrix1.M43 * (double)matrix2.M31 + (double)matrix1.M44 * (double)matrix2.M41);
            float num14 = (float)((double)matrix1.M41 * (double)matrix2.M12 + (double)matrix1.M42 * (double)matrix2.M22 + (double)matrix1.M43 * (double)matrix2.M32 + (double)matrix1.M44 * (double)matrix2.M42);
            float num15 = (float)((double)matrix1.M41 * (double)matrix2.M13 + (double)matrix1.M42 * (double)matrix2.M23 + (double)matrix1.M43 * (double)matrix2.M33 + (double)matrix1.M44 * (double)matrix2.M43);
            float num16 = (float)((double)matrix1.M41 * (double)matrix2.M14 + (double)matrix1.M42 * (double)matrix2.M24 + (double)matrix1.M43 * (double)matrix2.M34 + (double)matrix1.M44 * (double)matrix2.M44);
            result.M11 = num1;
            result.M12 = num2;
            result.M13 = num3;
            result.M14 = num4;
            result.M21 = num5;
            result.M22 = num6;
            result.M23 = num7;
            result.M24 = num8;
            result.M31 = num9;
            result.M32 = num10;
            result.M33 = num11;
            result.M34 = num12;
            result.M41 = num13;
            result.M42 = num14;
            result.M43 = num15;
            result.M44 = num16;
        }

        public static Matrix Multiply(Matrix matrix1, float scaleFactor)
        {
            Matrix result;
            Matrix.Multiply(ref matrix1, scaleFactor, out result);
            return result;
        }

        public static void Multiply(ref Matrix matrix1, float scaleFactor, out Matrix result)
        {
            result.M11 = matrix1.M11 * scaleFactor;
            result.M12 = matrix1.M12 * scaleFactor;
            result.M13 = matrix1.M13 * scaleFactor;
            result.M14 = matrix1.M14 * scaleFactor;
            result.M21 = matrix1.M21 * scaleFactor;
            result.M22 = matrix1.M22 * scaleFactor;
            result.M23 = matrix1.M23 * scaleFactor;
            result.M24 = matrix1.M24 * scaleFactor;
            result.M31 = matrix1.M31 * scaleFactor;
            result.M32 = matrix1.M32 * scaleFactor;
            result.M33 = matrix1.M33 * scaleFactor;
            result.M34 = matrix1.M34 * scaleFactor;
            result.M41 = matrix1.M41 * scaleFactor;
            result.M42 = matrix1.M42 * scaleFactor;
            result.M43 = matrix1.M43 * scaleFactor;
            result.M44 = matrix1.M44 * scaleFactor;
        }

        public static Matrix Divide(Matrix matrix1, Matrix matrix2)
        {
            Matrix result;
            Matrix.Divide(ref matrix1, ref matrix2, out result);
            return result;
        }

        public static void Divide(ref Matrix matrix1, ref Matrix matrix2, out Matrix result)
        {
            result.M11 = matrix1.M11 / matrix2.M11;
            result.M12 = matrix1.M12 / matrix2.M12;
            result.M13 = matrix1.M13 / matrix2.M13;
            result.M14 = matrix1.M14 / matrix2.M14;
            result.M21 = matrix1.M21 / matrix2.M21;
            result.M22 = matrix1.M22 / matrix2.M22;
            result.M23 = matrix1.M23 / matrix2.M23;
            result.M24 = matrix1.M24 / matrix2.M24;
            result.M31 = matrix1.M31 / matrix2.M31;
            result.M32 = matrix1.M32 / matrix2.M32;
            result.M33 = matrix1.M33 / matrix2.M33;
            result.M34 = matrix1.M34 / matrix2.M34;
            result.M41 = matrix1.M41 / matrix2.M41;
            result.M42 = matrix1.M42 / matrix2.M42;
            result.M43 = matrix1.M43 / matrix2.M43;
            result.M44 = matrix1.M44 / matrix2.M44;
        }

        public static Matrix Divide(Matrix matrix1, float divider)
        {
            Matrix result;
            Matrix.Divide(ref matrix1, divider, out result);
            return result;
        }

        public static void Divide(ref Matrix matrix1, float divider, out Matrix result)
        {
            float num = 1f / divider;
            result.M11 = matrix1.M11 * num;
            result.M12 = matrix1.M12 * num;
            result.M13 = matrix1.M13 * num;
            result.M14 = matrix1.M14 * num;
            result.M21 = matrix1.M21 * num;
            result.M22 = matrix1.M22 * num;
            result.M23 = matrix1.M23 * num;
            result.M24 = matrix1.M24 * num;
            result.M31 = matrix1.M31 * num;
            result.M32 = matrix1.M32 * num;
            result.M33 = matrix1.M33 * num;
            result.M34 = matrix1.M34 * num;
            result.M41 = matrix1.M41 * num;
            result.M42 = matrix1.M42 * num;
            result.M43 = matrix1.M43 * num;
            result.M44 = matrix1.M44 * num;
        }

        public static Matrix Abs(Matrix matrix)
        {
            return new Matrix(System.Math.Abs(matrix.M11), System.Math.Abs(matrix.M12), System.Math.Abs(matrix.M13), System.Math.Abs(matrix.M14), System.Math.Abs(matrix.M21), System.Math.Abs(matrix.M22), System.Math.Abs(matrix.M23), System.Math.Abs(matrix.M24), System.Math.Abs(matrix.M31), System.Math.Abs(matrix.M32), System.Math.Abs(matrix.M33), System.Math.Abs(matrix.M34), System.Math.Abs(matrix.M41), System.Math.Abs(matrix.M42), System.Math.Abs(matrix.M43), System.Math.Abs(matrix.M44));
        }

        public static void Abs(ref Matrix matrix1, out Matrix result)
        {
            result.M11 = System.Math.Abs(matrix1.M11);
            result.M12 = System.Math.Abs(matrix1.M12);
            result.M13 = System.Math.Abs(matrix1.M13);
            result.M14 = System.Math.Abs(matrix1.M14);
            result.M21 = System.Math.Abs(matrix1.M21);
            result.M22 = System.Math.Abs(matrix1.M22);
            result.M23 = System.Math.Abs(matrix1.M23);
            result.M24 = System.Math.Abs(matrix1.M24);
            result.M31 = System.Math.Abs(matrix1.M31);
            result.M32 = System.Math.Abs(matrix1.M32);
            result.M33 = System.Math.Abs(matrix1.M33);
            result.M34 = System.Math.Abs(matrix1.M34);
            result.M41 = System.Math.Abs(matrix1.M41);
            result.M42 = System.Math.Abs(matrix1.M42);
            result.M43 = System.Math.Abs(matrix1.M43);
            result.M44 = System.Math.Abs(matrix1.M44);
        }

        public static Matrix operator -(Matrix matrix1)
        {
            Matrix matrix;
            matrix.M11 = -matrix1.M11;
            matrix.M12 = -matrix1.M12;
            matrix.M13 = -matrix1.M13;
            matrix.M14 = -matrix1.M14;
            matrix.M21 = -matrix1.M21;
            matrix.M22 = -matrix1.M22;
            matrix.M23 = -matrix1.M23;
            matrix.M24 = -matrix1.M24;
            matrix.M31 = -matrix1.M31;
            matrix.M32 = -matrix1.M32;
            matrix.M33 = -matrix1.M33;
            matrix.M34 = -matrix1.M34;
            matrix.M41 = -matrix1.M41;
            matrix.M42 = -matrix1.M42;
            matrix.M43 = -matrix1.M43;
            matrix.M44 = -matrix1.M44;
            return matrix;
        }

        public static bool operator ==(Matrix matrix1, Matrix matrix2)
        {
            return matrix1.Equals(ref matrix2);
        }

        public static bool operator !=(Matrix matrix1, Matrix matrix2)
        {
            return !matrix1.Equals(ref matrix2);
        }

        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            Matrix matrix;
            matrix.M11 = matrix1.M11 + matrix2.M11;
            matrix.M12 = matrix1.M12 + matrix2.M12;
            matrix.M13 = matrix1.M13 + matrix2.M13;
            matrix.M14 = matrix1.M14 + matrix2.M14;
            matrix.M21 = matrix1.M21 + matrix2.M21;
            matrix.M22 = matrix1.M22 + matrix2.M22;
            matrix.M23 = matrix1.M23 + matrix2.M23;
            matrix.M24 = matrix1.M24 + matrix2.M24;
            matrix.M31 = matrix1.M31 + matrix2.M31;
            matrix.M32 = matrix1.M32 + matrix2.M32;
            matrix.M33 = matrix1.M33 + matrix2.M33;
            matrix.M34 = matrix1.M34 + matrix2.M34;
            matrix.M41 = matrix1.M41 + matrix2.M41;
            matrix.M42 = matrix1.M42 + matrix2.M42;
            matrix.M43 = matrix1.M43 + matrix2.M43;
            matrix.M44 = matrix1.M44 + matrix2.M44;
            return matrix;
        }

        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {
            Matrix matrix;
            matrix.M11 = matrix1.M11 - matrix2.M11;
            matrix.M12 = matrix1.M12 - matrix2.M12;
            matrix.M13 = matrix1.M13 - matrix2.M13;
            matrix.M14 = matrix1.M14 - matrix2.M14;
            matrix.M21 = matrix1.M21 - matrix2.M21;
            matrix.M22 = matrix1.M22 - matrix2.M22;
            matrix.M23 = matrix1.M23 - matrix2.M23;
            matrix.M24 = matrix1.M24 - matrix2.M24;
            matrix.M31 = matrix1.M31 - matrix2.M31;
            matrix.M32 = matrix1.M32 - matrix2.M32;
            matrix.M33 = matrix1.M33 - matrix2.M33;
            matrix.M34 = matrix1.M34 - matrix2.M34;
            matrix.M41 = matrix1.M41 - matrix2.M41;
            matrix.M42 = matrix1.M42 - matrix2.M42;
            matrix.M43 = matrix1.M43 - matrix2.M43;
            matrix.M44 = matrix1.M44 - matrix2.M44;
            return matrix;
        }

        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            Matrix matrix;
            matrix.M11 = (float)((double)matrix1.M11 * (double)matrix2.M11 + (double)matrix1.M12 * (double)matrix2.M21 + (double)matrix1.M13 * (double)matrix2.M31 + (double)matrix1.M14 * (double)matrix2.M41);
            matrix.M12 = (float)((double)matrix1.M11 * (double)matrix2.M12 + (double)matrix1.M12 * (double)matrix2.M22 + (double)matrix1.M13 * (double)matrix2.M32 + (double)matrix1.M14 * (double)matrix2.M42);
            matrix.M13 = (float)((double)matrix1.M11 * (double)matrix2.M13 + (double)matrix1.M12 * (double)matrix2.M23 + (double)matrix1.M13 * (double)matrix2.M33 + (double)matrix1.M14 * (double)matrix2.M43);
            matrix.M14 = (float)((double)matrix1.M11 * (double)matrix2.M14 + (double)matrix1.M12 * (double)matrix2.M24 + (double)matrix1.M13 * (double)matrix2.M34 + (double)matrix1.M14 * (double)matrix2.M44);
            matrix.M21 = (float)((double)matrix1.M21 * (double)matrix2.M11 + (double)matrix1.M22 * (double)matrix2.M21 + (double)matrix1.M23 * (double)matrix2.M31 + (double)matrix1.M24 * (double)matrix2.M41);
            matrix.M22 = (float)((double)matrix1.M21 * (double)matrix2.M12 + (double)matrix1.M22 * (double)matrix2.M22 + (double)matrix1.M23 * (double)matrix2.M32 + (double)matrix1.M24 * (double)matrix2.M42);
            matrix.M23 = (float)((double)matrix1.M21 * (double)matrix2.M13 + (double)matrix1.M22 * (double)matrix2.M23 + (double)matrix1.M23 * (double)matrix2.M33 + (double)matrix1.M24 * (double)matrix2.M43);
            matrix.M24 = (float)((double)matrix1.M21 * (double)matrix2.M14 + (double)matrix1.M22 * (double)matrix2.M24 + (double)matrix1.M23 * (double)matrix2.M34 + (double)matrix1.M24 * (double)matrix2.M44);
            matrix.M31 = (float)((double)matrix1.M31 * (double)matrix2.M11 + (double)matrix1.M32 * (double)matrix2.M21 + (double)matrix1.M33 * (double)matrix2.M31 + (double)matrix1.M34 * (double)matrix2.M41);
            matrix.M32 = (float)((double)matrix1.M31 * (double)matrix2.M12 + (double)matrix1.M32 * (double)matrix2.M22 + (double)matrix1.M33 * (double)matrix2.M32 + (double)matrix1.M34 * (double)matrix2.M42);
            matrix.M33 = (float)((double)matrix1.M31 * (double)matrix2.M13 + (double)matrix1.M32 * (double)matrix2.M23 + (double)matrix1.M33 * (double)matrix2.M33 + (double)matrix1.M34 * (double)matrix2.M43);
            matrix.M34 = (float)((double)matrix1.M31 * (double)matrix2.M14 + (double)matrix1.M32 * (double)matrix2.M24 + (double)matrix1.M33 * (double)matrix2.M34 + (double)matrix1.M34 * (double)matrix2.M44);
            matrix.M41 = (float)((double)matrix1.M41 * (double)matrix2.M11 + (double)matrix1.M42 * (double)matrix2.M21 + (double)matrix1.M43 * (double)matrix2.M31 + (double)matrix1.M44 * (double)matrix2.M41);
            matrix.M42 = (float)((double)matrix1.M41 * (double)matrix2.M12 + (double)matrix1.M42 * (double)matrix2.M22 + (double)matrix1.M43 * (double)matrix2.M32 + (double)matrix1.M44 * (double)matrix2.M42);
            matrix.M43 = (float)((double)matrix1.M41 * (double)matrix2.M13 + (double)matrix1.M42 * (double)matrix2.M23 + (double)matrix1.M43 * (double)matrix2.M33 + (double)matrix1.M44 * (double)matrix2.M43);
            matrix.M44 = (float)((double)matrix1.M41 * (double)matrix2.M14 + (double)matrix1.M42 * (double)matrix2.M24 + (double)matrix1.M43 * (double)matrix2.M34 + (double)matrix1.M44 * (double)matrix2.M44);
            return matrix;
        }

        public static Matrix operator *(Matrix matrix, float scaleFactor)
        {
            Matrix matrix1;
            matrix1.M11 = matrix.M11 * scaleFactor;
            matrix1.M12 = matrix.M12 * scaleFactor;
            matrix1.M13 = matrix.M13 * scaleFactor;
            matrix1.M14 = matrix.M14 * scaleFactor;
            matrix1.M21 = matrix.M21 * scaleFactor;
            matrix1.M22 = matrix.M22 * scaleFactor;
            matrix1.M23 = matrix.M23 * scaleFactor;
            matrix1.M24 = matrix.M24 * scaleFactor;
            matrix1.M31 = matrix.M31 * scaleFactor;
            matrix1.M32 = matrix.M32 * scaleFactor;
            matrix1.M33 = matrix.M33 * scaleFactor;
            matrix1.M34 = matrix.M34 * scaleFactor;
            matrix1.M41 = matrix.M41 * scaleFactor;
            matrix1.M42 = matrix.M42 * scaleFactor;
            matrix1.M43 = matrix.M43 * scaleFactor;
            matrix1.M44 = matrix.M44 * scaleFactor;
            return matrix1;
        }

        public static Matrix operator *(float scaleFactor, Matrix matrix)
        {
            Matrix matrix1;
            matrix1.M11 = matrix.M11 * scaleFactor;
            matrix1.M12 = matrix.M12 * scaleFactor;
            matrix1.M13 = matrix.M13 * scaleFactor;
            matrix1.M14 = matrix.M14 * scaleFactor;
            matrix1.M21 = matrix.M21 * scaleFactor;
            matrix1.M22 = matrix.M22 * scaleFactor;
            matrix1.M23 = matrix.M23 * scaleFactor;
            matrix1.M24 = matrix.M24 * scaleFactor;
            matrix1.M31 = matrix.M31 * scaleFactor;
            matrix1.M32 = matrix.M32 * scaleFactor;
            matrix1.M33 = matrix.M33 * scaleFactor;
            matrix1.M34 = matrix.M34 * scaleFactor;
            matrix1.M41 = matrix.M41 * scaleFactor;
            matrix1.M42 = matrix.M42 * scaleFactor;
            matrix1.M43 = matrix.M43 * scaleFactor;
            matrix1.M44 = matrix.M44 * scaleFactor;
            return matrix1;
        }

        public static Matrix operator /(Matrix matrix1, Matrix matrix2)
        {
            Matrix matrix;
            matrix.M11 = matrix1.M11 / matrix2.M11;
            matrix.M12 = matrix1.M12 / matrix2.M12;
            matrix.M13 = matrix1.M13 / matrix2.M13;
            matrix.M14 = matrix1.M14 / matrix2.M14;
            matrix.M21 = matrix1.M21 / matrix2.M21;
            matrix.M22 = matrix1.M22 / matrix2.M22;
            matrix.M23 = matrix1.M23 / matrix2.M23;
            matrix.M24 = matrix1.M24 / matrix2.M24;
            matrix.M31 = matrix1.M31 / matrix2.M31;
            matrix.M32 = matrix1.M32 / matrix2.M32;
            matrix.M33 = matrix1.M33 / matrix2.M33;
            matrix.M34 = matrix1.M34 / matrix2.M34;
            matrix.M41 = matrix1.M41 / matrix2.M41;
            matrix.M42 = matrix1.M42 / matrix2.M42;
            matrix.M43 = matrix1.M43 / matrix2.M43;
            matrix.M44 = matrix1.M44 / matrix2.M44;
            return matrix;
        }

        public static Matrix operator /(Matrix matrix1, float divider)
        {
            float num = 1f / divider;
            Matrix matrix;
            matrix.M11 = matrix1.M11 * num;
            matrix.M12 = matrix1.M12 * num;
            matrix.M13 = matrix1.M13 * num;
            matrix.M14 = matrix1.M14 * num;
            matrix.M21 = matrix1.M21 * num;
            matrix.M22 = matrix1.M22 * num;
            matrix.M23 = matrix1.M23 * num;
            matrix.M24 = matrix1.M24 * num;
            matrix.M31 = matrix1.M31 * num;
            matrix.M32 = matrix1.M32 * num;
            matrix.M33 = matrix1.M33 * num;
            matrix.M34 = matrix1.M34 * num;
            matrix.M41 = matrix1.M41 * num;
            matrix.M42 = matrix1.M42 * num;
            matrix.M43 = matrix1.M43 * num;
            matrix.M44 = matrix1.M44 * num;
            return matrix;
        }

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.M11;
                    case 1:
                        return this.M12;
                    case 2:
                        return this.M13;
                    case 3:
                        return this.M14;
                    case 4:
                        return this.M21;
                    case 5:
                        return this.M22;
                    case 6:
                        return this.M23;
                    case 7:
                        return this.M24;
                    case 8:
                        return this.M31;
                    case 9:
                        return this.M32;
                    case 10:
                        return this.M33;
                    case 11:
                        return this.M34;
                    case 12:
                        return this.M41;
                    case 13:
                        return this.M42;
                    case 14:
                        return this.M43;
                    case 15:
                        return this.M44;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(index), "Indices for Matrix run from 0 to 15, inclusive.");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.M11 = value;
                        break;
                    case 1:
                        this.M12 = value;
                        break;
                    case 2:
                        this.M13 = value;
                        break;
                    case 3:
                        this.M14 = value;
                        break;
                    case 4:
                        this.M21 = value;
                        break;
                    case 5:
                        this.M22 = value;
                        break;
                    case 6:
                        this.M23 = value;
                        break;
                    case 7:
                        this.M24 = value;
                        break;
                    case 8:
                        this.M31 = value;
                        break;
                    case 9:
                        this.M32 = value;
                        break;
                    case 10:
                        this.M33 = value;
                        break;
                    case 11:
                        this.M34 = value;
                        break;
                    case 12:
                        this.M41 = value;
                        break;
                    case 13:
                        this.M42 = value;
                        break;
                    case 14:
                        this.M43 = value;
                        break;
                    case 15:
                        this.M44 = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(index), "Indices for Matrix run from 0 to 15, inclusive.");
                }
            }
        }

        public float this[int row, int column]
        {
            get
            {
                switch (row)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        switch (column)
                        {
                            case 0:
                            case 1:
                            case 2:
                            case 3:
                                return this[row * 4 + column];
                            default:
                                throw new ArgumentOutOfRangeException(nameof(column), "Rows and columns for matrices run from 0 to 3, inclusive.");
                        }
                    default:
                        throw new ArgumentOutOfRangeException(nameof(row), "Rows and columns for matrices run from 0 to 3, inclusive.");
                }
            }
            set
            {
                switch (row)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        switch (column)
                        {
                            case 0:
                            case 1:
                            case 2:
                            case 3:
                                this[row * 4 + column] = value;
                                return;
                            default:
                                throw new ArgumentOutOfRangeException(nameof(column), "Rows and columns for matrices run from 0 to 3, inclusive.");
                        }
                    default:
                        throw new ArgumentOutOfRangeException(nameof(row), "Rows and columns for matrices run from 0 to 3, inclusive.");
                }
            }
        }

        public static void ToEngineFlippedYMatrix(ref Matrix m, float[] mat)
        {
            m.M11 = mat[0];
            m.M12 = mat[1];
            m.M13 = mat[2];
            m.M14 = mat[3];
            m.M21 = mat[4];
            m.M22 = -mat[5];
            m.M23 = mat[6];
            m.M24 = mat[7];
            m.M31 = mat[8];
            m.M32 = mat[9];
            m.M33 = mat[10];
            m.M34 = mat[11];
            m.M41 = mat[12];
            m.M42 = mat[5] + mat[13];
            m.M43 = mat[14];
            m.M44 = mat[15];
        }
    }
}
