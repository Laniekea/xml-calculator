using System;
using System.Xml.Linq;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/calculate", (string xml) =>
{
    try {
        var result = CalculateFromXml(xml);
        return Results.Ok(new { Result = result });    
    }
    catch (Exception ex) {
        return Results.BadRequest(new { Message = ex.Message });
    }
})
.WithName("Calculate")
.WithOpenApi();

app.Run();

double CalculateFromXml(string xml) 
{
    XDocument xmlDocument = XDocument.Parse(xml);
    var operation = xmlDocument.Descendants().FirstOrDefault(e => e.Name.LocalName == "Operation");
    return ProcessOperation(operation);
}

double ProcessOperation(XElement operationElement)
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

double PerformAddition(XElement operationElement)
{
    var operands = operationElement.Descendants().Where(e => e.Name.LocalName == "Operand").Select(e => Convert.ToDouble(e.Attribute("quantity").Value));
    return operands.Sum();
}

double PerformMultiplication(XElement operationElement)
{
     var operands = operationElement.Descendants().Where(e => e.Name.LocalName == "Operand").Select(e => Convert.ToDouble(e.Attribute("quantity").Value));
    return operands.Aggregate(1.0, (accumulate, value) => accumulate * value);
}