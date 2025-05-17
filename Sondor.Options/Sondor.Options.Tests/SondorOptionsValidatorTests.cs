using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Sondor.Options.Tests;

/// <summary>
/// Tests for <see cref="SondorOptionsValidator{TOptions}"/>.
/// </summary>
[TestFixture]
public class SondorOptionsValidatorTests
{
    /// <summary>
    /// The web application builder.
    /// </summary>
    private readonly IServiceCollection _services = new ServiceCollection();

    /// <summary>
    /// Test setup.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        _services.AddValidatorsFromAssembly(typeof(TestConstants).Assembly);
    }

    /// <summary>
    /// Ensures that <see cref="SondorOptionsValidator{TOptions}.Validate"/> returns the <see cref="ValidateOptionsResult.Skip"/> when the name doesn't match.
    /// </summary>
    [Test]
    public void SondorOptionsValidator_validate_skip()
    {
        // arrange
        const string name = "invalid-name";
        var serviceProvider = _services.BuildServiceProvider();
        var validator = new SondorOptionsValidator<TestOptions>(serviceProvider, nameof(TestOptions));

        // act
        var result = validator.Validate(name, TestConstants.TestOptions);

        // assert
        Assert.That(result, Is.EqualTo(ValidateOptionsResult.Skip));
    }

    /// <summary>
    /// Ensures that <see cref="SondorOptionsValidator{TOptions}.Validate"/> returns the <see cref="ValidateOptionsResult.Skip"/> when the name doesn't match.
    /// </summary>
    [Test]
    public void SondorOptionsValidator_validate_success()
    {
        // arrange
        var serviceProvider = _services.BuildServiceProvider();
        var validator = new SondorOptionsValidator<TestOptions>(serviceProvider, nameof(TestOptions));

        // act
        var result = validator.Validate(nameof(TestOptions), TestConstants.TestOptions);

        // assert
        Assert.That(result, Is.EqualTo(ValidateOptionsResult.Success));
    }

    /// <summary>
    /// Ensures that <see cref="SondorOptionsValidator{TOptions}.Validate"/> returns the <see cref="ValidateOptionsResult.Fail(IEnumerable{string})"/> when the name doesn't match.
    /// </summary>
    [Test]
    public void SondorOptionsValidator_validate_fail()
    {
        // arrange
        var serviceProvider = _services.BuildServiceProvider();
        var validator = new SondorOptionsValidator<TestOptions>(serviceProvider, nameof(TestOptions));
        var expectedError = string.Format(OptionsConstants.OptionsValidationErrorFormat, nameof(TestOptions),
            nameof(TestOptions.Name), $"'{nameof(TestOptions.Name)}' must not be empty.");

        // act
        var result = validator.Validate(nameof(TestOptions), new TestOptions
        {
            Description = TestConstants.TestDescription
        });

        // assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Failed, Is.True);
            Assert.That(result.FailureMessage, Is.EqualTo(expectedError));
            Assert.That(result.Failures, Is.EqualTo(new List<string> { expectedError }));
            Assert.That(result.Skipped, Is.False);
            Assert.That(result.Succeeded, Is.False);
        });
    }
}