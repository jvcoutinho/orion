namespace Orion.Mining;

public interface ICommitFilter
{
    bool Passes(Commit commit);
}