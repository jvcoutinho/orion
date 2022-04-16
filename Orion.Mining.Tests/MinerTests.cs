using System.Collections.Generic;
using NUnit.Framework;

namespace Orion.Mining.Tests;

public class MinerTests
{
    [Test]
    public void Start_ShouldRunDefinedSteps()
    {
        // Arrange
        var list = new List<string>();

        // Act
        Miner
            .GetCommitsFrom(false, new OddCommitsRepository(), new EvenCommitsRepository())
            .FilteredBy(new HashBetweenCommitFilter(3, 6))
            .ProcessWith(new AppendHashToListCommitProcessor(list))
            .Start();

        // Assert
        Assert.That(list, Is.EquivalentTo(new[] {"3", "4", "5", "6"}));
    }

    private class AppendHashToListCommitProcessor : ICommitProcessor
    {
        private readonly IList<string> _list;

        public AppendHashToListCommitProcessor(IList<string> list)
        {
            _list = list;
        }

        public void Process(Commit commit)
        {
            _list.Add(commit.Hash);
        }
    }

    private class HashBetweenCommitFilter : ICommitFilter
    {
        private readonly int _max;
        private readonly int _min;

        public HashBetweenCommitFilter(int min, int max)
        {
            _min = min;
            _max = max;
        }

        public bool Passes(Commit commit)
        {
            return int.TryParse(commit.Hash, out var n) && n >= _min && n <= _max;
        }
    }

    private record OddCommitsRepository() : Repository("")
    {
        public override IEnumerable<Commit> GetCommits()
        {
            return new[]
            {
                new Commit(this, "1"),
                new Commit(this, "3"),
                new Commit(this, "5")
            };
        }
    }

    private record EvenCommitsRepository() : Repository("")
    {
        public override IEnumerable<Commit> GetCommits()
        {
            return new[]
            {
                new Commit(this, "2"),
                new Commit(this, "4"),
                new Commit(this, "6")
            };
        }
    }
}