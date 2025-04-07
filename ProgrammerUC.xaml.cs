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

namespace WPFcalculator
{
    /// <summary>
    /// Interaction logic for ProgrammerUC.xaml
    /// </summary>
    public partial class ProgrammerUC : UserControl
    {
      private readonly UCModel model=new UCModel();


        public ProgrammerUC()
        {
            InitializeComponent();
            this.DataContext = model;
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            var button=sender as System.Windows.Controls.Button;
            model.AppendNumber(button.Content.ToString());
        }

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
           model.Backspace();
        }
       
        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = sender as System.Windows.Controls.Button;
                model.SetOperation(button.Content.ToString());
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Eroare: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                model.Calculate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erroare: {ex.Message}", "Erroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            model.Clear();
        }

        private void Bin_Click(object sender, RoutedEventArgs e)
        {
            model.SwitchBase("BIN");
            UpdateButton();
        }

        private void Oct_Click(object sender, RoutedEventArgs e)
        {
            model.SwitchBase("OCT");
            UpdateButton();
        }

        private void Dec_Click(object sender, RoutedEventArgs e)
        {
            model.SwitchBase("DEC");
            UpdateButton();
        }

        private void Hex_Click(object sender, RoutedEventArgs e)
        {
            model.SwitchBase("HEX");
            UpdateButton();
        }

        private void MemoryClear_Click(object sender, RoutedEventArgs e)
        {
            model.MemoryClear();
        }

        private void MemoryRecall_Click(object sender, RoutedEventArgs e)
        {
            model.MemoryRecall();
        }

        private void MemoryAdd_Click(object sender, RoutedEventArgs e)
        {
            model.MemoryAdd();
        }

        private void MemorySubtract_Click(object sender, RoutedEventArgs e)
        {
            model.MemorySubtract();
        }

        private void MemoryStore_Click(object sender, RoutedEventArgs e)
        {
            model.MemoryStore();
        }

        private void MemoryView_Click(object sender, RoutedEventArgs e)

        {
            model.MemoryView();
        }
        private void UpdateButton()
        {
            Button0.IsEnabled = model.IsDigitEnabled("0");
            Button1.IsEnabled = model.IsDigitEnabled("1");
            Button2.IsEnabled = model.IsDigitEnabled("2");
            Button3.IsEnabled = model.IsDigitEnabled("3");
            Button4.IsEnabled = model.IsDigitEnabled("4");
            Button5.IsEnabled = model.IsDigitEnabled("5");
            Button6.IsEnabled = model.IsDigitEnabled("6");
            Button7.IsEnabled = model.IsDigitEnabled("7");
            Button8.IsEnabled = model.IsDigitEnabled("8");
            Button9.IsEnabled = model.IsDigitEnabled("9");
            ButtonA.IsEnabled = model.IsDigitEnabled("A");
            ButtonB.IsEnabled = model.IsDigitEnabled("B");
            ButtonC.IsEnabled = model.IsDigitEnabled("C");
            ButtonD.IsEnabled = model.IsDigitEnabled("D");
            ButtonE.IsEnabled = model.IsDigitEnabled("E");
            ButtonF.IsEnabled = model.IsDigitEnabled("F");
        }

    }
}

