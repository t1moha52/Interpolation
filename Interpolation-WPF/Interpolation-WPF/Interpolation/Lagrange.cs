using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Documents;
using Interpolation_WPF.Mathematics;
using Interpolation_WPF.Models;

namespace Interpolation_WPF.Interpolation
{
    public class Lagrange : IInterpolator
    {
        public bool CanInterpolate(List<Point> points)
        {
            if (points == null || points.Count == 0)
                return false;

            List<double> xs = new();
     
            foreach (Point point in points)
            {
                if (point.Y.Count != 1)
                    return false;

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

        public Polynom Interpolate(List<Point> points)
        {

            if (!CanInterpolate(points))
                throw new ArgumentException("Wrong input.");

            List<double> xs = new();
            List<double> ys = new();

            foreach (Point p in points)
            {
                xs.Add(p.X);
                ys.Add(p.Y[0]);
            }

            Polynom result = new Polynom(new List<double>() { 0 });

            int length = xs.Count;

            List<Polynom> Base = Lagrange_Bases(points);

            for (int i = 0; i < length; i++)
            {
                Polynom basis = new Polynom(Base[i].Coefficients.ToList());

                for(int k = 0; k < basis.Coefficients.Count(); k++)
                {
                    basis.Coefficients[k] = basis.Coefficients[k] * ys[i];
                }

                result = PolynomialOperations.AddPol(result, basis);
            }

            return result;
        }


        public List<Polynom> Lagrange_Bases(List<Point> points)
        {
            List<double> xs = new();
            foreach(Point p in points)
            {
                xs.Add(p.X);
            }

            if (xs.Distinct().Count() != xs.Count)
                throw new ArgumentException("Nodes must be distinct.");

            List<Polynom> Bases = new();

            int length = xs.Count;

            for (int i = 0; i < length; i++)
            {
                Polynom basis = new Polynom(new List<double>() { 1 });

                for (int j = 0; j < length; j++)
                {
                    if (i != j)
                    {
                        Polynom factor = new(new List<double> { -xs[j], 1 });

                        basis = PolynomialOperations.MultPol(basis, factor);
                    }
                }

                double lx = basis.Evaluate(xs[i]);
                for (int k = 0; k < basis.Coefficients.Count(); k++)
                {
                    basis.Coefficients[k] = basis.Coefficients[k] / lx;
                }

                Bases.Add(basis);
            }

            return Bases;
        }
    }
}
