using System;
using System.Diagnostics;
using kurSova.Enums;
using kurSova.Models;

namespace kurSova.ViewModels
{
    public delegate void ShowMillisecondsHandler(long[] milliseconds, MultiplyType type);
    public class MatrixViewModel
    {
        
        
        private Matrix _matrixA;
        private Matrix _matrixB;
        private Matrix _resultMatrix;

        public Matrix MatrixA {
            get => _matrixA;
            set => _matrixA = value;
        }

        public Matrix MatrixB {
            get => _matrixB;
            set => _matrixB = value;
        }

        public event ShowMillisecondsHandler ShowMilliseconds;

        public MatrixViewModel()
        {
            _matrixA = new Matrix();
            _matrixB = new Matrix();
            _resultMatrix = new Matrix();
        }

        public void NormalMultiply()
        {
            if (MatrixA.ColumnsCount !=MatrixB.RowsCount)
                throw new ArgumentException("Not identical matrices.");
            var (matrix, milliseconds) = TimeTester.TestMatrixTime(_matrixA, _matrixB, Matrix.NormalMultiply);
            MatrixSaver.SaveMatrix(_matrixA, MatrixSaver.Path1);
            MatrixSaver.SaveMatrix(_matrixB, MatrixSaver.Path2);
            _resultMatrix = matrix;
            MatrixSaver.SaveMatrix(_resultMatrix, MatrixSaver.PathRes);
            ShowMilliseconds?.Invoke(new long[] { milliseconds }, MultiplyType.NormalMultiply);
        }
        public void StrassenMultiply()
        {
            if (MatrixA.ColumnsCount !=MatrixB.RowsCount)
                throw new ArgumentException("Not identical matrices.");
            var (matrix, milliseconds) = TimeTester.TestMatrixTime(_matrixA, _matrixB, Matrix.StrassenMultiply);
            MatrixSaver.SaveMatrix(_matrixA, MatrixSaver.Path1);
            MatrixSaver.SaveMatrix(_matrixB, MatrixSaver.Path2);
            _resultMatrix = matrix;
            MatrixSaver.SaveMatrix(_resultMatrix, MatrixSaver.PathRes);
            ShowMilliseconds?.Invoke(new long[] { milliseconds }, MultiplyType.StrassenMultiply);
        }
        public void StrassenVinogradMultiply()
        {
            if (MatrixA.ColumnsCount !=MatrixB.RowsCount)
                throw new ArgumentException("Not identical matrices.");
            var (matrix, milliseconds) = TimeTester.TestMatrixTime(_matrixA, _matrixB, Matrix.StrassenVinogradMultiply);
            MatrixSaver.SaveMatrix(_matrixA, MatrixSaver.Path1);
            MatrixSaver.SaveMatrix(_matrixB, MatrixSaver.Path2);
            _resultMatrix = matrix;
            MatrixSaver.SaveMatrix(_resultMatrix, MatrixSaver.PathRes);
            ShowMilliseconds?.Invoke(new long[] { milliseconds }, MultiplyType.StrassenVinogradMultiply);
        }
        public void AllMultiplys()
        {
            if (MatrixA.ColumnsCount !=MatrixB.RowsCount)
                throw new ArgumentException("Not identical matrices.");
            long millisecondsNorm = TimeTester.TestMatrixTime(_matrixA, _matrixB, Matrix.NormalMultiply).milliseconds;
            var (matrix, millisecondsStrassen) = TimeTester.TestMatrixTime(_matrixA, _matrixB, Matrix.StrassenMultiply);
            long millisecondsStrassenVinograd = TimeTester.TestMatrixTime(_matrixA, _matrixB, Matrix.StrassenVinogradMultiply).milliseconds;
            MatrixSaver.SaveMatrix(_matrixA, MatrixSaver.Path1);
            MatrixSaver.SaveMatrix(_matrixB, MatrixSaver.Path2);
            _resultMatrix = matrix;
            MatrixSaver.SaveMatrix(_resultMatrix, MatrixSaver.PathRes);
            ShowMilliseconds?.Invoke(new long[] { millisecondsNorm, millisecondsStrassen, millisecondsStrassenVinograd }, MultiplyType.All);
        }

        public void ReadMatrix1()
        {
            Process.Start(MatrixSaver.Path1);
        }
        public void ReadMatrix2()
        {
            Process.Start(MatrixSaver.Path2);
        }
        public void ReadMatrixRes()
        {
            Process.Start(MatrixSaver.PathRes);
        }

        public string GetPath1()
        {
            return MatrixSaver.Path1;
        }
        public string GetPath2()
        {
            return MatrixSaver.Path2;
        }
        
        
    }
}