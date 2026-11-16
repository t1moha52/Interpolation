using Interpolation_WPF.Commands;
using Interpolation_WPF.Mathematics;
using Interpolation_WPF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Permissions;
using System.Text;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Input;

namespace Interpolation_WPF.ViewModels
{
    internal class CalculatorViewModel : INotifyPropertyChanged
    {

        public PolynomInput PolynomInput1 { get; set; }
        public PolynomInput PolynomInput2 { get; set; }

        public Polynom? Polynom1 { get; set; }
        public Polynom? Polynom2 { get; set; }

        private string? _polynom1Display;
        public string? Polynom1Display
        {
            get => _polynom1Display;
            set
            {
                _polynom1Display = value;
                OnPropertyChanged(nameof(Polynom1Display));
            }
        }
        private string? _polynom2Display;
        public string? Polynom2Display
        {
            get => _polynom2Display;
            set
            {
                _polynom2Display = value;
                OnPropertyChanged(nameof(Polynom2Display));
            }
        }

        private string? _calculationResult;
        public string? CalculationResult
        {
            get => _calculationResult;
            set
            {
                _calculationResult = value;
                OnPropertyChanged(nameof(CalculationResult));
            }
        }

        public RelayCommand AddPol1Command { get; set; }
        public RelayCommand AddPol2Command { get; set; }
        public RelayCommand PolynomialAddition {  get; set; }
        public RelayCommand PolynomialMultiplication { get; set; }

        public CalculatorViewModel()
        {
            PolynomInput1 = new PolynomInput();
            PolynomInput2 = new PolynomInput();

            AddPol1Command = new RelayCommand(AddPolynom1);
            AddPol2Command = new RelayCommand(AddPolynom2);
            PolynomialAddition = new RelayCommand(Addition);
            PolynomialMultiplication = new RelayCommand(Multiplication);
        }

        private void AddPolynom1()
        {
            AddPolynomWindow window = new AddPolynomWindow(PolynomInput1);
            if (window.ShowDialog() == true)
            {
                CalculationResult = string.Empty;
                Polynom1 = PolynomInput1.ToPolynom();
                Polynom1Display = Polynom1.Display();
            }
        }

        private void AddPolynom2()
        {
            AddPolynomWindow window = new AddPolynomWindow(PolynomInput2);
            if (window.ShowDialog() == true)
            {
                CalculationResult = string.Empty;
                Polynom2 = PolynomInput2.ToPolynom();
                Polynom2Display = Polynom2.Display();
            }
        }

        private void Addition()
        {
            if(Polynom1 != null && Polynom2 != null)
                CalculationResult = PolynomialOperations.AddPol(Polynom1, Polynom2).Display();
            else
                MessageBox.Show("Fill out all Polynomes");
        }

        private void Multiplication()
        {
            if (Polynom1 != null && Polynom2 != null)
                CalculationResult = PolynomialOperations.MultPol(Polynom1, Polynom2).Display();
            else
                MessageBox.Show("Fill out all Polynomes");
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
