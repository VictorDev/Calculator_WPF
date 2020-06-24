using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp3
{
    enum Operation { Add, Div, Sub, Mul, Sqrt, Pow, Tan, Log, Sin, Cos, Interest }

    delegate void CalculatorDidUpdateOutput(Calculator sender, double value, int precision);

    class Calculator
    {
        double? left = null;
        double? right = null;
        Operation? currentOp = null;
        bool decimalPoint = false;
        int precision = 0;

        public event CalculatorDidUpdateOutput DidUpdateValue;
        public event EventHandler<string> InputError;
        public event EventHandler<string> CalculationError;

        public void AddDigit(int digit)
        {
            if (left.HasValue && Math.Log10(left.Value) > 10 || precision > 13)
            {
                InputError?.Invoke(this, "Input overflow");
                return;
            }
            if (precision > 10)
            {
                InputError?.Invoke(this, "Input overflow");
                return;
            }
            if (!decimalPoint)
            {
                if (left >= 0 || left==null)
                {
                    left = (left ?? 0) * 10 + digit;
                }
                else {
                    left = (left ?? 0) * 10 - digit;
                }
            }
            else
            {
                precision += 1;
                left = left + (Math.Pow(0.1, precision) * digit);
            }
            DidUpdateValue?.Invoke(this, left.Value, precision);
        }

        public void AddDecimalPoint()
        {
            if (!left.HasValue)
            {
                left = 0;
            }
            decimalPoint = true;
        }

        public void AddOperation(Operation op)
        {
            if (left.HasValue && currentOp.HasValue)
            {
                Compute();

            }
            if (!right.HasValue && !(op == Operation.Sqrt || op == Operation.Cos || op == Operation.Log || op == Operation.Tan || op == Operation.Sin))
            {
                right = left;
                left = 0;
                precision = 0;
                decimalPoint = false;
                DidUpdateValue.Invoke(this, left.Value, precision);
                currentOp = op;
            }
            if (left.HasValue && op == Operation.Sqrt || op == Operation.Cos || op == Operation.Log || op == Operation.Tan || op == Operation.Sin || op == Operation.Interest)
            {
                currentOp = op;
                ComputeUnar();
                currentOp = null;
            }
            currentOp = op;

        }

        public void ComputeUnar()
        {
            switch (currentOp)
            {
                
                case Operation.Sqrt:
                    if (left < 0)
                    {
                        InputError?.Invoke(this, "Error");
                        return;
                    }
                    if (left != null)
                        right = Math.Sqrt((double)left);
                    else right = Math.Sqrt((double)right);
                    break;

                case Operation.Log:
                    if (left < 0)
                    {
                        InputError?.Invoke(this, "Error");
                        return;
                    }
                    if (left != null)
                        right = Math.Log((double)left);
                    else right = Math.Log((double)right);

                    break;

                case Operation.Sin:
                    if (left != null)
                        right = Math.Sin((double)left);
                    else right = Math.Cos((double)right);
                    
                    break;

                case Operation.Cos:
                    if(left!=null)
                        right = Math.Cos((double)left);
                    else right = Math.Cos((double)right);
                    break;

                case Operation.Tan:
                    if (left != null)
                        right = Math.Tan((double)left);
                    else right = Math.Tan((double)right);
                    break;

                case Operation.Interest:
                    right = (double)right / 100;
                    break;
            }
            left = right;
            DidUpdateValue?.Invoke(this, right.Value, precision);
            right = null;
        }
        public void Sign()
        {
            if (left != null)
            {
                left = left * (-1);
                DidUpdateValue?.Invoke(this, left.Value, precision);
            }
            else {
                right = right * (-1);
                DidUpdateValue?.Invoke(this, right.Value, precision);
            }
        }

        public void Compute()
        {
            switch (currentOp)
            {
                case Operation.Add:
                    right = left + right;
                    left = null;
                    break;

                case Operation.Sub:
                    right = right - left;
                    left = null;
                    break;

                case Operation.Mul:
                    right = left * right;
                    left = null;
                    break;

                case Operation.Div:
                    if (left == 0)
                    {
                        CalculationError?.Invoke(this, "Division by 0!");
                        return;
                    }
                    right = right / left;
                    left = null;
                    break;

                case Operation.Pow:
                    right = Math.Pow((double)right, (double)left);
                    left = null;
                    break;
                default:
                    currentOp = null;
                    precision = 0;
                    decimalPoint = false;
                    return;
            }
            left = right;
            DidUpdateValue?.Invoke(this, right.Value, precision);
            right = null;
            currentOp = null;
        }

        public void Clear()
        {
            left = 0;
            DidUpdateValue?.Invoke(this, left.Value, precision);
        }
        public void Reset()
        {
            currentOp = null;
            left = 0;
            right = null;
            precision = 0;
            decimalPoint = false;
            DidUpdateValue?.Invoke(this, left.Value, 0);
        }
        public void ClearSimbol()
        {
            if (decimalPoint)
            {
                left = left - (left * Math.Pow(10, precision - 1) % 1) * Math.Pow(0.1, precision - 1);
                precision--;
                if (precision == 0)
                    decimalPoint = false;
            }
            else
            {
                left = (int)(left * 0.1);
            }
            DidUpdateValue?.Invoke(this, left.Value, precision);
        }
    }
}