using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Newtonsoft.Json;
namespace kurSova.Models
{
    public static class MatrixSaver
    {
        public static string path1 { get; } = "matrixRead1.txt";
        public static string path2 { get; } = "matrixRead2.txt";
        public static string pathRes { get; } = "matrixRes.txt";
        public static void SaveMatrix(Matrix matrix)
        {
            Dictionary<int, int[]> savedMatrix = new Dictionary<int, int[]>();
            for (int i = 0; i < matrix.ColumnsCount; i++)
            {
                int[] tempMatrix = new int[matrix.RowsCount];
                for (int j = 0; j < matrix.RowsCount; j++)
                {
                    tempMatrix[j] = matrix[i, j];
                }
                savedMatrix.Add(i, tempMatrix);
            }

            using (StreamWriter writer = new StreamWriter(pathRes, append: false))
            {
                string json = JsonConvert.SerializeObject(savedMatrix, Formatting.Indented);
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
            if (fromJson == null)
            {
                throw new NullReferenceException("Unable to Deserialize");
            }
            return readedMatrix;
        }


    }
}