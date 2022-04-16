namespace Orion.Mining;

public static class StringExtensions
{
    private const string GitDirectory = ".git";

    /// <summary>
    ///     Returns if the given path is a valid directory and it contains a <see cref="GitDirectory" /> directory.
    /// </summary>
    /// <param name="path">String to evaluate.</param>
    /// <returns>True if and only if path is a valid Git local repository.</returns>
    public static bool IsGitRepository(this string path)
    {
        return Directory.Exists(path) && ContainsGitDirectory(path);
    }

    private static bool ContainsGitDirectory(string path)
    {
        return Directory.Exists(Path.Combine(path, GitDirectory));
    }
}