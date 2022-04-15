namespace Orion.Mining;

public record GitRemoteRepository : Repository
{
    public Uri Uri { get; init; }

    public GitRemoteRepository(string uri) : base(uri)
    {
        if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            throw new ArgumentException($"{uri} is not a valid URI", nameof(uri));

        Uri = new Uri(uri);
        Path = uri;
    }

    public override IEnumerable<Commit> GetCommits()
    {
        string temporaryDirectoryPath = System.IO.Path.GetTempPath();
        (_, int statusCode) = Runner.Run("git.exe", temporaryDirectoryPath, "clone", Path, Uri.Fragment);

        if (statusCode != 0) throw new ProgramFailedException($"Git Clone operation failed for repository {Path}");

        string localPath = System.IO.Path.Combine(temporaryDirectoryPath, Uri.Fragment);
        return new GitLocalRepository(localPath).GetCommits();
    }
}