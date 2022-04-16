namespace Orion.Mining;

public record GitRemoteRepository : Repository
{
    public const string CloneDirectory = "clones";

    public GitRemoteRepository(string uri) : base(uri)
    {
        if (!uri.IsRemoteGitRepository())
            throw new ArgumentException($"{uri} is not a valid remote Git repository", nameof(uri));

        Uri = new Uri(uri);
        Path = uri;
    }

    private Uri Uri { get; }

    public override IEnumerable<Commit> GetCommits()
    {
        var localPath = System.IO.Path.Combine(CloneDirectory, Uri.Segments.Last());
        var (_, error, statusCode) = Runner.Run("git.exe", ".", "clone", Path, localPath);

        if (statusCode != 0)
            throw new ProgramFailedException($"Git Clone operation failed for repository {Path}: {error}");

        return new GitLocalRepository(localPath).GetCommits();
    }
}