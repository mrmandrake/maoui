namespace WebGl
{
    public static class FloatExtensions
    {
        public static bool Equal(this float a, float b, float maxRelativeError = 0.0001f)
        {
            return (double)System.Math.Abs(a - b) < (double)maxRelativeError;
        }

        public static bool Distinct(this float a, float b, float maxRelativeError = 0.0001f)
        {
            return (double)System.Math.Abs(a - b) > (double)maxRelativeError;
        }
    }
}
