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
    /// Interaction logic for StandardUC.xaml
    /// </summary>
    public partial class StandardUC : UserControl
    {
        private readonly UCModel model=new UCModel();
        public StandardUC()
        {
            InitializeComponent();
            this.DataContext = model;
        }

        private void Menu_Cut_Click(object sender, RoutedEventArgs e)
        {
            model.Cut();
        }

        private void Menu_Copy_Click(object sender, RoutedEventArgs e)
        {
            model.Copy();
        }

        private void Menu_Paste_Click(object sender, RoutedEventArgs e)
        {
            model.Paste();
        }

        private void Menu_About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Standard Calculator v1.0\nDeveloped by Ciutacu Elena-Isabela", "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void Number_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            model.AppendNumber(button.Content.ToString());
        }

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            model.Backspace();
        }
        private void Operator_Click(object sender, RoutedEventArgs e)
        {
           var button=sender as System.Windows.Controls.Button;
            model.SetOperation(button.Content.ToString());
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            model.Clear();
        }

        private void ClearEntry_Click(object sender, RoutedEventArgs e)
        {
            model.ClearEntry();
        }

        private void Reciprocal_Click(object sender, RoutedEventArgs e)
        {
            model.Reciprocal();
        }

        private void Square_Click(object sender, RoutedEventArgs e)
        {
           model.Square();
        }

        private void SquareRoot_Click(object sender, RoutedEventArgs e)
        {
            model.SquareRoot();
        }

        private void Negate_Click(object sender, RoutedEventArgs e)
        {
           model.Negate();
        }

        private void Decimal_Click(object sender, RoutedEventArgs e)
        {
            model.Decimal();
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

        private void Digit_Grouping_Click(object sender, RoutedEventArgs e)
        {
            model.IsDigitGrouping = !model.IsDigitGrouping;
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                model.Calculate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
