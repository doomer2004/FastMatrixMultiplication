using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace kurSova.Models
{
    [Serializable]
     public sealed class Matrix
    {
        public int Rows
        {
            get
            {
                return values.GetLength(0);
            }
        }
        public int Columns
        {
            get
            {
                return values.GetLength(1);
            }
        }
        [JsonInclude]
        public double[,] values { get; }
        public double this[int row, int col]
        {
            get
            {
                return values[row, col];
            }
            set
            {
                values[row, col] = value;
            }
        }

        public Matrix(int m, int n)
        {
            if (m < 0)
                throw new ArgumentOutOfRangeException("m is less than  to 0.");
            if (n < 0)
                throw new ArgumentOutOfRangeException("n is less than  to 0.");

            values = new double[m, n];
        }

        public Matrix(int m) : this(m, m) { }

        public Matrix(double[,] values)
        {
            if (values == null)
                throw new ArgumentNullException("values == null.");

            this.values = values;
        }

        public Matrix()
        {
            
        }

        public override bool Equals(object obj)
        {
            Matrix a = this, b = (Matrix)obj;

            if (b == null)
                return false;
            if (a.Rows != b.Rows || a.Columns != b.Columns)
                return false;

            for (int row = 0; row < a.Rows; row++)
                for (int col = 0; col < a.Columns; col++)
                    if (a[row, col] != b[row, col]) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        
        public static Matrix operator +(Matrix a, Matrix b)
        {
            return Matrix.Add(a, b);
        }

        public static Matrix operator -(Matrix a, Matrix b)
        {
            return Matrix.Subtract(a, b);
        }

        public static Matrix Add(Matrix a, Matrix b)
        {
            if (a.Rows != b.Rows || a.Columns != b.Columns) throw new ArgumentException("Not identical matrices.");
            
            Matrix result = new Matrix(a.Rows, a.Columns);
            for (int row = 0; row < a.Rows; row++)
                for (int col = 0; col < a.Columns; col++)
                    result[row, col] = a[row, col] + b[row, col];

            return result;
        }

        public static Matrix Subtract(Matrix a, Matrix b)
        {
            if (a.Rows != b.Rows || a.Columns != b.Columns)
                throw new ArgumentException("Not identical matrices.");

            Matrix result = new Matrix(a.Rows, a.Columns);

            for (int row = 0; row < a.Rows; row++)
                for (int col = 0; col < a.Columns; col++)
                    result[row, col] = a[row, col] - b[row, col];

            return result;
        }

        public static Matrix NormalMultiply(Matrix a, Matrix b)
        {
            if (a.Columns != b.Rows)
                throw new ArgumentException("Number of rows of the matrix a doesnt equal to number of columns of the matrix b.");

            Matrix result = new Matrix(a.Rows, b.Columns);

            for (int row = 0; row < a.Rows; row++)
            {
                for (int col = 0; col < b.Columns; col++)
                {
                    double tmp = 0;
                    for (int i = 0; i < a.Columns; i++) 
                        tmp += a[row, i] * b[i, col];

                    result[row, col] = tmp;
                }
            }

            return result;
        }

        public static Matrix StrassenMultiply(Matrix a, Matrix b)
        {
            var sizes = new int[] { a.Rows, a.Columns, b.Rows, b.Columns };
            if (sizes.Distinct().Count() != 1 || (a.Rows & (a.Rows - 1)) != 0)
                throw new ArgumentException("Not identical or square matrices.");

            int N = b.Rows;
            if (N < 64)
                return NormalMultiply(a, b);
            
            

            int halfN = N / 2;

            var a11 = a.SubMatrix(0, halfN, 0, halfN);
            var a12 = a.SubMatrix(0, halfN, halfN, N);
            var a21 = a.SubMatrix(halfN, N, 0, halfN);
            var a22 = a.SubMatrix(halfN, N, halfN, N);

            var b11 = b.SubMatrix(0, halfN, 0, halfN);
            var b12 = b.SubMatrix(0, halfN, halfN, N);
            var b21 = b.SubMatrix(halfN, N, 0, halfN);
            var b22 = b.SubMatrix(halfN, N, halfN, N);

            Matrix[] m = new Matrix[]{
                StrassenMultiply(a11 + a22, b11 + b22),     
                StrassenMultiply(a21 + a22, b11),           
                StrassenMultiply(a11, b12 - b22),           
                StrassenMultiply(a22, b21 - b11),           
                StrassenMultiply(a11 + a12, b22),           
                StrassenMultiply(a21 - a11, b11 + b12),     
                StrassenMultiply(a12 - a22, b21 + b22),     
            };

            var c11 = m[0] + m[3] - m[4] + m[6];
            var c12 = m[2] + m[4];
            var c21 = m[1] + m[3];
            var c22 = m[0] - m[1] + m[2] + m[5];

            return CombineSubMatrices(c11, c12, c21, c22);
        }

        private Matrix SubMatrix(int rowFrom, int rowTo, int colFrom, int colTo)
        {
            Matrix result = new Matrix(rowTo - rowFrom, colTo - colFrom);
            for (int row = rowFrom, i = 0; row < rowTo; row++, i++)
                for (int col = colFrom, j = 0; col < colTo; col++, j++)
                    result[i, j] = values[row, col];
            return result;
        }

        private static Matrix CombineSubMatrices(Matrix a11, Matrix a12, Matrix a21, Matrix a22)
        {
            Matrix result = new Matrix(a11.Rows * 2);
            int shift = a11.Rows;
            for (int row = 0; row < a11.Rows; row++)
                for (int col = 0; col < a11.Columns; col++)
                {
                    result[row, col] = a11[row, col];
                    result[row, col + shift] = a12[row, col];
                    result[row + shift, col] = a21[row, col];
                    result[row + shift, col + shift] = a22[row, col];
                }
            return result;
        }
        
        public static Matrix StrassenVinogradMultiply(Matrix a, Matrix b)
        {
            var sizes = new int[] { a.Rows, a.Columns, b.Rows, b.Columns };
            if (sizes.Distinct().Count() != 1 || (a.Rows & (a.Rows - 1)) != 0)
                throw new ArgumentException("Not identical or square matrices.");

            int N = b.Rows;
            if (N < 64)
                return NormalMultiply(a, b);
            
            

            int halfN = N / 2;

            var a11 = a.SubMatrix(0, halfN, 0, halfN);
            var a12 = a.SubMatrix(0, halfN, halfN, N);
            var a21 = a.SubMatrix(halfN, N, 0, halfN);
            var a22 = a.SubMatrix(halfN, N, halfN, N);

            var b11 = b.SubMatrix(0, halfN, 0, halfN);
            var b12 = b.SubMatrix(0, halfN, halfN, N);
            var b21 = b.SubMatrix(halfN, N, 0, halfN);
            var b22 = b.SubMatrix(halfN, N, halfN, N);

            var s1 = a21 + a22;
            var s2 = s1 - a11;
            var s3 = a11 - a21;
            var s4 = a12 - s2;
            var s5 = b12 - b11;
            var s6 = b22 - s5;
            var s7 = b22 - b12;
            var s8 = s6 - b21;
            
            Matrix[] m = new Matrix[]{
                StrassenMultiply(s2, s6),     
                StrassenMultiply(a11, b11),     
                StrassenMultiply(a12, b21),     
                StrassenMultiply(s3, s7),     
                StrassenMultiply(s1, s5),     
                StrassenMultiply(s4, b22),     
                StrassenMultiply(a22, s8),     
            };

            var t1 = m[1] + m[1];
            var t2 = t1 + m[3];
            
            var c11 = m[1]  +m[2] ;
            var c12 = t1 + m[4] + m[5];
            var c21 = t2 - m[6];
            var c22 = t2 + m[4];

            return CombineSubMatrices(c11, c12, c21, c22);
        }

        
        
        
    }
}