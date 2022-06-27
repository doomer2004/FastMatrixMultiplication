using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Newtonsoft.Json;
using Path = System.Windows.Shapes.Path;

namespace kurSova.Models
{
    public static class MatrixSaver
    {
        public static string Path1 { get;  }= "matrixRead1.txt";
        public static string Path2 { get;  }= "matrixRead2.txt";
        public static string PathRes { get;  }= "matrixRes.txt";

        static MatrixSaver()
        {
            RewriteFile(Path1);
            RewriteFile(Path2);
            RewriteFile(PathRes);
        }
        
        public static void SaveMatrix(Matrix matrix, string path)
        {
            Matrix clearMatrix = matrix.RemoveZeroVlues();
            Dictionary<int, int[]> savedMatrix = new Dictionary<int, int[]>();
            for (int i = 0; i < clearMatrix.RowsCount; i++)
            {
                int[] tempMatrix = new int[clearMatrix.ColumnsCount];
                for (int j = 0; j < clearMatrix.ColumnsCount; j++)
                {
                    tempMatrix[j] = clearMatrix[i, j];
                }
                savedMatrix.Add(i, tempMatrix);
            }
            using (StreamWriter writer = new StreamWriter(path, append: false))
            {
                string json = JsonConvert.SerializeObject(savedMatrix);
                writer.Write(json);
            }
        }

        public static Matrix ReadMatrixFromFile(string path)
        {
            Dictionary<int, int[]> fromJson;
            using (StreamReader streamReader = new StreamReader(path))
            {
                fromJson = JsonConvert.DeserializeObject<Dictionary<int, int[]>>(streamReader.ReadToEnd());
            }
            Matrix readedMatrix = new Matrix(fromJson.Keys.Count, fromJson.Values.Count);
            int[][] matrix = fromJson.Values.ToArray();
            for (int i = 0; i < readedMatrix.ColumnsCount; i++)
            {
                for (int j = 0; j < readedMatrix.RowsCount; j++)
                {
                    readedMatrix[i, j] = matrix[i][j];
                }
            }
            return fromJson != null ? readedMatrix : throw new NullReferenceException("Unable to Deserialize");
        }
        
        
        private static void RewriteFile(string path) => File.Create(path).Close();
    }
}