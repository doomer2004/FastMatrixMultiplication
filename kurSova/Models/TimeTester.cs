using System.Diagnostics;

namespace kurSova.Models
{
    public delegate Matrix MatrixMultiply(Matrix m1, Matrix m2);
    public static class TimeTester
    {
        public static (Matrix matrix, long milliseconds) TestMatrixTime(Matrix m1, Matrix m2, MatrixMultiply multiply)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var matrix = multiply(m1, m2);
            stopwatch.Stop();
            return (matrix, stopwatch.ElapsedMilliseconds);
        }
    }
}