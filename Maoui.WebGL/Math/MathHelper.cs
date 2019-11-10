namespace WebGl
{
    public static class MathHelper
    {
        public const float E = 2.718282f;
        public const float Log10E = 0.4342945f;
        public const float Log2E = 1.442695f;
        public const float Pi = 3.141593f;
        public const float PiOver2 = 1.570796f;
        public const float PiOver4 = 0.7853982f;
        public const float TwoPi = 6.283185f;
        public const float Epsilon = 1.192093E-07f;

        public static float Barycentric(
          float value1,
          float value2,
          float value3,
          float amount1,
          float amount2)
        {
            return (float)((double)value1 + (double)amount1 * ((double)value2 - (double)value1) + (double)amount2 * ((double)value3 - (double)value1));
        }

        public static float CatmullRom(
          float value1,
          float value2,
          float value3,
          float value4,
          float amount)
        {
            float num1 = amount * amount;
            float num2 = amount * num1;
            return (float)(0.5 * (2.0 * (double)value2 + (-(double)value1 + (double)value3) * (double)amount + (2.0 * (double)value1 - 5.0 * (double)value2 + 4.0 * (double)value3 - (double)value4) * (double)num1 + (-(double)value1 + 3.0 * (double)value2 - 3.0 * (double)value3 + (double)value4) * (double)num2));
        }

        public static float Clamp(float value, float min, float max)
        {
            value = (double)value > (double)max ? max : value;
            value = (double)value < (double)min ? min : value;
            return value;
        }

        public static double Clamp(double value, double min, double max)
        {
            value = value > max ? max : value;
            value = value < min ? min : value;
            return value;
        }

        public static float Distance(float value1, float value2)
        {
            return System.Math.Abs(value1 - value2);
        }

        public static float Hermite(
          float value1,
          float tangent1,
          float value2,
          float tangent2,
          float amount)
        {
            float num1 = amount;
            float num2 = num1 * num1;
            float num3 = num1 * num2;
            float num4 = (float)(2.0 * (double)num3 - 3.0 * (double)num2 + 1.0);
            float num5 = (float)(-2.0 * (double)num3 + 3.0 * (double)num2);
            float num6 = num3 - 2f * num2 + num1;
            float num7 = num3 - num2;
            return (float)((double)value1 * (double)num4 + (double)value2 * (double)num5 + (double)tangent1 * (double)num6 + (double)tangent2 * (double)num7);
        }

        public static float Area(ref Vector2 a, ref Vector2 b, ref Vector2 c)
        {
            return (float)((double)a.X * ((double)b.Y - (double)c.Y) + (double)b.X * ((double)c.Y - (double)a.Y) + (double)c.X * ((double)a.Y - (double)b.Y));
        }

        public static float Lerp(float value1, float value2, float amount)
        {
            return value1 + (value2 - value1) * amount;
        }

        public static double Lerp(double value1, double value2, double amount)
        {
            return value1 + (value2 - value1) * amount;
        }

        public static float LerpClamped(float value1, float value2, float amount)
        {
            return value1 + (value2 - value1) * MathHelper.Clamp(amount, 0.0f, 1f);
        }

        public static float Max(float value1, float value2)
        {
            return System.Math.Max(value1, value2);
        }

        public static float Min(float value1, float value2)
        {
            return System.Math.Min(value1, value2);
        }

        public static float Max(ref Vector2 value)
        {
            float num = value.X;
            if ((double)num < (double)value.Y)
                num = value.Y;
            return num;
        }

        public static float Min(ref Vector2 value)
        {
            float num = value.X;
            if ((double)num > (double)value.Y)
                num = value.Y;
            return num;
        }

        public static float Max(ref Vector3 value)
        {
            float num = value.X;
            if ((double)num < (double)value.Y)
                num = value.Y;
            if ((double)num < (double)value.Z)
                num = value.Z;
            return num;
        }

        public static float Min(ref Vector3 value)
        {
            float num = value.X;
            if ((double)num > (double)value.Y)
                num = value.Y;
            if ((double)num > (double)value.Z)
                num = value.Z;
            return num;
        }

        public static float SmoothStep(float value1, float value2, float amount)
        {
            float num = MathHelper.Clamp(amount, 0.0f, 1f);
            return MathHelper.Lerp(value1, value2, (float)((double)num * (double)num * (3.0 - 2.0 * (double)num)));
        }

        public static float SmoothDamp(
          float current,
          float target,
          ref float currentVelocity,
          float smoothTime,
          float gameTime)
        {
            smoothTime = System.Math.Max(0.0001f, smoothTime);
            float num1 = 2f / smoothTime;
            float num2 = num1 * gameTime;
            float num3 = (float)(1.0 / (1.0 + (double)num2 + 0.479999989271164 * (double)num2 * (double)num2 + 0.234999999403954 * (double)num2 * (double)num2 * (double)num2));
            float num4 = current - target;
            float num5 = target;
            target = current - num4;
            float num6 = (currentVelocity + num1 * num4) * gameTime;
            currentVelocity = (currentVelocity - num1 * num6) * num3;
            float num7 = target + (num4 + num6) * num3;
            if ((double)num5 - (double)current > 0.0 == (double)num7 > (double)num5)
            {
                num7 = num5;
                currentVelocity = (num7 - num5) / gameTime;
            }
            return num7;
        }

        public static float ToDegrees(float radians)
        {
            return radians * 57.29578f;
        }

        public static float ToDegrees(double radians)
        {
            return (float)(radians * 57.29578);
        }

        public static float ToRadians(float degrees)
        {
            return degrees * ((float)System.Math.PI / 180f);
        }

        public static float ToRadians(double degrees)
        {
            return (float)(degrees * 0.01745329);
        }

        public static float WrapAngle(float angle)
        {
            angle = (float)System.Math.IEEERemainder((double)angle, 6.28318548202515);
            if ((double)angle <= -3.14159297943115)
            {
                angle += 6.283185f;
                return angle;
            }
            if ((double)angle > 3.14159297943115)
                angle -= 6.283185f;
            return angle;
        }

        public static bool FloatEquals(float value1, float value2)
        {
            return (double)System.Math.Abs(value1 - value2) <= 1.19209289550781E-07;
        }

        public static bool FloatEquals(float value1, float value2, float delta)
        {
            return MathHelper.FloatInRange(value1, value2 - delta, value2 + delta);
        }

        public static bool FloatInRange(float value, float min, float max)
        {
            if ((double)value >= (double)min)
                return (double)value <= (double)max;
            return false;
        }
    }
}
