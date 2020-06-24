using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        Calculator calc;
        Thickness onMouseDown;
        Thickness onMouseUp;
        public MainWindow()
        {
            InitializeComponent();
            calc = new Calculator();
            calc.DidUpdateValue += calc_DidUpdate;
            calc.InputError += calc_Error;
            calc.CalculationError += calc_Error;
            onMouseDown = new Thickness(3);
            onMouseUp = new Thickness(1);
        }

        private void calc_Error(object sender, string e)
        {
            MessageBox.Show(e, "Calculator Error",MessageBoxButton.OK,MessageBoxImage.Warning);
        }

        private void calc_DidUpdate(Calculator sender, double value, int precision)
        {
            Result_Text_Block.Text = $"{value}";
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string text = (sender as Button).Content.ToString();
            string name = (sender as Button).Name;
            int digit;
            if (int.TryParse(text, out digit))
            {
                calc.AddDigit(digit);
            }
            else
            {
                switch (name)
                {
                    case "dec":
                        calc.AddDecimalPoint();
                        break;
                    case "evaluate":
                        calc.Compute();
                        break;
                    case "addition":
                        calc.AddOperation(Operation.Add);
                        break;
                    case "substraction":
                        calc.AddOperation(Operation.Sub);
                        break;
                    case "multiplication":
                        calc.AddOperation(Operation.Mul);
                        break;
                    case "division":
                        calc.AddOperation(Operation.Div);
                        break;
                    case "clear":
                        calc.Clear();
                        break;
                    case "reset":
                        calc.Reset();
                        break;
                    case "back":
                        calc.ClearSimbol();
                        break;
                    case "pow":
                        calc.AddOperation(Operation.Pow);
                        break;
                    case "sqrt":
                        calc.AddOperation(Operation.Sqrt);
                        break;
                    case "log":
                        calc.AddOperation(Operation.Log);
                        break;
                    case "tan":
                        calc.AddOperation(Operation.Tan);
                        break;
                    case "Cos":
                        calc.AddOperation(Operation.Cos);
                        break;
                    case "Sin":
                        calc.AddOperation(Operation.Sin);
                        break;
                    case "inter":
                        calc.AddOperation(Operation.Interest);
                        break;
                    case "sign":
                        calc.Sign();
                        break;
                    case "exit":
                        this.Close();
                        break;
                }
            }
        }

        private void Mouse_Down(object sender, MouseButtonEventArgs e)
        {
            (sender as Button).BorderThickness = onMouseDown;
        }

        private void Mouse_Up(object sender, MouseButtonEventArgs e)
        {
            (sender as Button).BorderThickness = onMouseUp;
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D1:
                case Key.NumPad1:
                    calc.AddDigit(1);
                    break;
                case Key.D2:
                case Key.NumPad2:
                    calc.AddDigit(2);
                    break;
                case Key.D3:
                case Key.NumPad3:
                    calc.AddDigit(3);
                    break;
                case Key.D4:
                case Key.NumPad4:
                    calc.AddDigit(4);
                    break;
                case Key.D5:
                case Key.NumPad5:
                    calc.AddDigit(5);
                    break;
                case Key.D6:
                case Key.NumPad6:
                    calc.AddDigit(6);
                    break;
                case Key.D7:
                case Key.NumPad7:
                    calc.AddDigit(7);
                    break;
                case Key.D8:
                case Key.NumPad8:
                    calc.AddDigit(8);
                    break;
                case Key.D9:
                case Key.NumPad9:
                    calc.AddDigit(9);
                    break;
                case Key.D0:
                case Key.NumPad0:
                    calc.AddDigit(0);
                    break;
                case Key.Add:
                    calc.AddOperation(Operation.Add);
                    break;
                case Key.Back:
                    calc.ClearSimbol();
                    break;
                case Key.Decimal:
                    calc.AddDecimalPoint();
                    break;
                case Key.Delete:
                    calc.Clear();
                    break;
                case Key.Divide:
                    calc.AddOperation(Operation.Div);
                    break;
                case Key.Multiply:
                    calc.AddOperation(Operation.Mul);
                    break;
                case Key.Subtract:
                    calc.AddOperation(Operation.Sub);
                    break;
                case Key.Enter:
                    calc.Compute();
                    break;

            }
        }
    }
}
