namespace Orion.Mining;

public record GitLocalRepository : Repository
{
    public GitLocalRepository(string path) : base(path)
    {
        if (!path.IsGitRepository())
            throw new ArgumentException($"{path} is not a Git repository", nameof(path));

        Path = path;
    }

    public override IEnumerable<Commit> GetCommits()
    {
        (string output, int statusCode) = Runner.Run("git.exe", Path, "log", "--format=%H");

        if (statusCode != 0) throw new ProgramFailedException($"Git Log operation failed for repository {Path}");

        return output
            .Split('\n')
            .Select(hash => new Commit(this, hash));
    }
}