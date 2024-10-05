using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using XMLCalculator;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<CalculatorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/calculate", (string xml, CalculatorService calculatorService) =>
{
    try
    {
        var result = calculatorService.CalculateFromXml(xml);

        XDocument responseXml = new XDocument(
        new XElement("Result",
            new XElement("quantity", result),   // If using a DTO
            new XElement("unit", "cm")      // If using a DTO
        )
    );

        // Return XML with proper content type
        return Results.Content(responseXml.ToString(), "application/xml", statusCode: 200);
    }
    catch (InvalidOperationException ex)
    {

        XDocument errorXml = new XDocument(
            new XElement("Bad Request",
                new XElement("Message", ex.Message)
            )
        );

        // Return XML error response with status code 400
        return Results.Content(errorXml.ToString(), "application/xml", statusCode: 400);
    }
    catch (Exception ex)
    {

        XDocument errorXml = new XDocument(
            new XElement("Internal Server Error",
                new XElement("Message", ex.Message)
            )
        );

        // Return XML error response with status code 400
        return Results.Content(errorXml.ToString(), "application/xml", statusCode: 500);
    }
})
.WithName("Calculate")
.WithOpenApi();

app.Run();