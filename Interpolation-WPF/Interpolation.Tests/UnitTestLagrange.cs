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
            List<Point> points = new();
            for(int i = 0; i < xs.Count; i++)
            {
                points.Add(new Point(xs[i], ys[i]));
            }

            Polynom result = new Polynom();

            Lagrange engine = new();

            result = engine.Interpolate(points);

            Polynom expected = new Polynom(new List<double>() { 11, -6.5, 0.5 });

            Assert.Equal(expected.Coefficients, result.Coefficients);
        }

        [Fact]
        public void TestStartValue()
        {
            List<double> xs = new List<double>() { 1, 2, 3 };
            List<double> ys = new List<double>() { 5, 0, -4 };
            List<Point> points = new();
            for (int i = 0; i < xs.Count; i++)
            {
                points.Add(new Point(xs[i], ys[i]));
            }

            Polynom result = new Polynom();

            Lagrange engine = new();

            result = engine.Interpolate(points);

            Assert.Equal(5, result.Evaluate(1));
            Assert.Equal(0, result.Evaluate(2));
            Assert.Equal(-4, result.Evaluate(3));
        }

        [Fact]
        public void TestLinearInterpolation()
        {
            List<double> xs = new List<double>() { 0, 2, 4 };
            List<double> ys = new List<double>() { 1, 5, 9 };
            List<Point> points = new();
            for (int i = 0; i < xs.Count; i++)
            {
                points.Add(new Point(xs[i], ys[i]));
            }

            Lagrange engine = new();
            Polynom result = engine.Interpolate(points);

            Assert.Equal(1, result.Evaluate(0));
            Assert.Equal(5, result.Evaluate(2));
            Assert.Equal(9, result.Evaluate(4));
        }

        [Fact]
        public void TestConstantInterpolation()
        {
            List<double> xs = new List<double>() { -3, 0, 2, 7 };
            List<double> ys = new List<double>() { 4, 4, 4, 4 };
            List<Point> points = new();
            for (int i = 0; i < xs.Count; i++)
            {
                points.Add(new Point(xs[i], ys[i]));
            }

            Lagrange engine = new();
            Polynom result = engine.Interpolate(points);

            Assert.Equal(4, result.Evaluate(-3));
            Assert.Equal(4, result.Evaluate(0));
            Assert.Equal(4, result.Evaluate(7));
        }

        [Fact]
        public void Lagrange_Bases_ReturnsCorrectBasisPolynomials()
        {
            List<double> xs = new() { 5, 1, 2 };
            List<Point> points = new();
            for (int i = 0; i < xs.Count; i++)
            {
                points.Add(new Point(xs[i], 0));
            }

            Lagrange interpolation = new Lagrange();

            List<Polynom> bases = interpolation.Lagrange_Bases(points);

            Assert.Equal(3, bases.Count);

            for (int i = 0; i < xs.Count; i++)
            {
                for (int j = 0; j < xs.Count; j++)
                {
                    double expected = (i == j) ? 1.0 : 0.0;
                    double actual = Math.Round(bases[i].Evaluate(xs[j]), 3);

                    Assert.Equal(expected, actual);
                }
            }
        }
    }
}
