using System;
using System.Collections.Generic;
using System.Text;

namespace Interpolation_WPF.Models
{
    public class Point
    {
        public double X { get; set; }

        public List<double> Y { get; set; } = new List<double>();

        public Point(double x, double y)
        {
            X = x;
            Y.Add(y);
        }

        public Point(double x, List<double> y)
        {
            X = x;
            Y = y;
        }
    }
}
