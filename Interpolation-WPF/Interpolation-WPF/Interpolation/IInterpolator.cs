using Interpolation_WPF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interpolation_WPF.Interpolation
{
    internal interface IInterpolator
    {
        public bool CanInterpolate(List<Point> points);
        public Polynom Interpolate(List<Point> points);

    }
}
