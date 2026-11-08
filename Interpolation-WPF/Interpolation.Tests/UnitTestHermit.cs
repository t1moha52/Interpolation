using Interpolation_WPF.Interpolation;
using System;
using System.Collections.Generic;
using System.Text;
using Interpolation_WPF.Models;

namespace Interpolation.Tests
{
    public class UnitTestHermit
    {
        Hermit hermit = new Hermit();

        [Fact]
        public void ExpandNodes_ReturnsRepeatedNodes()
        {
            var points = new List<Point>
            {
                new Point(0, new List<double> { -1, -2 }),
                new Point(3, new List<double> { 0, 10, 40 })
            };

            var result = hermit.ExpandNodes(points);

            Assert.Equal(new List<double> { 0, 0, 3, 3, 3 }, result);
        }

        [Fact]
        public void InitialValues_MultiplePoints_ReturnsRepeatedFunctionValues()
        {
            var points = new List<Point>
            {
                new Point(0, new List<double> { -1, -2 }),
                new Point(1, new List<double> { 0, 10, 40 })
            };

            var result = hermit.InitialValues(points);

            Assert.Equal(new List<double> { -1, -1, 0, 0, 0 }, result);
        }

        [Fact]
        public void InitialValues_OnePoint_ReturnsRepeatedValue()
        {
            var points = new List<Point>
            {
                new Point(2, new List<double> { 5, 3, 7 })
            };

            var result = hermit.InitialValues(points);

            Assert.Equal(new List<double> { 5, 5, 5 }, result);
        }

        [Fact]
        public void InitialValues_DifferentNumberOfDerivatives_ReturnsCorrectValues()
        {
            var points = new List<Point>
            {
                new Point(-1, new List<double> { 4 }),
                new Point(0, new List<double> { 2, 3 }),
                new Point(3, new List<double> { 7, 1, 5 })
            };

            var result = hermit.InitialValues(points);

            Assert.Equal(
                new List<double> { 4, 2, 2, 7, 7, 7 },
                result
            );
        }

        [Fact]
        public void DD_Coefficients_HermiteExample_ReturnsCorrectCoefficients()
        {
            var points = new List<Point>
            {
                new Point(0, new List<double> { -1, -2 }),
                new Point(1, new List<double> { 0, 10, 40 })
            };

            var expected = new List<double> { -1, -2, 3, 6, 5 };

            var actual = hermit.DD_Coefficients(points);

            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i], actual[i], 10);
            }
        }

        [Fact]
        public void DD_Coefficients_OnePoint_ReturnsCorrectCoefficients()
        {
            var points = new List<Point>
            {
                new Point(2, new List<double> { 5, 3, 7 })
            };

            var expected = new List<double> { 5, 3, 3.5 };

            var actual = hermit.DD_Coefficients(points);

            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i], actual[i], 10);
            }
        }

        [Fact]
        public void DD_Coefficients_DifferentDerivativeCounts_ReturnsCorrectCoefficients()
        {
            var points = new List<Point>
            {
                new Point(0, new List<double> { 1, 2 }),
                new Point(1, new List<double> { 4, 6 })
            };

            var expected = new List<double> { 1, 2, 1, 2 };

            var actual = hermit.DD_Coefficients(points);

            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i], actual[i], 10);
            }
        }

        [Fact]
        public void Bases_HermiteExample_ReturnsCorrectBases()
        {
            var points = new List<Point>
            {
                new Point(0, new List<double> { -1, -2 }),
                new Point(1, new List<double> { 0, 10, 40 })
            };

            var expected = new List<List<double>>
            {
                new List<double> { 1 },
                new List<double> { 0, 1 },
                new List<double> { 0, 0, 1 },
                new List<double> { 0, 0, -1, 1 },
                new List<double> { 0, 0, 1, -2, 1 }
            };

            var actual = hermit.Bases(points);

            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Count, actual[i].Coefficients.Count);

                for (int j = 0; j < expected[i].Count; j++)
                {
                    Assert.Equal(
                        expected[i][j],
                        actual[i].Coefficients[j],
                        10
                    );
                }
            }
        }

        [Fact]
        public void Interpolate_HermiteExample_ReturnsCorrectPolynomial()
        {
            // Arrange
            var points = new List<Point>
            {
                new Point(0, new List<double> { -1, -2 }),
                new Point(1, new List<double> { 0, 10, 40 })
            };

            // 5x^4 - 4x^3 + 2x^2 - 2x - 1
            var expected = new List<double>
            {
                -1, -2, 2, -4, 5
            };

            // Act
            Polynom actual = hermit.Interpolate(points);

            // Assert
            Assert.Equal(expected.Count, actual.Coefficients.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i], actual.Coefficients[i], 10);
            }
        }

        [Fact]
        public void Interpolate_HermiteExample_SatisfiesInterpolationConditions()
        {
            // Arrange
            var points = new List<Point>
            {
                new Point(0, new List<double> { -1, -2 }),
                new Point(1, new List<double> { 0, 10, 40 })
            };

            // Act
            Polynom actual = hermit.Interpolate(points);

            // Assert
            Assert.Equal(-1, actual.Evaluate(0), 10);
            Assert.Equal(0, actual.Evaluate(1), 10);

            // P'(0) = -2
            // P'(1) = 10
            // P''(1) = 40
        }
    }
}
