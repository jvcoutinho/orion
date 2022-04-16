namespace Orion.Mining;

public record GitLocalRepository : Repository
{
    public GitLocalRepository(string path) : base(path)
    {
        if (!path.IsLocalGitRepository())
            throw new ArgumentException($"{path} is not a Git repository", nameof(path));

        Path = path;
    }

    public override IEnumerable<Commit> GetCommits()
    {
        var (output, error, statusCode) = Runner.Run("git.exe", Path, "log", "--format=%H");

        if (statusCode != 0)
            throw new ProgramFailedException($"Git Log operation failed for repository {Path}: {error}");

        return output
            .TrimEnd()
            .Split('\n')
            .Select(hash => new Commit(this, hash));
    }
}