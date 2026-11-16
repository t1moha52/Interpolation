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
                result = Math.Round(result * x + Coefficients[i - 1], 6);
            }
            return result;
        }

        public Polynom(List<double> list)
        {
            if (list != null)
                Coefficients = list;
        }

        public Polynom()
        {

        }

        private string ToSuperscript(int number)
        {
            string digits = "⁰¹²³⁴⁵⁶⁷⁸⁹";
            return string.Concat(number.ToString().Select(c => digits[c - '0']));
        }

        public string Display()
        {
            string result = string.Empty;

            for (int i = Coefficients.Count - 1; i >= 0; i--)
            {
                double coefficient = Coefficients[i];

                if (coefficient == 0)
                    continue;

                string term;

                if (i == 0)
                {
                    term = Math.Abs(coefficient).ToString();
                }
                else
                {
                    string variable = i == 1 ? "x" : $"x{ToSuperscript(i)}";

                    term = Math.Abs(coefficient) == 1
                        ? variable
                        : Math.Abs(coefficient) + variable;
                }

                if (result.Length == 0)
                    result += coefficient < 0 ? "-" + term : term;
                else
                    result += coefficient < 0 ? " - " + term : " + " + term;
            }

            return result.Length == 0 ? "0" : result;
        }
    }
}