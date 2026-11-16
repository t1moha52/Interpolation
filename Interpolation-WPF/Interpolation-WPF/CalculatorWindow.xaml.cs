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
using Interpolation_WPF.ViewModels;

namespace Interpolation_WPF
{
    /// <summary>
    /// Interaction logic for CalculatorWindow.xaml
    /// </summary>
    public partial class CalculatorWindow : Window
    {
        
        public CalculatorWindow()
        {
            InitializeComponent();
            DataContext = new CalculatorViewModel();
        }
    }
}
