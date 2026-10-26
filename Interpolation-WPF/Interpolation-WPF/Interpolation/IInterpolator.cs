using Interpolation_WPF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interpolation_WPF.Interpolation
{
    internal interface IInterpolator
    {
        //string Name { get; }

        public Polynom Interpolate(List<double> xs, List<double> ys);

    }
}
