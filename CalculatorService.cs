using System;
using System.Linq;
using System.Xml.Linq;

public class CalculatorService
{
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
        var operands = operationElement.Descendants().Where(e => e.Name.LocalName == "Operand").Select(e => Convert.ToDouble(e.Attribute("quantity").Value));
        return operands.Sum();
    }

    private double PerformMultiplication(XElement operationElement)
    {
        var operands = operationElement.Descendants().Where(e => e.Name.LocalName == "Operand").Select(e => Convert.ToDouble(e.Attribute("quantity").Value));
        return operands.Aggregate(1.0, (accumulate, value) => accumulate * value);
    }
}