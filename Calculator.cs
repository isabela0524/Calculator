using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFcalculator
{
    public class Calculator
    {
        private double number;
        private string input = "";
        private double memory = 0;
        private string operation;
        private double currentValue = 0;
        private string lastOperator = "";
        public string CurrentBase { get; set; } = "DEC";

        public string Input => input;

        public void AppendNumber(string number)
        {
            if (input == "0")
                input = number;
            else
                input = input + number;
        }

        public void Decimal()
        {
            if(!input.Contains("."))
            {
                if(string.IsNullOrEmpty(input))
                {
                    input = "0.";
                }
                else
                {
                    input += ".";
                }
            }
        }

        public void SetOperation(string op)
        {
            try
            {
                if (!string.IsNullOrEmpty(lastOperator))
                {
                   input= Calculate();
                    currentValue=ParseInput(input);
                  
                }
                else
                {
                    currentValue=ParseInput(input);
                }

                lastOperator = op switch
                {
                    "x" => "*",
                    "÷"=>"/",
                    _=>op
                };
               
                input = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare în Calculator.SetOperation: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private long ParseInput(string input)
        {
            try
            {
                return CurrentBase switch
                {
                    "BIN" => Convert.ToInt32(input, 2),
                    "OCT" => Convert.ToInt32(input, 8),
                    "HEX" => Convert.ToInt32(input, 16),
                    _ => long.Parse(input)
                };
            }
            catch
            {
                throw new FormatException($"Număr invalid pentru baza {CurrentBase}: {input}");
            }
        }
        

        public string Calculate()
        {
          //  MessageBox.Show($"Calculate called! Input: {input}, CurrentValue: {currentValue}, LastOperator: {lastOperator}");
            try
            {
                if(string.IsNullOrEmpty(lastOperator))
                {
                    return input;
                }
                long newValue = ParseInput(input);

                switch (lastOperator)
                {
                    case "+":
                        currentValue += newValue;
                        break;
                    case "-":
                        currentValue -= newValue;
                        break;
                    case "*":
                        currentValue *= newValue;
                        break;
                    case "/":
                        if (newValue != 0)
                            currentValue /= newValue;
                        else
                            return "Eroare";
                        break;
                    default:
                        currentValue = newValue;
                        break;
                }

                lastOperator = "";
                string displayResult= CurrentBase switch
                {
                    "BIN" => Convert.ToString((int)currentValue, 2),
                    "OCT" => Convert.ToString((int)currentValue, 8),
                    "HEX" => Convert.ToString((int)currentValue, 16).ToUpper(),
                    _ => currentValue.ToString()
                };
                input= displayResult;
                return displayResult;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Eroare în Calculator.Calculate: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                return "Eroare";
            }

        }

        public string Square()
        {
            if(double.TryParse(input, out double number))
            {
                double result=Math.Pow(number, 2);
                input=result.ToString();
                return input;
            }
            return "Eroare!";
        }

        public string Reciprocal()
        {
            if(double.TryParse(input,out double number))
            {
                double result = 1 / number;
                input=result.ToString(); 
                return input;
            }
            return "Eroare!";
        }

        public string SquareRoot()
        {
            if( double.TryParse(input,out double number))
            {
                double result=Math.Sqrt(number);
                input=result.ToString();
                return input;
            }
            return "Eroare!";
        }

        public string Negate()
        {
            if(double.TryParse(input, out double number))
            {
                double result = (-1) * (number);
                input=result.ToString();
                return input;
            }
            return "Eroare!";
        }

        public void Clear()
        {
            input = "";
            number = 0;
            operation = "";
        }

        public void ClearEntry()
        {
            input = "";
        }

        public void MemoryClear()
        {
            memory = 0;
        }

        public string MemoryRecall()
        {
            input=memory.ToString();
            return input;
        }

        public void MemoryAdd()
        {
           double value=ParseInput(input);
            memory += value;
        }

        public void MemorySubtract()
        {
            double value = ParseInput(input);
            memory -= value;
        }

        public void MemoryStore()
        {
            memory=ParseInput(input);
        }

        public string MemoryView()
        {
             return memory.ToString();
        }

        public void Backspace()
        {
            if(!string.IsNullOrEmpty(input))
            {
                input=input.Substring(0, input.Length - 1);
            }
        }
    }

}
