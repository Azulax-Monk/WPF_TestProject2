using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_TestProject2.Classes
{
    class MatrixUtils
    {
        public double [] MatrixVectorMultiply(double[,] a, double[] b)
        {
            int rowSize = a.GetLength(0);
            int colSize = a.GetLength(1);
            double[] res = new double[rowSize];

            for(int i = 0; i < rowSize; i++)
            {
                for(int j = 0; j < colSize; j++)
                {
                    res[i] += a[i, j] * b[j];
                }
            }
            return res;
        }
    }
}
