using NUnit.Framework;

namespace Orion.Mining.Tests;

public class RunnerTests
{
    [Test]
    public void Run_GivenProgramExists_WhenProgramIsSuccessful_ReturnsOutput()
    {
        // Act
        var (output, error, statusCode) = Runner.Run("git.exe", arguments: "version");

        // Assert
        Assert.That(output, Contains.Substring("git version"));
        Assert.That(error, Is.Empty);
        Assert.That(statusCode, Is.Zero);
    }

    [Test]
    public void Run_GivenProgramExists_WhenProgramIsUnsuccessful_ReturnsError()
    {
        // Act
        var (output, error, statusCode) = Runner.Run("git.exe", arguments: "wrong-version");

        // Assert
        Assert.That(output, Is.Empty);
        Assert.That(error, Contains.Substring("is not a git command"));
        Assert.That(statusCode, Is.EqualTo(1));
    }
}