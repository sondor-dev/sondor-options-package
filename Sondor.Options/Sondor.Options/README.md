# Sondor Options
Sondor Options is a library for managing application settings in .NET applications.
It provides a simple and flexible way to define,validate, and access application options.

## Getting Started
1. Create a class to represent your options
```csharp
/// <summary>
/// Represents the options for the application.
/// </summary>
public class MyOptions
{
    /// <summary>
    /// Gets or sets the first option.
    /// </summary>
    public string Option1 { get; init; }

    /// <summary>
    /// Gets or sets the second option.
    /// </summary>
    public int Option2 { get; init; }
}
```
2. Create a FluentValidation validator for your options class.
```csharp
public class MyOptionsValidator : AbstractValidator<MyOptions>
{
    public MyOptionsValidator()
    {
        RuleFor(x => x.Option1)
            .NotEmpty()
            .WithMessage("Option1 is required.");
        RuleFor(x => x.Option2)
            .GreaterThan(0)
            .WithMessage("Option2 must be greater than 0.");
    }
}
```
3. Add `Sondor.Options` namespace to your project.
```csharp
using Sondor.Options;
```
4. Add the configuration section to your `appsettings.json` file.
```json
{
  "MyOptions": {
    "Option1": "Value1",
    "Option2": 10
  }
}
```
5. Register the options and validator in your DI container.
```csharp
_services.AddSondorOptions<MyOptions>();
```