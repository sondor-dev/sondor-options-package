using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Sondor.Options.Extensions;
using Microsoft.Extensions.Options;

namespace Sondor.Options.Tests.Extensions;

/// <summary>
/// Tests for <see cref="ServiceCollectionExtensions"/>.
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
        configurationBuilder.AddJsonFile("appsettings.json");

        _services.AddSingleton<IConfiguration>(configurationBuilder.Build());
    }

    /// <summary>
    /// Ensures that <see cref="ServiceCollectionExtensions.AddSondorOptions{TOptions}"/> loads and validates options.
    /// </summary>
    [Test]
    public void AddSondorOptions()
    {
        // arrange
        _services.AddSondorOptions<TestOptions>();

        // act
        using var serviceProvider = _services.BuildServiceProvider();
        var options = serviceProvider.GetRequiredService<IOptions<TestOptions>>().Value;

        // assert
        Assert.Multiple(() =>
        {
            Assert.That(options, Is.Not.NaN);
            Assert.That(options.Name, Is.EqualTo(TestConstants.TestName));
            Assert.That(options.Description, Is.EqualTo(TestConstants.TestDescription));
        });
    }
}