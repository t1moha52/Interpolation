using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Interpolation_WPF.Models
{
    public class PointInput
    {
        public double? X { get; set; }

        public ObservableCollection<DerivativeInput> Derivatives { get; set; }
            = new();

        public void AddDerivative()
        {
            Derivatives.Add(new DerivativeInput(Derivatives.Count));
            
        }

        public Point ToMathPoint()
        {
            return new Point(
                X!.Value,
                Derivatives.Select(d => d.Value!.Value).ToList()
            );
        }

        public PointInput()
        {
            AddDerivative();
        }
    }
}
