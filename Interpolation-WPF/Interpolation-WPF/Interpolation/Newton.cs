using Interpolation_WPF.Mathematics;
using Interpolation_WPF.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Interpolation_WPF.Interpolation
{
    public class Newton : IInterpolator
    {
        public bool CanInterpolate(List<Point> points)
        {

            if (points == null || points.Count == 0)
                return false;

            List<double> xs = new();

            foreach (Point point in points)
            {
                if (point == null || point.Y == null || point.Y.Count == 0)
                    return false;

                if (double.IsNaN(point.X) || double.IsInfinity(point.X))
                    return false;

                foreach (double y in point.Y)
                {
                    if (double.IsNaN(y) || double.IsInfinity(y))
                        return false;
                }
                xs.Add(point.X);

            }

            if (xs.Distinct().Count() != xs.Count)
                return false;

            return true;
        }

        public List<Polynom> Newton_Bases(List<Point> points)
        {
            List<Polynom> bases = new List<Polynom>() { new Polynom(new List<double>() { 1 }) };
            //bases.Add(new Polynom(new List<double>() { 1 }));


            foreach (Point point in points[..^1])
            {
                Polynom term = new Polynom(new List<double> { -point.X, 1 });

                bases.Add(PolynomialOperations.MultPol(bases[^1], term));
            }

            return bases;
        }

        public List<double> DD(List<Point> points)
        {
            List<double> Coefficients = new();

            int length = points.Count();

            for (int i = 1; i <= length; i++)
            {
                Coefficients.Add(coef(points[..i]));
            }

            return Coefficients;
        }

        public double coef(List<Point> points)
        {
            if (points.Count == 1)
                return points[0].Y[0];

            return (coef(points[1..]) - coef(points[..^1])) / (points[^1].X - points[0].X);
        }

        public Polynom Interpolate(List<Point> points)
        {
            Polynom result = new();
            List<double> Coefficients = DD(points);


            return result;
        }
    }
}
