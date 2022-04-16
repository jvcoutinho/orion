namespace Orion.Mining;

public record GitRemoteRepository : Repository
{
    public GitRemoteRepository(string uri) : base(uri)
    {
        if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            throw new ArgumentException($"{uri} is not a valid URI", nameof(uri));

        Uri = new Uri(uri);
        Path = uri;
    }

    public Uri Uri { get; init; }

    public override IEnumerable<Commit> GetCommits()
    {
        var temporaryDirectoryPath = System.IO.Path.GetTempPath();
        var (_, error, statusCode) = Runner.Run("git.exe", temporaryDirectoryPath, "clone", Path, Uri.Fragment);

        if (statusCode != 0)
            throw new ProgramFailedException($"Git Clone operation failed for repository {Path}: {error}");

        var localPath = System.IO.Path.Combine(temporaryDirectoryPath, Uri.Fragment);
        return new GitLocalRepository(localPath).GetCommits();
    }
}