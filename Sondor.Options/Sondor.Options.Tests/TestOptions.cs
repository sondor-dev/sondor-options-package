namespace Sondor.Options.Tests;

/// <summary>
/// The test options.
/// </summary>
/// <remarks>
/// Creates a new instance of <see cref="TestOptions"/>.
/// </remarks>
/// <param name="name">The name.</param>
/// <param name="description">The description.</param>
public class TestOptions
{
    /// <summary>
    /// The name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// The description.
    /// </summary>
    public string Description { get; init; } = string.Empty;
}
