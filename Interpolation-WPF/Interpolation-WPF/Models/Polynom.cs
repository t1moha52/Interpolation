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

    }
}
