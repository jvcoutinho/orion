namespace Orion.Mining;

public class Miner
{
    private IEnumerable<Commit> _commits;

    private Miner(IEnumerable<Commit> commits)
    {
        _commits = commits;
    }

    /// <summary>
    ///     Default entry point. As such, can be called only once.
    ///     Lazily gets all the commits from the given repositories.
    /// </summary>
    /// <param name="parallel">Marks if the miner should process each repository in parallel.</param>
    /// <param name="repositories">Repositories to mine.</param>
    /// <returns>A Miner instance for this study.</returns>
    public static Miner GetCommitsFrom(bool parallel = false, params Repository[] repositories)
    {
        IEnumerable<Repository> iterator = parallel ? repositories.AsParallel() : repositories;

        var commits = iterator.SelectMany(
            repository => repository.GetCommits()
        );

        return new Miner(commits);
    }

    /// <summary>
    ///     Lazily filters the commits testing them in all of the filters.
    /// </summary>
    /// <param name="filters">Filters to test the commits.</param>
    /// <returns>The same Miner instance.</returns>
    public Miner FilteredBy(params ICommitFilter[] filters)
    {
        var filteredCommits = _commits.Where(
            commit => filters.All(filter => filter.Passes(commit))
        );

        _commits = filteredCommits;
        return this;
    }

    /// <summary>
    ///     Lazily processes the commits in all of the processors.
    /// </summary>
    /// <param name="processors">Processors to process the commits.</param>
    /// <returns>The same Miner instance.</returns>
    public Miner ProcessWith(params ICommitProcessor[] processors)
    {
        var processedCommits = _commits.Stream(
            commit => _ = processors.Stream(processor => processor.Process(commit)).Last()
        );

        _commits = processedCommits;
        return this;
    }

    /// <summary>
    ///     Starts the mining defined with the previous steps.
    /// </summary>
    /// <param name="printCommit">If each commit processed should be printed on console. True by default.</param>
    public void Start(bool printCommit = true)
    {
        foreach (var commit in _commits)
            if (printCommit)
                Console.WriteLine(commit);
    }
}