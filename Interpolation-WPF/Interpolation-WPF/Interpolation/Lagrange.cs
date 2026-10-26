using System;
using System.Collections.Generic;
using System.Text;
using Interpolation_WPF.Mathematics;
using Interpolation_WPF.Models;

namespace Interpolation_WPF.Interpolation
{
    public class Lagrange : IInterpolator
    {
        public Polynom Interpolate(List<double> xs, List<double> ys)
        {
            if (xs.Count != ys.Count)
                throw new ArgumentException("The number of nodes and values must match.");

            if (xs.Count == 0)
                throw new ArgumentException("At least one interpolation node is required.");

            if (xs.Distinct().Count() != xs.Count)
                throw new ArgumentException("Lagrange interpolation requires distinct nodes.");

            Polynom result = new Polynom(new List<double>() { 0 });

            int length = xs.Count;
            //List<Polynom> list = new();



            for (int i = 0; i < length; i++)
            {
                Polynom basis = new(new List<double>() { 1 });

                for (int j = 0; j < length; j++)
                {
                    if (i != j)
                    {
                        Polynom factor = new(new List<double> { -xs[j], 1 });

                        basis = PolynomialOperations.MultPol(basis, factor);
                    }
                }

                double lx = basis.Evaluate(xs[i]);

                //list.Add(basis);

                for(int k = 0; k < length; k++)
                {
                    basis.Coefficients[k] = basis.Coefficients[k] * ys[i] / lx;
                }

                result = PolynomialOperations.AddPol(result, basis);
            }

            //List<double> levaluated = new();

            //for (int i = 0; i < length; i++)
            //{
            //    double lx = list[i].Evaluate(xs[i]);
            //    levaluated.Add(lx);
            //}

            //for(int i = 0; i < length - 1; i++)
            //{
            //    //Polynom betweenstep = list[i];
            //    for(int j = 0; j < length; j++)
            //    {
            //        list[i].Coefficients[j] = list[i].Coefficients[j] / levaluated[i] * ys[i];
            //    }
            //    result = PolynomialOperations.AddPol(result, list[i]);
            //}



            return result;
        }
    }
}
