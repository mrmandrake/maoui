namespace WebGl
{
    public static class DoubleExtensions
    {
        public static bool Equal(this double a, double b, float maxRelativeError = 1E-07f)
        {
            return System.Math.Abs(a - b) < (double)maxRelativeError;
        }

        public static bool Distinct(this double a, double b, float maxRelativeError = 1E-07f)
        {
            return System.Math.Abs(a - b) > (double)maxRelativeError;
        }
    }
}
