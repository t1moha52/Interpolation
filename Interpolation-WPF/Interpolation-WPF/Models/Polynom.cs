using System;
using System.Collections.Generic;
using System.Text;

namespace Interpolation_WPF.Models
{
    public class Polynom
    {
        // Ein Polynom wird durch eine Liste seiner Koeffizienten dargestellt.
        // Die Koeffizienten werden in aufsteigender Reihenfolge der Potenzen gespeichert.
        // Der Index eines Koeffizienten entspricht der Potenz von x, zu der er gehört.

        public List<double> Coefficients { get; set; } = new();

        public double Evaluate(double x)
        {
            double result = Coefficients[^1];

            for(int i = Coefficients.Count()-1; i > 0; i--)
            {
                result = result * x + Coefficients[i - 1];
            }
            return result;
        }
    }
}