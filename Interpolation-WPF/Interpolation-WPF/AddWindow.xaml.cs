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
using Interpolation_WPF.Models;

namespace Interpolation_WPF
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public PointInput pointInput { get; set; }
        public AddWindow(PointInput point)
        {
            pointInput = point;
            InitializeComponent();
            DataContext = pointInput;
        }

        private void Button_Add_Derivative(object sender, RoutedEventArgs e)
        {
            pointInput.AddDerivative();
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            if (pointInput.Derivatives.Count > 0)
            {
                pointInput.Derivatives.RemoveAt(pointInput.Derivatives.Count - 1);
            }
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            if (pointInput.X == null)
            {
                MessageBox.Show("Enter the X");
                return;
            }

            if (pointInput.Derivatives.Count == 0 ||
                pointInput.Derivatives.Any(d => d.Value == null))
            {
                MessageBox.Show("Fill out all derivatives");
                return;
            }

            DialogResult = true;
            Close();
        }

        
    }
}
