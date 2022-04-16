namespace Orion.Mining;

public static class StringExtensions
{
    private const string GitDirectory = ".git";

    /// <summary>
    ///     Returns if the given path is a valid directory and it contains a <see cref="GitDirectory" /> directory.
    /// </summary>
    /// <param name="path">String to evaluate.</param>
    /// <returns>True if and only if path is a valid Git local repository.</returns>
    public static bool IsLocalGitRepository(this string path)
    {
        return Directory.Exists(path) && ContainsGitDirectory(path);
    }

    public static bool IsRemoteGitRepository(this string path)
    {
        var (_, _, statusCode) = Runner.Run("git.exe", arguments: new[] {"ls-remote", path});

        return statusCode == 0;
    }

    private static bool ContainsGitDirectory(string path)
    {
        return Directory.Exists(Path.Combine(path, GitDirectory));
    }
}