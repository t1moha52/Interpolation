using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Interpolation_WPF.Mathematics;
using Interpolation_WPF.Models;

namespace Interpolation_WPF.Interpolation
{
    public class Hermit : IInterpolator
    {
        public bool CanInterpolate(List<Point> points)
        {
            if (points == null || points.Count == 0)
                return false;

            List<double> xs = new();

            bool hasDerivative = false;

            foreach (Point point in points)
            {
                if (point == null || point.Y == null || point.Y.Count == 0)
                    return false;

                if (double.IsNaN(point.X) || double.IsInfinity(point.X))
                    return false;

                if (point.Y.Count > 1)
                    hasDerivative = true;

                foreach (double y in point.Y)
                {
                    if (double.IsNaN(y) || double.IsInfinity(y))
                        return false;
                }
                xs.Add(point.X);
            }

            if (!hasDerivative)
                return false;

            if (xs.Distinct().Count() != xs.Count)
                return false;

            return true;
        }

        public List<double> ExpandNodes(List<Point> points)
        {
            List<double> result = new List<double>();
            foreach (Point point in points)
            {
                foreach(double y in point.Y)
                {
                    result.Add(point.X);
                }
            }

            return result;
        }

        public List<double> InitialValues(List<Point> points)
        {
            List<double> result = new();

            foreach (Point point in points)
            {
                foreach (double y in point.Y)
                {
                    result.Add(point.Y[0]);
                }
            }

            return result;
        }

        public List<double> Derivatives(List<Point> points)
        {
            List<double> result = new();

            foreach (Point point in points)
            {
                foreach (double y in point.Y)
                {
                    result.Add(y);
                }
            }

            return result;
        }

        public double coef(List<double> xs, List<double> ys, List<double> derivatives, int i, int j)
        {
            if (i == j)
                return ys[i];

            if (xs[i] == xs[j])
            {
                return derivatives[j] / Factorial(j - i);
            }
            else
            {
                return (coef(xs, ys, derivatives, i + 1, j) - coef(xs, ys, derivatives, i, j - 1)) / (xs[j] - xs[i]);
            }
        }

        static long Factorial(int n)
        {
            if (n <= 1)
                return 1;

            return n * Factorial(n - 1);
        }

        public List<double> DD_Coefficients(List<Point> points)
        {
            List<double> result = new();

            List<double> xs = ExpandNodes(points);
            List<double> ys = InitialValues(points);
            List<double> derivatives = Derivatives(points);

            for(int j = 0; j < xs.Count; j++)
            {
                result.Add(coef(xs, ys, derivatives, 0, j));
            }

            return result;
        }

        public List<Polynom> Bases(List<Point> points)
        {
            List<Polynom> bases = new List<Polynom>();
            List<double> xs = ExpandNodes(points);

            for (int i = 0; i< xs.Count; i++)
            {
                Polynom local = new Polynom(new List<double>(){ 1 });

                for(int j = 0; j < i; j++)
                {
                    local = PolynomialOperations.MultPol(local, new Polynom(new List<double> { -xs[j], 1 }));
                }

                bases.Add(local);
            }

            return bases;
        }

        public Polynom Interpolate(List<Point> points)
        {
            Polynom result = new( new List<double>() { 0 });

            List<Polynom> bases = Bases(points);
            List<double> coefficients = DD_Coefficients(points);

            for(int i = 0; i < coefficients.Count; i++)
            {
                result = PolynomialOperations.AddPol(
                    result,
                    PolynomialOperations.MultPol(
                        new Polynom(new List<double>() { coefficients[i] }),
                        bases[i]));
            }

            return result;
        }

    }
}
