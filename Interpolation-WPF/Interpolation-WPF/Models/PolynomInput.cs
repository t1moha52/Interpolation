using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Interpolation_WPF.Models
{
    public class PolynomInput
    {
        public ObservableCollection<CoefficientInput> Coefficients { get; } = new();

        public PolynomInput()
        {
            AddCoefficient();
        }

        public void AddCoefficient()
        {
            Coefficients.Add(new CoefficientInput(Coefficients.Count));
        }

        public void RemoveCoefficient()
        {
            if (Coefficients.Count > 0)
                Coefficients.RemoveAt(Coefficients.Count - 1);
        }

        public Polynom ToPolynom()
        {
            List<double> coefficients = Coefficients.Select(c => c.Value!.Value).ToList();

            return new Polynom(coefficients);
        }
    }
}
