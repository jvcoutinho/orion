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
        try
        {
            // Arrange
            const string orionUri = "https://github.com/jvcoutinho/orion";
            const string initialCommitHash = "8688935b5bfa842a33deefec84c01d7f7b6d91ba";
            Repository repository = new GitRemoteRepository(orionUri);

            // Act
            var commits = repository.GetCommits();

            // Assert
            Assert.That(Path.Combine(GitRemoteRepository.CloneDirectory, "orion"), Does.Exist);
            Assert.That(commits.Last().Hash, Is.EqualTo(initialCommitHash));
        }
        finally
        {
            // Cleanup
            DeleteGitDirectory(GitRemoteRepository.CloneDirectory);
        }
    }

    private static void DeleteGitDirectory(string path)
    {
        var directory = new DirectoryInfo(path) {Attributes = FileAttributes.Normal};

        // ".git" files have a restriction where changing their attributes is required to delete them.
        foreach (var info in directory.GetFileSystemInfos("*", SearchOption.AllDirectories))
            info.Attributes = FileAttributes.Normal;

        Directory.Delete(path, true);
    }
}