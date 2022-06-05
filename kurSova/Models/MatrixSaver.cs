using System;
using System.IO;
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
            using (StreamWriter writer = new StreamWriter(pathRes, append: false))
            {
                string json = JsonSerializer.Serialize(matrix);
                writer.Write(json);
            }
        }

        public static Matrix ReadMatrixFromFile(string path)
        {
            Matrix fromJson;
            using (StreamReader streamReader = new StreamReader(path))
            {
                fromJson = JsonSerializer.Deserialize<Matrix>(streamReader.ReadToEnd());
            }

            if (fromJson == null)
            {
                throw new NullReferenceException("Unable to Deserialize");
            }
            return fromJson;
        }
        
        
    }
}