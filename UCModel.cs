using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFcalculator
{
    public class UCModel : INotifyPropertyChanged
    {
        private readonly Calculator calc = new Calculator();
        private string displayText = "0";
        private HashSet<char> Digits = new HashSet<char>("0123456789ABCDEF");

        private bool isDigitGrouping=false;

        public UCModel()
        {
            Console.WriteLine("UCModel created");
        }
        public bool IsDigitGrouping
        {
            get{return isDigitGrouping;}
            set{
                isDigitGrouping=value;
                OnPropertyChanged(nameof(IsDigitGrouping));
                OnPropertyChanged(nameof(DisplayText));
            }
        }

        public string DisplayText
        {
            get
            {
                if(double.TryParse(displayText, System.Globalization.NumberStyles.AllowDecimalPoint | System.Globalization.NumberStyles.AllowThousands,
                    CultureInfo.InvariantCulture, out double number))
                {
                    if (number % 1 == 0)
                        return IsDigitGrouping
                            ? number.ToString("N0", CultureInfo.InvariantCulture)
                            : number.ToString(CultureInfo.InvariantCulture);
                    return number.ToString("G", CultureInfo.InvariantCulture);
                }
                return displayText;
            }
            set
            {
                displayText = value;
                OnPropertyChanged();
            }
        }

        private string selectedBase;

        public string SelectedBase
        {
            get { return selectedBase; }
            set
            {
                selectedBase = value;
                OnPropertyChanged(nameof(SelectedBase));
                UpdateDigits();
            }
        }

        public void Decimal()
        {
            calc.Decimal();
            DisplayText = calc.Input;
        }

        public void AppendNumber(string number)
        {
            calc.AppendNumber(number);
            DisplayText = calc.Input;
            OnPropertyChanged(nameof(DisplayText));
        }
        public void SetOperation(string op)
        {
            calc.SetOperation(op);
            DisplayText=calc.Input;
            OnPropertyChanged(nameof(DisplayText));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void Calculate()
        {
            DisplayText = calc.Calculate();
            OnPropertyChanged(nameof(DisplayText));
        }

        public void Square()
        {
            DisplayText=calc.Square();
        }

        public void SquareRoot()
        {
            DisplayText=calc.SquareRoot();
        }

        public void Negate()
        {
            DisplayText=calc.Negate(); 
        }

        public void Reciprocal()
        {
            DisplayText=calc.Reciprocal();
        }

        public void Clear()
        {
            calc.Clear();
            DisplayText = "0";
        }

        public void ClearEntry()
        {
            calc.ClearEntry();
            DisplayText = "0";
        }

        public void MemoryClear()
        {
            calc.MemoryClear();
        }

        public void MemoryRecall()
        {
            DisplayText=calc.MemoryRecall();
            OnPropertyChanged(nameof (DisplayText));
        }

        public void MemoryAdd()
        {
            calc.MemoryAdd();
        }

        public void MemorySubtract()
        {
            calc.MemorySubtract();
        }

        public void MemoryStore()
        {
            calc.MemoryStore();
        }

        public void MemoryView()
        {
            MessageBox.Show($"Valoare în memorie: {calc.MemoryView()}", "Memorie", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void Backspace()
        {
            calc.Backspace();
            DisplayText = calc.Input;
        }

        public bool IsDigitEnabled(string digit)
        {
            return Digits.Contains(digit[0]);
        }

        private void UpdateDigits()
        {
            switch (selectedBase)
            {
                case "BIN": Digits = new HashSet<char>("01"); 
                    break;
                case "OCT":Digits = new HashSet<char>("01234567");
                    break;
                case "DEC": Digits = new HashSet<char>("0123456789");
                    break;
                case "HEX": Digits = new HashSet<char>("0123456789ABCDEF");
                    break;
            }
            OnPropertyChanged(nameof(Digits));
        }

        public void SwitchBase(string baseType)
        {
            try
            {
                calc.CurrentBase = baseType;
                int nr;
                try
                {
                    nr = selectedBase switch

                    {
                        "BIN" => Convert.ToInt32(displayText, 2),
                        "OCT" => Convert.ToInt32(displayText, 8),
                        "HEX" => Convert.ToInt32(displayText, 16),
                        _ => int.Parse(displayText)
                    };
                }
                catch
                {
                    MessageBox.Show("Format invalid pentru conversie!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                DisplayText = baseType switch
                {
                    "BIN" => Convert.ToString(nr, 2),
                    "OCT" => Convert.ToString(nr, 8),
                    "DEC" => nr.ToString(),
                    "HEX" => Convert.ToString(nr, 16).ToUpper(),
                    _ => DisplayText
                };
                SelectedBase = baseType;
                UpdateDigits();
                OnPropertyChanged(nameof(DisplayText));
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Eroare la conversia bazei: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                DisplayText = "Error!";
            }
        }

        private void SetBase(string newBase)
        {
            SelectedBase = newBase;
        }

        public void Copy()
        {
            if(!string.IsNullOrEmpty(DisplayText))
            {
                Clipboard.SetText(DisplayText);
            }
        }

        public void Paste()
        {
            if(Clipboard.ContainsText())
            {
                string txt=Clipboard.GetText();
                if(int.TryParse(txt, out _))
                {
                    DisplayText=txt;
                }
            }
        }

        public void Cut()
        {
            if(!string.IsNullOrEmpty(DisplayText) )
            {
                Clipboard.SetText(DisplayText);
                DisplayText = "0";
            }
        }

        public void Equals_Click()
        {
            DisplayText = calc.Calculate();
            OnPropertyChanged(nameof(DisplayText) );
        }

    }
}
