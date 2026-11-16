using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Interpolation_WPF.Models
{
    public class CoefficientInput
    {
        public int Order { get;}
        public string Name { get;}
        public double? Value { get; set; }

        public CoefficientInput(int order)
        {
            string digits = "⁰¹²³⁴⁵⁶⁷⁸⁹";
            Order = order;
            Name = "x" + string.Concat(order.ToString().Select(c => digits[c - '0']));
        }
    }
}
