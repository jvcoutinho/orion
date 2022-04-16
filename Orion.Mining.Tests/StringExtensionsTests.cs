using NUnit.Framework;

namespace Orion.Mining.Tests;

public class StringExtensionsTests
{
    [Test]
    [TestCase("../../../..", ExpectedResult = true)]
    [TestCase("./", ExpectedResult = false)]
    public bool Test_IsGitRepository(string directory)
    {
        return directory.IsGitRepository();
    }
}