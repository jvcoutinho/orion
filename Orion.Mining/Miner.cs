namespace Orion.Mining;

public class Miner
{
    private IEnumerable<Commit> _commits;

    public Miner(IEnumerable<Commit> commits)
    {
        _commits = commits;
    }

    public static Miner GetCommitsFrom(bool parallel = false, params Repository[] repositories)
    {
        IEnumerable<Repository> iterator = parallel ? repositories.AsParallel() : repositories;

        var commits = iterator.SelectMany(
            repository => repository.GetCommits()
        );

        return new Miner(commits);
    }

    public Miner FilteredBy(params ICommitFilter[] filters)
    {
        var filteredCommits = _commits.Where(
            commit => filters.All(filter => filter.Passes(commit))
        );

        _commits = filteredCommits;
        return this;
    }

    public Miner ProcessWith(params ICommitProcessor[] processors)
    {
        var processedCommits = _commits.Stream(
            commit => processors.Stream(processor => processor.Process(commit))
        );

        _commits = processedCommits;
        return this;
    }

    public void Start()
    {
        foreach (var commit in _commits) Console.WriteLine(commit);
    }
}