using FluentValidation;

namespace Sondor.Options.Tests.Validators;

/// <summary>
/// Validator for <see cref="TestOptions"/>.
/// </summary>
public class TestOptionsValidator :
    AbstractValidator<TestOptions>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TestOptionsValidator"/> class.
    /// </summary>
    public TestOptionsValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull();
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .NotNull();
    }
}