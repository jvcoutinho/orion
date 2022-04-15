namespace Orion.Mining;

public interface ICommitProcessor
{
    void Process(Commit commit);
}