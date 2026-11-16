using Interpolation_WPF.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Interpolation_WPF
{
    /// <summary>
    /// Interaction logic for AddPolynomWindow.xaml
    /// </summary>
    public partial class AddPolynomWindow : Window
    {
        private PolynomInput NewPolynom {  get; set; }
        public AddPolynomWindow(PolynomInput polynom)
        {
            NewPolynom = polynom;
            InitializeComponent();
            DataContext = NewPolynom;
        }

        private void Button_Add_Coefficient(object sender, RoutedEventArgs e)
        {
            NewPolynom.AddCoefficient();
        }

        private void Button_Remove_Coefficient(object sender, RoutedEventArgs e)
        {
            NewPolynom.RemoveCoefficient();
        }

        private void Button_Save_Coefficient(object sender, RoutedEventArgs e)
        {
            if (NewPolynom.Coefficients.Count == 0)
            {
                MessageBox.Show("Add at least one coefficient");
                return;
            }

            if (NewPolynom.Coefficients.Any(c => c.Value == null))
            {
                MessageBox.Show("Fill out all coefficients");
                return;
            }

            if (NewPolynom.Coefficients.Any(c =>
                !double.IsFinite(c.Value!.Value)))
            {
                MessageBox.Show("All coefficients must be finite numbers");
                return;
            }

            DialogResult = true;
            Close();
        }
    }
}
