using System.Collections.Generic;
using Interpolation_WPF.Models;
using Xunit;



namespace Interpolation.Tests
{
    public class UnitTest1
    {
        public class EngineTests
        {
            [Fact]
            public void TestAddPol()
            {
                // Arrange
                Engine engine = new();

                Polynom p1 = new()
                {
                    Coefficients = new List<double> { 2, 3, 4 }
                };

                Polynom p2 = new()
                {
                    Coefficients = new List<double> { 5, 2 }
                };

                // Act
                Polynom result = engine.AddPol(p1, p2);

                // Assert
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
