using System;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Orion.Mining.Tests;

public class GitRemoteRepositoryTests
{
    [Test]
    [TestCase("not path")]
    [TestCase("..")]
    [TestCase("http://google.com")]
    public void Constructor_WhenGivenPathIsNotGitRepositoryUri_ShouldThrowException(string uri)
    {
        Assert.Throws<ArgumentException>(() => _ = new GitRemoteRepository(uri));
    }

    [Test]
    public void GetCommits_ShouldCloneRepository_AndGetGitCommits()
    {
        // Arrange
        const string orionUri = "https://github.com/jvcoutinho/orion";
        const string initialCommitHash = "8688935b5bfa842a33deefec84c01d7f7b6d91ba";
        Repository repository = new GitRemoteRepository(orionUri);

        // Act
        var commits = repository.GetCommits();

        // Assert
        var orionPath = Path.Combine(Path.GetTempPath(), "orion");
        Assert.That(orionPath, Does.Exist);
        Assert.That(commits.Last().Hash, Is.EqualTo(initialCommitHash));

        // Cleanup
        Directory.Delete(orionPath, true);
    }
}