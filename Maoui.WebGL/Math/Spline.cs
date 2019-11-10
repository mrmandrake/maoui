namespace WebGl
{
    public struct Spline
    {
        private readonly Vector3 a;
        private readonly Vector3 b;
        private readonly Vector3 c;
        private readonly Vector3 d;

        public Spline(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
        }

        public Vector3 GetPointOnSpline(float s)
        {
            return ((this.d * s + this.c) * s + this.b) * s + this.a;
        }

        public static Spline[] CalculateCubicSpline(int n, Vector3[] v)
        {
            Spline[] c = (Spline[])null;
            Spline.CalculateCubicSpline(ref n, ref v, out c);
            return c;
        }

        public static void CalculateCubicSpline(ref int n, ref Vector3[] v, out Spline[] c)
        {
            Vector3[] vector3Array1 = new Vector3[n + 1];
            Vector3[] vector3Array2 = new Vector3[n + 1];
            Vector3[] vector3Array3 = new Vector3[n + 1];
            vector3Array1[0] = Vector3.Zero;
            vector3Array1[0].X = 0.5f;
            vector3Array1[0].Y = 0.5f;
            vector3Array1[0].Z = 0.5f;
            for (int index = 1; index < n; ++index)
            {
                vector3Array1[index].X = (float)(1.0 / (4.0 - (double)vector3Array1[index - 1].X));
                vector3Array1[index].Y = (float)(1.0 / (4.0 - (double)vector3Array1[index - 1].Y));
                vector3Array1[index].Z = (float)(1.0 / (4.0 - (double)vector3Array1[index - 1].Z));
            }
            vector3Array1[n].X = (float)(1.0 / (2.0 - (double)vector3Array1[n - 1].X));
            vector3Array1[n].Y = (float)(1.0 / (2.0 - (double)vector3Array1[n - 1].Y));
            vector3Array1[n].Z = (float)(1.0 / (2.0 - (double)vector3Array1[n - 1].Z));
            vector3Array2[0].X = (float)(3.0 * ((double)v[1].X - (double)v[0].X)) * vector3Array1[0].X;
            vector3Array2[0].Y = (float)(3.0 * ((double)v[1].Y - (double)v[0].Y)) * vector3Array1[0].Y;
            vector3Array2[0].Z = (float)(3.0 * ((double)v[1].Z - (double)v[0].Z)) * vector3Array1[0].Z;
            for (int index = 1; index < n; ++index)
            {
                vector3Array2[index].X = ((float)(3.0 * ((double)v[index + 1].X - (double)v[index - 1].X)) - vector3Array2[index - 1].X) * vector3Array1[index].X;
                vector3Array2[index].Y = ((float)(3.0 * ((double)v[index + 1].Y - (double)v[index - 1].Y)) - vector3Array2[index - 1].Y) * vector3Array1[index].Y;
                vector3Array2[index].Z = ((float)(3.0 * ((double)v[index + 1].Z - (double)v[index - 1].Z)) - vector3Array2[index - 1].Z) * vector3Array1[index].Z;
            }
            vector3Array2[n].X = ((float)(3.0 * ((double)v[n].X - (double)v[n - 1].X)) - vector3Array2[n - 1].X) * vector3Array1[n].X;
            vector3Array2[n].Y = ((float)(3.0 * ((double)v[n].Y - (double)v[n - 1].Y)) - vector3Array2[n - 1].Y) * vector3Array1[n].Y;
            vector3Array2[n].Z = ((float)(3.0 * ((double)v[n].Z - (double)v[n - 1].Z)) - vector3Array2[n - 1].Z) * vector3Array1[n].Z;
            vector3Array3[n] = vector3Array2[n];
            for (int index = n - 1; index >= 0; --index)
            {
                vector3Array3[index].X = vector3Array2[index].X - vector3Array1[index].X * vector3Array3[index + 1].X;
                vector3Array3[index].Y = vector3Array2[index].Y - vector3Array1[index].Y * vector3Array3[index + 1].Y;
                vector3Array3[index].Z = vector3Array2[index].Z - vector3Array1[index].Z * vector3Array3[index + 1].Z;
            }
            c = new Spline[n];
            for (int index = 0; index < n; ++index)
            {
                Vector3 a = v[index];
                Vector3 b = vector3Array3[index];
                Vector3 vector3_1 = v[index + 1];
                Vector3 vector3_2 = vector3Array3[index + 1];
                c[index] = new Spline(a, b, 3f * (vector3_1 - a) - 2f * b - vector3_2, 2f * (a - vector3_1) + b + vector3_2);
            }
        }
    }
}
