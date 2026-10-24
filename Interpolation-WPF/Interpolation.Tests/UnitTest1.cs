using System.Collections.Generic;
using Interpolation_WPF.Models;
using Interpolation_WPF.Mathematics;
using Xunit;



namespace Interpolation.Tests
{
    public class UnitTest1
    {
        public class EngineTests
        {

            [Fact]
            public void TestEvaluate1()
            {
                Polynom test = new()
                {
                    Coefficients = new List<double> { -1, 2, 0, 5, 2 }
                };

                double result = test.Evaluate(2);

                Assert.Equal(75, result);
            }

            [Fact]
            public void TestEvaluate2()
            {
                Polynom test = new()
                {
                    Coefficients = new List<double> { -1, 2, 0, 5, 2 }
                };

                double result = test.Evaluate(0);

                Assert.Equal(-1, result);
            }


            [Fact]
            public void TestAddPol()
            {
                Polynom p1 = new()
                {
                    Coefficients = new List<double> { 2, 3, 4 }
                };

                Polynom p2 = new()
                {
                    Coefficients = new List<double> { 5, 2 }
                };

                Polynom result = PolynomialOperations.AddPol(p1, p2);

                Assert.Equal(
                    new double[] { 7, 5, 4 },
                    result.Coefficients
                );
            }

            [Fact]

            public void TestMultPol()
            {

            }
        }
    }
}
