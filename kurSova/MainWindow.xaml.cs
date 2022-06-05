using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using kurSova.Enums;
using kurSova.ViewModels;
namespace kurSova
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private ViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new ViewModel();
            viewModel.ShowMilliseconds += Show_Milliseconds;
        }

        private void From_File(object sender, RoutedEventArgs e)
        {
            if (((Button)sender) == FromFile1)
            {
                viewModel.MatrixFirstFromFile();
            }
            else
            {
                viewModel.MatrixSecondFromFile();
            }
        }

        private void Random_New(object sender, RoutedEventArgs e)
        {
            try
            {
                if (((Button)sender) == RandButton1)
                {
                    string[] text = RandBox1.Text.Split(' ', (char) StringSplitOptions.RemoveEmptyEntries);
                    int row = int.Parse(text[0]);
                    int col = int.Parse(text[1]);
                    viewModel.MatrixFirstRandom(row, col);
                    RandBox1.Text = "";
                }
                else
                {
                    string[] text = RandBox2.Text.Split(' ', (char) StringSplitOptions.RemoveEmptyEntries);
                    int row = int.Parse(text[0]);
                    int col = int.Parse(text[1]);
                    viewModel.MatrixSecondRandom(row, col);
                    RandBox2.Text = "";
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            
        }

        private void Normal_Multiply(object sender, RoutedEventArgs e)
        {
            viewModel.NormalMultiply();
        }
        private void Strassen_Multiply(object sender, RoutedEventArgs e)
        {
            viewModel.StrassenMultiply();
        }
        private void Strassen_Vinograd_Multiply(object sender, RoutedEventArgs e)
        {
            viewModel.StrassenVinogradMultiply();
        }
        private void All_Multiplys(object sender, RoutedEventArgs e)
        {
            viewModel.AllMultiplys();
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

            MessageBox.Show(res);
        }
    }
}