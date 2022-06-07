using kurSova.Enums;
using kurSova.Models;

namespace kurSova.ViewModels
{
    public delegate void ShowMillisecondsHandler(long[] milliseconds, MultiplyType type);
    public class ViewModel
    {
        private Matrix m1;
        private Matrix m2;
        private Matrix resMatrix;

        public ViewModel()
        {
            m1 = new Matrix();
            m2 = new Matrix();
            resMatrix = new Matrix();
        }
        public event ShowMillisecondsHandler ShowMilliseconds;
        public void MatrixFirstFromFile()
        {
            MatrixInitFromFile(out m1, MatrixSaver.path1);
        }
        public void MatrixSecondFromFile()
        {
            MatrixInitFromFile(out m2, MatrixSaver.path2);
        }
        private void MatrixInitFromFile(out Matrix matrix, string path)
        {
            matrix = MatrixSaver.ReadMatrixFromFile(path);
        }

        private void MatrixRandom(out Matrix matrix, int row, int col)
        {
            matrix = MatrixGenerator.Generate(row, col);
        }

        public void MatrixFirstRandom(int row, int col)
        {
            MatrixRandom(out m1, row, col);
        }
        public void MatrixSecondRandom(int row, int col)
        {
            MatrixRandom(out m2, row, col);
        }

        public void NormalMultiply()
        {
            var (matrix, milliseconds) = TimeTester.TestMatrixTime(m1, m2, Matrix.NormalMultiply);
            resMatrix = matrix;
            MatrixSaver.SaveMatrix(resMatrix);
            ShowMilliseconds?.Invoke(new long[] { milliseconds }, MultiplyType.NormalMultiply);
        }
        public void StrassenMultiply()
        {
            var (matrix, milliseconds) = TimeTester.TestMatrixTime(m1, m2, Matrix.StrassenMultiply);
            resMatrix = matrix;
            MatrixSaver.SaveMatrix(resMatrix);
            ShowMilliseconds?.Invoke(new long[] { milliseconds }, MultiplyType.StrassenMultiply);
        }
        public void StrassenVinogradMultiply()
        {
            var (matrix, milliseconds) = TimeTester.TestMatrixTime(m1, m2, Matrix.StrassenVinogradMultiply);
            resMatrix = matrix;
            MatrixSaver.SaveMatrix(resMatrix);
            ShowMilliseconds?.Invoke(new long[] { milliseconds }, MultiplyType.StrassenVinogradMultiply);
        }
        public void AllMultiplys()
        {
            long millisecondsNorm = TimeTester.TestMatrixTime(m1, m2, Matrix.NormalMultiply).milliseconds;
            var (matrix, millisecondsStrassen) = TimeTester.TestMatrixTime(m1, m2, Matrix.StrassenMultiply);
            long millisecondsStrassenVinograd = TimeTester.TestMatrixTime(m1, m2, Matrix.StrassenVinogradMultiply).milliseconds;
            resMatrix = matrix;
            MatrixSaver.SaveMatrix(resMatrix);
            ShowMilliseconds?.Invoke(new long[] { millisecondsNorm, millisecondsStrassen, millisecondsStrassenVinograd }, MultiplyType.All);
        }

    }
}