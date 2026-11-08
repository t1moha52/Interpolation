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

        public List<double> DD_Coefficients(List<Point> points)
        {
            List<double> coefficients = new();

            int length = points.Count();

            for (int i = 1; i <= length; i++)
            {
                coefficients.Add(coef(points[..i]));
            }

            return coefficients;
        }

        public double coef(List<Point> points)
        {
            if (points.Count == 1)
                return points[0].Y[0];

            return (coef(points[1..]) - coef(points[..^1])) / (points[^1].X - points[0].X);
        }

        public Polynom Interpolate(List<Point> points)
        {
            if (!CanInterpolate(points))
                throw new ArgumentException("Wrong input");

            Polynom result = new Polynom(new List<double> { 0 });
            List<double> coefficients = DD_Coefficients(points);
            List<Polynom> bases = Newton_Bases(points);

            int length = points.Count();
            for (int i = 0; i < length; i++)
            {
                Polynom local = new Polynom(new List<double>() { coefficients[i] });
                local = PolynomialOperations.MultPol(local, bases[i]);

                result = PolynomialOperations.AddPol(result, local);
            }

            return result;
        }
    }
}
