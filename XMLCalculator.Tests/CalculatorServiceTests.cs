using Xunit;
using System;

namespace XMLCalculator.Tests
{
    public class CalculatorServiceTests
    {
        private readonly CalculatorService _calculatorService;
        public CalculatorServiceTests()
        {
            _calculatorService = new CalculatorService();
        }

        [Fact]
        public void TestAddition()
        {
            // Arrange
            string xml = @"<?xml version='1.0' encoding='UTF-8'?><Maths xmlns:calc='http://example.com/calc'><calc:Operation type='Addition'><calc:Operand unit='meters' quantity='5.5'/><calc:Operand unit='cm' quantity='3.25'/></calc:Operation></Maths>";

            // Act
            double result = _calculatorService.CalculateFromXml(xml);

            // Assert
            Assert.Equal(8.75, result, 2); // 5.5 + 3.25 = 8.75
        }

        [Fact]
        public void TestMultiplication()
        {
            // Arrange
            string xml = @"<?xml version='1.0' encoding='UTF-8'?><Maths xmlns:calc='http://example.com/calc'><calc:Operation type='Multiplication'><calc:Operand unit='meters' quantity='5'/><calc:Operand unit='cm' quantity='3'/></calc:Operation></Maths>";

            // Act
            double result = _calculatorService.CalculateFromXml(xml);

            // Assert
            Assert.Equal(15, result, 2); // 5*3 = 15
        }
    }
}