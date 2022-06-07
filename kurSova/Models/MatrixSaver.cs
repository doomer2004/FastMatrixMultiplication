using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace kurSova.Models
{
    public static class MatrixSaver
    {
        public static string path1 { get; } = "matrixRead1";
        public static string path2{ get; } = "matrixRead2";
        public static string pathRes{ get; } = "matrixRes";
        public static void SaveMatrix(Matrix matrix)
        {
            var savedMatrix =new Dictionary<int, int[]>();
            for (int i = 0; i < matrix.Columns; i++)
            {
                int[] tempMatrix = new int [matrix.Rows];
                for (int j = 0; j < matrix.Rows; j++)
                {
                    tempMatrix[j] = matrix[i, j];
                }
                savedMatrix.Add(i, tempMatrix);
            }

            using (StreamWriter writer = new StreamWriter(pathRes, append: false))
            {
                string json = JsonSerializer.Serialize(savedMatrix);
                writer.Write(json);
            }
        }

        public static Matrix ReadMatrixFromFile(string path)
        {
            Dictionary<int, int[]> fromJson;
            using (StreamReader streamReader = new StreamReader(path))
            {
                fromJson = JsonSerializer.Deserialize<Dictionary<int, int[]>>(streamReader.ReadToEnd());
            }
            Matrix readedMatrix = new Matrix(fromJson.Keys.Count, fromJson.Values.Count);
            var matrix = fromJson.Values.ToArray();
            for (int i = 0; i < readedMatrix.Columns; i++)
            {
                for (int j = 0; j < readedMatrix.Rows; j++)
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