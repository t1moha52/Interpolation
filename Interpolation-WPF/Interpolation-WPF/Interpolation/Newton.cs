using Interpolation_WPF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interpolation_WPF.Interpolation
{
    public class Newton : IInterpolator
    {
        public bool CanInterpolate(List<Point> points)
        {
            return false;
        }

        public Polynom Interpolate(List<Point> points)
        {
            Polynom result = new();



            return result;
        }
    }
}
