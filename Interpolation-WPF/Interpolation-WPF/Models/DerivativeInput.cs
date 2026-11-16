using System;
using System.Collections.Generic;
using System.Text;

namespace Interpolation_WPF.Models
{
    public class DerivativeInput
    {
        public int Order { get; set; }
        public string Name { get; set; }
        public double? Value { get; set; }

        public DerivativeInput(int order)
        {
            Order = order;
            Name = order switch
            {
                0 => "f(x)",
                1 => "f'(x)",
                2 => "f''(x)",
                _ => $"f{new string('\'', order)}(x)"
            };
            Value = null;
        }

    }
}
