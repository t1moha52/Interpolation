using Interpolation_WPF.Interpolation;
using Interpolation_WPF.Models;

using System;
using System.Collections.Generic;
using System.Text;

namespace Interpolation.Tests
{
    public class UnitTestNewton
    {
        [Fact]
        public void NewtonBases_Firts_Test()
        {
            List<Point> points = new()
            {
                new Point(1, 0),
                new Point(2, 0),
                new Point(4, 0)
            };

            Newton interpolation = new();

            List<Polynom> bases = interpolation.Newton_Bases(points);

            Assert.Equal(3, bases.Count);

            Assert.Equal(new List<double> { 1 }, bases[0].Coefficients);
            Assert.Equal(new List<double> { -1, 1 }, bases[1].Coefficients);
            Assert.Equal(new List<double> { 2, -3, 1 }, bases[2].Coefficients);
        }


        [Fact]
        public void NewtonBases_OnePoint_ReturnsOneBasis()
        {
            List<Point> points = new()
            {
                new Point(5, 10)
            };

            Newton interpolation = new();

            List<Polynom> bases = interpolation.Newton_Bases(points);

            Assert.Single(bases);
            Assert.Equal(new List<double> { 1 }, bases[0].Coefficients);
        }

        [Fact]
        public void NewtonBases_TwoPoints_ReturnsCorrectBases()
        {
            List<Point> points = new()
            {
                new Point(3, 10),
                new Point(5, 20)
            };

            Newton interpolation = new();

            List<Polynom> bases = interpolation.Newton_Bases(points);

            Assert.Equal(2, bases.Count);

            Assert.Equal(new List<double> { 1 }, bases[0].Coefficients);
            Assert.Equal(new List<double> { -3, 1 }, bases[1].Coefficients);
        }

        [Fact]
        public void NewtonBases_NegativeCoordinates_ReturnsCorrectBases()
        {
            List<Point> points = new()
            {
                new Point(-2, 0),
                new Point(3, 0)
            };

            Newton interpolation = new();

            List<Polynom> bases = interpolation.Newton_Bases(points);

            Assert.Equal(2, bases.Count);

            Assert.Equal(new List<double> { 2, 1 }, bases[1].Coefficients);
        }

        [Fact]
        public void NewtonBases_RepeatedNodes_ReturnsCorrectBases()
        {
            List<Point> points = new()
            {
                new Point(2, 0),
                new Point(2, 0),
                new Point(3, 0)
            };

            Newton interpolation = new();

            List<Polynom> bases = interpolation.Newton_Bases(points);

            Assert.Equal(3, bases.Count);

            Assert.Equal(new List<double> { 1 }, bases[0].Coefficients);
            Assert.Equal(new List<double> { -2, 1 }, bases[1].Coefficients);
            Assert.Equal(new List<double> { 4, -4, 1 }, bases[2].Coefficients);
        }

        [Fact]
        public void DD_ThreePoints_ReturnsCorrectCoefficients()
        {
            List<Point> points = new()
            {
                new Point(1, 2),
                new Point(2, 3),
                new Point(4, 7)
            };

            Newton interpolation = new();

            List<double> result = interpolation.DD(points);

            Assert.Equal(new List<double> { 2, 1, 1.0 / 3 }, result);
        }

        [Fact]
        public void DD_TwoPoints_ReturnsCorrectCoefficients()
        {
            List<Point> points = new()
            {
                new Point(3, 5),
                new Point(5, 9)
            };

            Newton interpolation = new();

            List<double> result = interpolation.DD(points);

            Assert.Equal(new List<double> { 5, 2 }, result);
        }

        [Fact]
        public void DD_OnePoint_ReturnsY()
        {
            List<Point> points = new()
            {
                new Point(5, 12)
            };

            Newton interpolation = new();

            List<double> result = interpolation.DD(points);

            Assert.Equal(new List<double> { 12 }, result);
        }

        [Fact]
        public void DD_NegativeCoordinates_ReturnsCorrectCoefficients()
        {
            List<Point> points = new()
            {
                new Point(-2, 1),
                new Point(0, 3),
                new Point(2, 5)
            };

            Newton interpolation = new();

            List<double> result = interpolation.DD(points);

            Assert.Equal(new List<double> { 1, 1, 0 }, result);
        }

        [Fact]
        public void DD_DoesNotModifyInputPoints()
        {
            List<Point> points = new()
            {
                new Point(1, 2),
                new Point(2, 3),
                new Point(4, 7)
            };

            List<Point> original = points.ToList();

            Newton interpolation = new();

            interpolation.DD(points);

            Assert.Equal(original.Count, points.Count);

            for (int i = 0; i < points.Count; i++)
            {
                Assert.Equal(original[i].X, points[i].X);
                Assert.Equal(original[i].Y, points[i].Y);
            }
        }
    }
}
