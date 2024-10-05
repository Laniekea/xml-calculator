using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using XMLCalculator;

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
    try {
        var result = calculatorService.CalculateFromXml(xml);
        return Results.Ok(new { Value = result, Unit = "cm"  });    
    }
    catch (Exception ex) {
        return Results.BadRequest(new { Message = ex.Message });
    }
})
.WithName("Calculate")
.WithOpenApi();

app.Run();