using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Sondor.Options.Extensions;

/// <summary>
/// Collection of <see cref="OptionsBuilder{TOptions}"/> extensions.
/// </summary>
public static class OptionsBuilderExtensions
{
    /// <summary>
    /// Add option fluent validation.
    /// </summary>
    /// <typeparam name="TOptions">The options type.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <returns>Returns the options builder.</returns>
    public static OptionsBuilder<TOptions> OptionsFluentValidation<TOptions>(this OptionsBuilder<TOptions> builder)
        where TOptions : class
    {
        builder.Services.AddSingleton<IValidateOptions<TOptions>>(
            serviceProvider => new SondorOptionsValidator<TOptions>(serviceProvider, builder.Name));

        return builder;
    }
}