namespace Orion.Mining;

public abstract record Repository(string Path)
{
    public abstract IEnumerable<Commit> GetCommits();
}