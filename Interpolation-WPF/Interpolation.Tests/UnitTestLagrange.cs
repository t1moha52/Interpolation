using Interpolation_WPF.Models;
using Interpolation_WPF.Interpolation;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Sdk;

namespace Interpolation.Tests
{
    public class UnitTestLagrange
    {
        [Fact]
        public void TestStart()
        {
            List<double> xs = new List<double>() { 1, 2, 3 };
            List<double> ys = new List<double>() { 5, 0, -4 };

            Polynom result = new Polynom();

            Lagrange engine = new();

            result = engine.Interpolate(xs, ys);

            Polynom expected = new Polynom(new List<double>() { 11, -6.5, 0.5 });

            Assert.Equal(expected.Coefficients, result.Coefficients);
        }

        [Fact]
        public void TestStartValue()
        {
            List<double> xs = new List<double>() { 1, 2, 3 };
            List<double> ys = new List<double>() { 5, 0, -4 };

            Polynom result = new Polynom();

            Lagrange engine = new();

            result = engine.Interpolate(xs, ys);

            Assert.Equal(5, result.Evaluate(1));
            Assert.Equal(0, result.Evaluate(2));
            Assert.Equal(-4, result.Evaluate(3));
        }

        [Fact]
        public void TestLinearInterpolation()
        {
            List<double> xs = new List<double>() { 0, 2, 4 };
            List<double> ys = new List<double>() { 1, 5, 9 };

            Lagrange engine = new();
            Polynom result = engine.Interpolate(xs, ys);

            Assert.Equal(1, result.Evaluate(0));
            Assert.Equal(5, result.Evaluate(2));
            Assert.Equal(9, result.Evaluate(4));
        }

        [Fact]
        public void TestConstantInterpolation()
        {
            List<double> xs = new List<double>() { -3, 0, 2, 7 };
            List<double> ys = new List<double>() { 4, 4, 4, 4 };

            Lagrange engine = new();
            Polynom result = engine.Interpolate(xs, ys);

            Assert.Equal(4, result.Evaluate(-3));
            Assert.Equal(4, result.Evaluate(0));
            Assert.Equal(4, result.Evaluate(7));
        }
    }
}
