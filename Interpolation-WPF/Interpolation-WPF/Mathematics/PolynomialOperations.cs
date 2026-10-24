using Interpolation_WPF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interpolation_WPF.Mathematics
{
    public static class PolynomialOperations
    {
        public static Polynom AddPol(Polynom polynom1, Polynom polynom2)
        {
            Polynom result = new();

            int length1 = polynom1.Coefficients.Count();
            int length2 = polynom2.Coefficients.Count();

            int maxLength = Math.Max(length1, length2);

            for (int i = 0; i < maxLength; i++)
            {
                double coefficient1 = i < length1 ? polynom1.Coefficients[i] : 0;
                double coefficient2 = i < length2 ? polynom2.Coefficients[i] : 0;

                result.Coefficients.Add(coefficient1 + coefficient2);
            }

            return result;
        }

        public static Polynom MultPol(Polynom pol1, Polynom pol2)
        {
            Polynom result = new Polynom();






            return result;
        }

    }
}
