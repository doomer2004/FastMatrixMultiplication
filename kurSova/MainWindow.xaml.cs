using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using kurSova.Enums;
using kurSova.Models;
using kurSova.ViewModels;

using Microsoft.Win32;

namespace kurSova
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private MultiplyType _multiplyType;
        public MatrixViewModel ViewModel { get; set; }
        public List<MultiplyType> Methods => Enum.GetValues(typeof(MultiplyType)).Cast<MultiplyType>().ToList();
        public MultiplyType SelectedMethod {
            get => _multiplyType;
            set {
                _multiplyType = value;
                Debug.WriteLine(_multiplyType);
            }

        }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            ViewModel = new MatrixViewModel();
            ViewModel.ShowMilliseconds += Show_Milliseconds;
        }
        private void MatrixFromFile_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            OpenFileDialog dialog = new OpenFileDialog() {
                DefaultExt = ".txt",
                Filter = "Text documents (.txt)|*.txt"
            };
            if (dialog.ShowDialog() != true)
                return;

            string path = dialog.FileName;
            Matrix loaded;
            try
            {
                loaded = GetMatrixFromFile(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("File content was incorrect format\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (button.Tag.ToString().EndsWith("A"))
                ViewModel.MatrixA = loaded;
            else
                ViewModel.MatrixB = loaded;
            MessageBox.Show("File loaded successfuly", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void MatrixRandom_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            try
            {
                int n, m;
                if (button.Tag.ToString().EndsWith("A"))
                {
                    m = MatrixASize1.Value.Value;
                    n = MatrixASize2.Value.Value;
                    ViewModel.MatrixA = MatrixGenerator.Generate(n, m);
                }
                else
                {
                    m = MatrixBSize1.Value.Value;
                    n = MatrixBSize2.Value.Value;
                    ViewModel.MatrixB = MatrixGenerator.Generate(n, m);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("Matrix generated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

        }
        private Matrix GetMatrixFromFile(string path)
        {
            return MatrixSaver.ReadMatrixFromFile(path);
        }
        private void Show_Milliseconds(long[] milliseconds, MultiplyType type)
        {
            string res;
            switch (type)
            {
                case MultiplyType.NormalMultiply:
                    res = $"Normal Multiply: {milliseconds[0]}ms";
                    break;
                case MultiplyType.StrassenMultiply:
                    res = $"Strassen Multiply: {milliseconds[0]}ms";
                    break;
                case MultiplyType.StrassenVinogradMultiply:
                    res = $"Strassen-Vinograd Multiply: {milliseconds[0]}ms";
                    break;
                case MultiplyType.All:
                    res = $"Normal Multiply: {milliseconds[0]}ms\nStrassen Multiply: {milliseconds[1]}ms\nStrassen-Vinograd Multiply: {milliseconds[2]}ms ";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Out of raange");
            }

            MessageBox.Show(res, "Statistics", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Multiply_Click(object sender, RoutedEventArgs e)
        {
            switch (SelectedMethod)
            {
                case MultiplyType.NormalMultiply:
                    ViewModel.NormalMultiply();

                    break;
                case MultiplyType.StrassenMultiply:
                    ViewModel.StrassenMultiply();

                    break;
                case MultiplyType.StrassenVinogradMultiply:
                    ViewModel.StrassenVinogradMultiply();

                    break;
                case MultiplyType.All:
                    ViewModel.AllMultiplys();

                    break;
            }
        }
    }
}