using kurSova.Enums;
using kurSova.Models;

namespace kurSova.ViewModels
{
    public delegate void ShowMillisecondsHandler(long[] milliseconds, MultiplyType type);
    public class MatrixViewModel
    {
        private Matrix matrixA;
        private Matrix matrixB;
        private Matrix resultMatrix;

        public Matrix MatrixA {
            get => matrixA;
            set => matrixA = value;
        }

        public Matrix MatrixB {
            get => matrixB;
            set => matrixB = value;
        }

        public event ShowMillisecondsHandler ShowMilliseconds;

        public MatrixViewModel()
        {
            matrixA = new Matrix();
            matrixB = new Matrix();
            resultMatrix = new Matrix();
        }

        public void NormalMultiply()
        {
            var (matrix, milliseconds) = TimeTester.TestMatrixTime(matrixA, matrixB, Matrix.NormalMultiply);
            resultMatrix = matrix;
            MatrixSaver.SaveMatrix(resultMatrix, );
            ShowMilliseconds?.Invoke(new long[] { milliseconds }, MultiplyType.NormalMultiply);
        }
        public void StrassenMultiply()
        {
            var (matrix, milliseconds) = TimeTester.TestMatrixTime(matrixA, matrixB, Matrix.StrassenMultiply);
            resultMatrix = matrix;
            MatrixSaver.SaveMatrix(resultMatrix);
            ShowMilliseconds?.Invoke(new long[] { milliseconds }, MultiplyType.StrassenMultiply);
        }
        public void StrassenVinogradMultiply()
        {
            var (matrix, milliseconds) = TimeTester.TestMatrixTime(matrixA, matrixB, Matrix.StrassenVinogradMultiply);
            resultMatrix = matrix;
            MatrixSaver.SaveMatrix(resultMatrix);
            ShowMilliseconds?.Invoke(new long[] { milliseconds }, MultiplyType.StrassenVinogradMultiply);
        }
        public void AllMultiplys()
        {
            long millisecondsNorm = TimeTester.TestMatrixTime(matrixA, matrixB, Matrix.NormalMultiply).milliseconds;
            var (matrix, millisecondsStrassen) = TimeTester.TestMatrixTime(matrixA, matrixB, Matrix.StrassenMultiply);
            long millisecondsStrassenVinograd = TimeTester.TestMatrixTime(matrixA, matrixB, Matrix.StrassenVinogradMultiply).milliseconds;
            resultMatrix = matrix;
            MatrixSaver.SaveMatrix(resultMatrix);
            ShowMilliseconds?.Invoke(new long[] { millisecondsNorm, millisecondsStrassen, millisecondsStrassenVinograd }, MultiplyType.All);
        }

    }
}