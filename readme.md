XML Calculator is an api service that can receive XML via HTTP POST to perform mathematical operations and response with the value.

# Steps to run
1. clone to local.
2. run `dotnet restore`.
3. run `dotnet watch run`.
4. for unit test, navigate to /XMLCalculator.Tests directory and run `dotnet test`.

# Example of payload accept
```
<?xml version="1.0" encoding="UTF-8"?>
<Maths xmlns:calc="http://example.com/calc">
<calc:Operation type="Addition" method="Recursive">
<compute operator="Multiplication" method="Parallel">
<target unit="centimeters" quantity="2"/>
<target unit="m" quantity="4"/>
</compute>
<calc:Operand unit="meters" quantity="5.5"/>
<calc:Operand unit="cm" quantity="3.25"/>
<compute operator="Multiplication" method="Parallel">
<calc:Operation type="Addition" method="Recursive">
<calc:Operand unit="meters" quantity="7.5"/>
<calc:Operand unit="cm" quantity="6.25"/>
</calc:Operation>
<target unit="centimeters" quantity="2"/>
<target unit="m" quantity="4"/>
</compute>
</calc:Operation>
</Maths>
```