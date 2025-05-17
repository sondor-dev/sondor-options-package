using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Sondor.Options.Extensions;

/// <summary>
/// Collection of <see cref="IServiceCollection"/> extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add options to the service collection.
    /// </summary>
    /// <typeparam name="TOptions">The options type.</typeparam>
    /// <param name="services">The services.</param>
    /// <param name="section">The section.</param>
    /// <returns>Returns the service collection.</returns>
    /// <exception cref="ValidationException">This exception is thrown if validation fails for the loaded options.</exception>
    public static IServiceCollection AddSondorOptions<TOptions>(this IServiceCollection services,
        string? section = null)
        where TOptions : class
    {
        if (string.IsNullOrWhiteSpace(section))
        {
            section = typeof(TOptions).Name;
        }

        services
            .AddOptions<TOptions>()
            .BindConfiguration(section)
            .OptionsFluentValidation()
            .ValidateOnStart();
        
        return services;
    }
}