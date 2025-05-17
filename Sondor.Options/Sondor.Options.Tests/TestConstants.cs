namespace Sondor.Options.Tests;

/// <summary>
/// Collection of test constants.
/// </summary>
internal class TestConstants
{
    /// <summary>
    /// The test name.
    /// </summary>
    internal const string TestName = "TestName";

    /// <summary>
    /// The test description.
    /// </summary>
    internal const string TestDescription = "TestDescription";

    /// <summary>
    /// The test options.
    /// </summary>
    internal static readonly TestOptions TestOptions =
        new()
        {
            Name = TestName,
            Description = TestDescription
        };
}
