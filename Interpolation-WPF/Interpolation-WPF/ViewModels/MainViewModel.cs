using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Interpolation_WPF.Models;
using Interpolation_WPF.Commands;
using Interpolation_WPF.Interpolation;

namespace Interpolation_WPF.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<PointInput> Points { get; } = new();
        public PointInput? SelectedPoint { get; set; }

        private List<Models.Point> mathPoints = new();

        public RelayCommand AddPointCommand { get; set; }
        public RelayCommand InterpolateCommand { get; set; }
        public RelayCommand EvaluateCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand ClearCommand { get; set; }


        private Polynom? ResultPolynom { get; set; }
        private string _interpolationResult = "";
        public string InterpolationResult
        {
            get => _interpolationResult;
            set
            {
                _interpolationResult = value;
                OnPropertyChanged(nameof(InterpolationResult));
            }
        }

        private string _lagrangeBases = string.Empty;
        public string LagrangeBases {
            get => _lagrangeBases;
            set
            {
                _lagrangeBases = value;
                OnPropertyChanged(nameof(LagrangeBases));
            } 
        }

        private string _newtonBases = string.Empty;
        public string NewtonBases { 
            get => _newtonBases;
            set 
            {
                _newtonBases = value;
                OnPropertyChanged(nameof(NewtonBases));
            }
           }

        private double? _evauationResult;
        public double? EvaluationResult
        {
            get => _evauationResult;
            set
            {
                _evauationResult = value;
                OnPropertyChanged(nameof(EvaluationResult));
            }
        }

        private double? _evaluationValue;
        public double? EvaluationValue
        {
            get => _evaluationValue;
            set
            {
                _evaluationValue = value;
                OnPropertyChanged(nameof(EvaluationValue));
            }
        }


        

        public MainViewModel()
        {
            AddPointCommand = new RelayCommand(AddPoint);
            InterpolateCommand = new RelayCommand(Interpolate);
            EvaluateCommand = new RelayCommand(Evaluate);
            DeleteCommand = new RelayCommand(DeletePoint);
            ClearCommand = new RelayCommand(ClearPoints);
        }



        private void AddPoint()
        {
            PointInput point = new PointInput();
            AddWindow window = new AddWindow(point);

            if (window.ShowDialog() == true)
            {
                Points.Add(point);
            }
        }

        private void DeletePoint()
        {
            if (SelectedPoint != null)
                Points.Remove(SelectedPoint);
        }

        private void ClearPoints()
        {
            Points.Clear();
        }


        public bool ValidatePoints(out string error)
        {
            error = string.Empty;

            if (Points.Count == 0)
            {
                error = "Add at least one point.";
                return false;
            }

            HashSet<double> uniqueX = new();

            for (int i = 0; i < Points.Count; i++)
            {
                PointInput point = Points[i];

                if (point == null)
                {
                    error = $"Point №{i + 1} does not exist.";
                    return false;
                }

                if (point.X == null || !double.IsFinite(point.X.Value))
                {
                    error = $"Point №{i + 1} has an incorrect value for X.";
                    return false;
                }

                if (!uniqueX.Add(point.X.Value))
                {
                    error = $"The X coordinate = {point.X} is duplicated.";
                    return false;
                }

                if (point.Derivatives == null || point.Derivatives.Count == 0)
                {
                    error = $"There is no function value at point №{i + 1}.";
                    return false;
                }

                for (int j = 0; j < point.Derivatives.Count; j++)
                {
                    var derivative = point.Derivatives[j];

                    if (derivative == null ||
                        derivative.Value == null ||
                        !double.IsFinite(derivative.Value.Value))
                    {
                        error = $"The {j}-th-order derivative for point №{i + 1} is missing.";
                        return false;
                    }

                    if (derivative.Order != j)
                    {
                        error = $"The order of the derivatives at point №{i + 1} is incorrect.";
                        return false;
                    }
                }
            }

            return true;
        }

        private void Interpolate()
        {
            ResultPolynom = null;
            InterpolationResult = string.Empty;
            LagrangeBases = string.Empty;
            NewtonBases = string.Empty;
            EvaluationResult = null;
            EvaluationValue = null;

            if (!ValidatePoints(out string error))
            {
                MessageBox.Show(error, "Input error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            mathPoints = Points.Select(p => p.ToMathPoint()).ToList();

            Lagrange lagrange = new Lagrange();
            Newton newton = new Newton();
            Hermit hermit = new Hermit();

            if (lagrange.CanInterpolate(mathPoints))
            {
                ResultPolynom = lagrange.Interpolate(mathPoints);
                InterpolationResult = ResultPolynom.Display();

                List<Polynom> lBases = lagrange.Lagrange_Bases(mathPoints);
                for(int i = 0; i < lBases.Count; i++)
                {
                    LagrangeBases += $"L{i}: " + lBases[i].Display() + "\n";
                }

                List<Polynom> nBases = newton.Newton_Bases(mathPoints);
                for (int i = 0; i < nBases.Count; i++)
                {
                    NewtonBases += $"N{i}: " + nBases[i].Display() + "\n";
                }
            }
            else if (hermit.CanInterpolate(mathPoints)) 
            {
                ResultPolynom = hermit.Interpolate(mathPoints);
                InterpolationResult = ResultPolynom.Display();
            }
            else
            {
                MessageBox.Show("Interpolation is not possible.");
                return;
            }
                

        }

        private void Evaluate()
        {
            if (ResultPolynom == null || EvaluationValue == null)
                return;

            EvaluationResult = ResultPolynom.Evaluate(EvaluationValue.Value);
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
