namespace WebGl
{
    internal class Gjk
    {
        private static int[] bitsToIndices = new int[16]
        {
      0,
      1,
      2,
      17,
      3,
      25,
      26,
      209,
      4,
      33,
      34,
      273,
      35,
      281,
      282,
      2257
        };
        private float[][] det = new float[16][];
        private float[][] edgeLengthSq = new float[4][]
        {
      new float[4],
      new float[4],
      new float[4],
      new float[4]
        };
        private Vector3[][] edges = new Vector3[4][]
        {
      new Vector3[4],
      new Vector3[4],
      new Vector3[4],
      new Vector3[4]
        };
        private Vector3[] y = new Vector3[4];
        private float[] yLengthSq = new float[4];
        private Vector3 closestPoint;
        private float maxLengthSq;
        private int simplexBits;

        public Vector3 ClosestPoint
        {
            get
            {
                return this.closestPoint;
            }
        }

        public bool FullSimplex
        {
            get
            {
                return this.simplexBits == 15;
            }
        }

        public float MaxLengthSquared
        {
            get
            {
                return this.maxLengthSq;
            }
        }

        public Gjk()
        {
            for (int index = 0; index < 16; ++index)
                this.det[index] = new float[4];
        }

        public bool AddSupportPoint(ref Vector3 newPoint)
        {
            int index1 = (Gjk.bitsToIndices[this.simplexBits ^ 15] & 7) - 1;
            this.y[index1] = newPoint;
            this.yLengthSq[index1] = newPoint.LengthSquared();
            for (int bitsToIndex = Gjk.bitsToIndices[this.simplexBits]; bitsToIndex != 0; bitsToIndex >>= 3)
            {
                int index2 = (bitsToIndex & 7) - 1;
                Vector3 vector3 = this.y[index2] - newPoint;
                this.edges[index2][index1] = vector3;
                this.edges[index1][index2] = -vector3;
                this.edgeLengthSq[index1][index2] = this.edgeLengthSq[index2][index1] = vector3.LengthSquared();
            }
            this.UpdateDeterminant(index1);
            return this.UpdateSimplex(index1);
        }

        public void Reset()
        {
            this.simplexBits = 0;
            this.maxLengthSq = 0.0f;
        }

        private Vector3 ComputeClosestPoint()
        {
            float num1 = 0.0f;
            Vector3 zero = Vector3.Zero;
            this.maxLengthSq = 0.0f;
            for (int bitsToIndex = Gjk.bitsToIndices[this.simplexBits]; bitsToIndex != 0; bitsToIndex >>= 3)
            {
                int index = (bitsToIndex & 7) - 1;
                float num2 = this.det[this.simplexBits][index];
                num1 += num2;
                zero += this.y[index] * num2;
                this.maxLengthSq = System.Math.Max(this.maxLengthSq, this.yLengthSq[index]);
            }
            return zero / num1;
        }

        private static float Dot(ref Vector3 a, ref Vector3 b)
        {
            return (float)((double)a.X * (double)b.X + (double)a.Y * (double)b.Y + (double)a.Z * (double)b.Z);
        }

        private bool IsSatisfiesRule(int xBits, int yBits)
        {
            for (int bitsToIndex = Gjk.bitsToIndices[yBits]; bitsToIndex != 0; bitsToIndex >>= 3)
            {
                int index = (bitsToIndex & 7) - 1;
                int num = 1 << index;
                if ((num & xBits) != 0)
                {
                    if ((double)this.det[xBits][index] <= 0.0)
                        return false;
                }
                else if ((double)this.det[xBits | num][index] > 0.0)
                    return false;
            }
            return true;
        }

        private void UpdateDeterminant(int xmIdx)
        {
            int index1 = 1 << xmIdx;
            this.det[index1][xmIdx] = 1f;
            int bitsToIndex = Gjk.bitsToIndices[this.simplexBits];
            int num1 = bitsToIndex;
            int num2 = 0;
            while (num1 != 0)
            {
                int index2 = (num1 & 7) - 1;
                int num3 = 1 << index2;
                int index3 = num3 | index1;
                this.det[index3][index2] = Gjk.Dot(ref this.edges[xmIdx][index2], ref this.y[xmIdx]);
                this.det[index3][xmIdx] = Gjk.Dot(ref this.edges[index2][xmIdx], ref this.y[index2]);
                int num4 = bitsToIndex;
                for (int index4 = 0; index4 < num2; ++index4)
                {
                    int index5 = (num4 & 7) - 1;
                    int num5 = 1 << index5;
                    int index6 = index3 | num5;
                    int index7 = (double)this.edgeLengthSq[index2][index5] < (double)this.edgeLengthSq[xmIdx][index5] ? index2 : xmIdx;
                    this.det[index6][index5] = (float)((double)this.det[index3][index2] * (double)Gjk.Dot(ref this.edges[index7][index5], ref this.y[index2]) + (double)this.det[index3][xmIdx] * (double)Gjk.Dot(ref this.edges[index7][index5], ref this.y[xmIdx]));
                    int index8 = (double)this.edgeLengthSq[index5][index2] < (double)this.edgeLengthSq[xmIdx][index2] ? index5 : xmIdx;
                    this.det[index6][index2] = (float)((double)this.det[num5 | index1][index5] * (double)Gjk.Dot(ref this.edges[index8][index2], ref this.y[index5]) + (double)this.det[num5 | index1][xmIdx] * (double)Gjk.Dot(ref this.edges[index8][index2], ref this.y[xmIdx]));
                    int index9 = (double)this.edgeLengthSq[index2][xmIdx] < (double)this.edgeLengthSq[index5][xmIdx] ? index2 : index5;
                    this.det[index6][xmIdx] = (float)((double)this.det[num3 | num5][index5] * (double)Gjk.Dot(ref this.edges[index9][xmIdx], ref this.y[index5]) + (double)this.det[num3 | num5][index2] * (double)Gjk.Dot(ref this.edges[index9][xmIdx], ref this.y[index2]));
                    num4 >>= 3;
                }
                num1 >>= 3;
                ++num2;
            }
            if ((this.simplexBits | index1) != 15)
                return;
            int index10 = (double)this.edgeLengthSq[1][0] < (double)this.edgeLengthSq[2][0] ? ((double)this.edgeLengthSq[1][0] < (double)this.edgeLengthSq[3][0] ? 1 : 3) : ((double)this.edgeLengthSq[2][0] < (double)this.edgeLengthSq[3][0] ? 2 : 3);
            this.det[15][0] = (float)((double)this.det[14][1] * (double)Gjk.Dot(ref this.edges[index10][0], ref this.y[1]) + (double)this.det[14][2] * (double)Gjk.Dot(ref this.edges[index10][0], ref this.y[2]) + (double)this.det[14][3] * (double)Gjk.Dot(ref this.edges[index10][0], ref this.y[3]));
            int index11 = (double)this.edgeLengthSq[0][1] < (double)this.edgeLengthSq[2][1] ? ((double)this.edgeLengthSq[0][1] < (double)this.edgeLengthSq[3][1] ? 0 : 3) : ((double)this.edgeLengthSq[2][1] < (double)this.edgeLengthSq[3][1] ? 2 : 3);
            this.det[15][1] = (float)((double)this.det[13][0] * (double)Gjk.Dot(ref this.edges[index11][1], ref this.y[0]) + (double)this.det[13][2] * (double)Gjk.Dot(ref this.edges[index11][1], ref this.y[2]) + (double)this.det[13][3] * (double)Gjk.Dot(ref this.edges[index11][1], ref this.y[3]));
            int index12 = (double)this.edgeLengthSq[0][2] < (double)this.edgeLengthSq[1][2] ? ((double)this.edgeLengthSq[0][2] < (double)this.edgeLengthSq[3][2] ? 0 : 3) : ((double)this.edgeLengthSq[1][2] < (double)this.edgeLengthSq[3][2] ? 1 : 3);
            this.det[15][2] = (float)((double)this.det[11][0] * (double)Gjk.Dot(ref this.edges[index12][2], ref this.y[0]) + (double)this.det[11][1] * (double)Gjk.Dot(ref this.edges[index12][2], ref this.y[1]) + (double)this.det[11][3] * (double)Gjk.Dot(ref this.edges[index12][2], ref this.y[3]));
            int index13 = (double)this.edgeLengthSq[0][3] < (double)this.edgeLengthSq[1][3] ? ((double)this.edgeLengthSq[0][3] < (double)this.edgeLengthSq[2][3] ? 0 : 2) : ((double)this.edgeLengthSq[1][3] < (double)this.edgeLengthSq[2][3] ? 1 : 2);
            this.det[15][3] = (float)((double)this.det[7][0] * (double)Gjk.Dot(ref this.edges[index13][3], ref this.y[0]) + (double)this.det[7][1] * (double)Gjk.Dot(ref this.edges[index13][3], ref this.y[1]) + (double)this.det[7][2] * (double)Gjk.Dot(ref this.edges[index13][3], ref this.y[2]));
        }

        private bool UpdateSimplex(int newIndex)
        {
            int yBits = this.simplexBits | 1 << newIndex;
            int xBits = 1 << newIndex;
            for (int simplexBits = this.simplexBits; simplexBits != 0; --simplexBits)
            {
                if ((simplexBits & yBits) == simplexBits && this.IsSatisfiesRule(simplexBits | xBits, yBits))
                {
                    this.simplexBits = simplexBits | xBits;
                    this.closestPoint = this.ComputeClosestPoint();
                    return true;
                }
            }
            bool flag = false;
            if (this.IsSatisfiesRule(xBits, yBits))
            {
                this.simplexBits = xBits;
                this.closestPoint = this.y[newIndex];
                this.maxLengthSq = this.yLengthSq[newIndex];
                flag = true;
            }
            return flag;
        }
    }
}
