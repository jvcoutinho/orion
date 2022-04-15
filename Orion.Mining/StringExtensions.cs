namespace Orion.Mining;

public static class StringExtensions
{
    private const string GitDirectory = ".git";
    
    public static bool IsGitRepository(this string path)
    {
        return Directory.Exists(path) && ContainsGitDirectory(path);
    }

    private static bool ContainsGitDirectory(string path)
    {
        return Directory.Exists(Path.Combine(path, GitDirectory));
    }
}