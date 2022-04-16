using System;
using System.Linq;
using NUnit.Framework;

namespace Orion.Mining.Tests;

public class GitLocalRepositoryTests
{
    [Test]
    public void Constructor_WhenGivenPathIsNotGitRepository_ShouldThrowException()
    {
        Assert.Throws<ArgumentException>(() => _ = new GitLocalRepository(".."));
    }

    [Test]
    public void GetCommits_ShouldGetGitCommits()
    {
        // Arrange
        const string orionPath = "../../../..";
        const string initialCommitHash = "8688935b5bfa842a33deefec84c01d7f7b6d91ba";
        Repository repository = new GitLocalRepository(orionPath);

        // Act
        var commits = repository.GetCommits();

        // Assert
        Assert.That(commits.Last().Hash, Is.EqualTo(initialCommitHash));
    }
}