using System;
using System.Collections.Generic;
using System.Text;

namespace Interpolation_WPF.Models
{
    public class Engine 
    {

        public Engine()
        {

        }

        public Polynom AddPol(Polynom polynom1, Polynom polynom2)
        {
            Polynom result = new();

            int length1 = polynom1.Coefficients.Count();
            int length2 = polynom2.Coefficients.Count();

            int maxLength = Math.Max(length1 , length2);

            for (int i = 0; i < maxLength; i++)
            {
                double coefficient1 = i < length1 ? polynom1.Coefficients[i] : 0;
                double coefficient2 = i < length2 ? polynom2.Coefficients[i] : 0;

                result.Coefficients.Add(coefficient1 + coefficient2);
            }

            return result;
        }

        protected Polynom MultPol(Polynom polynom1, Polynom polynom2)
        {
            Polynom result = new();
            return result;
        }
    }
}
