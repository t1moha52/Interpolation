using System.Collections.Generic;
using Interpolation_WPF.Models;
using Interpolation_WPF.Mathematics;
using Xunit;


namespace Interpolation.Tests
{
    public class UnitTest1
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
        public void TestAddPol1()
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
        public void TestAddPolResultZero()
        {
            Polynom p1 = new()
            {
                Coefficients = new List<double> { 2, 3, 4 }
            };

            Polynom p2 = new()
            {
                Coefficients = new List<double> { -2, -3, -4 }
            };

            Polynom result = PolynomialOperations.AddPol(p1, p2);

            Assert.Equal(
                new double[] { 0, 0, 0},
                result.Coefficients
            );
        }

        [Fact]

        public void TestMultPol1()
        {
            Polynom p1 = new()
            {
                Coefficients = new List<double> { 1, 2, -3 }
            };
            Polynom p2 = new()
            {
                Coefficients = new List<double> { 2, -3 }
            };

            Polynom result = PolynomialOperations.MultPol(p1, p2);

            Assert.Equal(
                new double[] { 2, 1, -12, 9 },
                result.Coefficients);
        }

        [Fact]
        public void TestMultPolByConstant()
        {
            Polynom p1 = new()
            {
                Coefficients = new List<double> { 1, 2, 3 }
            };

            Polynom p2 = new()
            {
                Coefficients = new List<double> { 4 }
            };

            Polynom result = PolynomialOperations.MultPol(p1, p2);

            Assert.Equal(
                new double[] { 4, 8, 12 },
                result.Coefficients);
        }

        [Fact]
        public void TestMultPolByZero()
        {
            Polynom p1 = new()
            {
                Coefficients = new List<double> { 1, 2, 3 }
            };

            Polynom p2 = new()
            {
                Coefficients = new List<double> { 0 }
            };

            Polynom result = PolynomialOperations.MultPol(p1, p2);

            Assert.All(result.Coefficients, coefficient =>
                Assert.Equal(0, coefficient));
        }

        [Fact]
        public void TestMultPolDifferentDegrees()
        {
            Polynom p1 = new()
            {
                Coefficients = new List<double> { 2, 1 }
            };

            Polynom p2 = new()
            {
                Coefficients = new List<double> { 3, 0, 4 }
            };

            Polynom result = PolynomialOperations.MultPol(p1, p2);

            Assert.Equal(
                new double[] { 6, 3, 8, 4 },
                result.Coefficients);
        }
    }
}
