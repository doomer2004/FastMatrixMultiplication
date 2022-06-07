using System;
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

            for (int row = 0; row < matrix.RowsCount; row++)
                for (int col = 0; col < matrix.ColumnsCount; col++)
                    matrix[row, col] = random.Next(-9, 9 + 1);

            return m <= 63 || n <= 63 ? throw new InvalidOperationException("Unable size") : matrix;
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