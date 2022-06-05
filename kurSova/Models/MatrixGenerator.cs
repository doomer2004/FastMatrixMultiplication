using System;
using System.IO;
using Newtonsoft.Json;
namespace kurSova.Models
{
    
    public static class MatrixGenerator
    {
        private static Random random;

        static MatrixGenerator()
        {
            random = new Random();
        }

        public static Matrix Generate(int m, int n)
        {
            Matrix matrix = new Matrix(m, n);

            for (int row = 0; row < matrix.Rows; row++)
                for (int col = 0; col < matrix.Columns; col++)
                    matrix[row, col] = random.Next(-9, 9 + 1);

            return matrix;
        }

        //public static Matrix Generate(int m, int minValue = -9, int maxValue = 9)
        //{
        //    return Generate(m, m, minValue, maxValue);
        //}

        public static Matrix IdentityMatrix(int m)
        {
            Matrix matrix = new Matrix(m);

            for (int i = 0; i < m; i++)
                matrix[i, i] = 1;

            return matrix;
        }

        
        
    }
}