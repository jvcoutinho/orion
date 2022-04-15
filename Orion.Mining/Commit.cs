namespace Orion.Mining;

public record Commit(Repository Repository, string Hash)
{
    public override string ToString()
    {
        return $"[{Repository.Path} - {Hash}]";
    }
}