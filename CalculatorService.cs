using System;
using System.Linq;
using System.Xml.Linq;

namespace XMLCalculator
{
    public class CalculatorService
    {
        private readonly Dictionary<string, double> _centimeterScale = new Dictionary<string, double>
        {
            { "cm", 1.0 }, // maintain cm to cm
            { "m", 100.0 }, // standardize meters to centimeter
            { "centimeters", 1.0 }, // maintain cm to cm
            { "meters", 100.0 } // standardize meters to centimeter
        };
        public double CalculateFromXml(string xml)
        {
            XDocument xmlDocument = XDocument.Parse(xml);
            var operation = xmlDocument.Descendants().FirstOrDefault(e => e.Name.LocalName == "Operation");
            return ProcessOperation(operation);
        }

        private double ProcessOperation(XElement operationElement)
        {
            string operationType = operationElement.Attribute("type").Value;

            switch (operationType)
            {
                case "Addition":
                    return PerformAddition(operationElement);
                case "Multiplication":
                    return PerformMultiplication(operationElement);
                default:
                    throw new InvalidOperationException("The operation is not support");
            }
        }

        private double PerformAddition(XElement operationElement)
        {
            var operands = operationElement.Descendants().Where(e => e.Name.LocalName == "Operand").Select(e => ToCentimeter(e)).ToArray();
            return operands.Sum();
        }

        private double PerformMultiplication(XElement operationElement)
        {
            var operands = operationElement.Descendants().Where(e => e.Name.LocalName == "Operand").Select(e => ToCentimeter(e)).ToArray();
            return operands.Aggregate(1.0, (accumulate, value) => accumulate * value);
        }

        private double ToCentimeter(XElement operandElement)
        {
            string unit = operandElement.Attribute("unit").Value;
            double quantity = Convert.ToDouble(operandElement.Attribute("quantity").Value);

            if (_centimeterScale.TryGetValue(unit, out double centimeterScale))
            {
                return quantity * centimeterScale; // convert meter to centimeter by multiplying with the scale
            }

            throw new InvalidOperationException($"The unit: {unit} is not supported for conversion");
        }
    }

}
