using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Sondor.Options.Extensions;
using Microsoft.Extensions.Options;

namespace Sondor.Options.Tests.Extensions;

/// <summary>
/// Tests for <see cref="Sondor.Options.Extensions.ServiceCollectionExtensions"/>.
/// </summary>
[TestFixture]
public class ServiceCollectionExtensionsTests
{
    /// <summary>
    /// The services.
    /// </summary>
    private readonly IServiceCollection _services;

    /// <summary>
    /// Creates a new instance of <see cref="ServiceCollectionExtensionsTests"/>.
    /// </summary>
    public ServiceCollectionExtensionsTests()
    {
        _services = new ServiceCollection();

        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.Sources.Clear();
        configurationBuilder.AddInMemoryCollection([
            new KeyValuePair<string, string?>($"{nameof(TestOptions)}:{nameof(TestOptions.Name)}", TestConstants.TestName),
            new KeyValuePair<string, string?>($"{nameof(TestOptions)}:{nameof(TestOptions.Description)}", TestConstants.TestDescription)
        ]);

        _services.AddSingleton<IConfiguration>(configurationBuilder.Build());
    }

    /// <summary>
    /// Ensures that <see cref="Sondor.Options.Extensions.ServiceCollectionExtensions.AddSondorOptions{TOptions}"/> loads and validates options.
    /// </summary>
    [Test]
    public void AddOptions()
    {
        // arrange
        _services.AddValidatorsFromAssembly(typeof(TestConstants).Assembly);

        // arrange
        _services.AddSondorOptions<TestOptions>();

        // act
        using var serviceProvider = _services.BuildServiceProvider();
        var options = serviceProvider.GetRequiredService<IOptions<TestOptions>>().Value;

        Assert.Multiple(() =>
        {
            Assert.That(options, Is.Not.NaN);
            Assert.That(options.Name, Is.EqualTo(TestConstants.TestName));
            Assert.That(options.Description, Is.EqualTo(TestConstants.TestDescription));
        });
    }
}