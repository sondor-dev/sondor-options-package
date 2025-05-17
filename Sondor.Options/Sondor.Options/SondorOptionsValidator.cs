using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Sondor.Options;

/// <summary>
/// Sondor settings validator.
/// </summary>
/// <remarks>
/// Creates a new instance of <see cref="SondorOptionsValidator{TSettings}"/>.
/// </remarks>
/// <typeparam name="TOptions">The options type.</typeparam>
/// <param name="name">The name.</param>
/// <param name="serviceProvider">The service provider.</param>
public class SondorOptionsValidator<TOptions>(IServiceProvider serviceProvider,
    string name) :
    IValidateOptions<TOptions>
    where TOptions : class
{
    /// <summary>
    /// The name.
    /// </summary>
    private readonly string? _name = name;

    /// <summary>
    /// The service provider.
    /// </summary>
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    /// <inheritdoc />
    public ValidateOptionsResult Validate(string? name, TOptions options)
    {
        if (_name is not null && _name != name)
        {
            return ValidateOptionsResult.Skip;
        }

        ArgumentNullException.ThrowIfNull(options);

        using var scope = _serviceProvider.CreateScope();

        var validator = scope.ServiceProvider.GetRequiredService<IValidator<TOptions>>();

        var result = validator.Validate(options);

        if (result.IsValid)
        {
            return ValidateOptionsResult.Success;
        }

        var type = options.GetType().Name;
        var errors = result.Errors
            .Select(error => string.Format(OptionsConstants.OptionsValidationErrorFormat,type, error.PropertyName, error.ErrorMessage))
            .ToList();

        return ValidateOptionsResult.Fail(errors);
    }
}