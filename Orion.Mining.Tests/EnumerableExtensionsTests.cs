using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Orion.Mining.Tests;

public class EnumerableExtensionsTests
{
    [Test]
    public void Stream_ExecuteActionLazilyOverElements()
    {
        // Arrange
        var enumerable = new[] {2, 3, 5, 9};

        // Act
        var list = new List<int>();
        var element = enumerable.Stream(list.Add).ElementAt(2);

        // Assert
        Assert.That(element, Is.EqualTo(5));
        Assert.That(list, Is.EquivalentTo(new[] {2, 3, 5}));
    }

    [Test]
    public void Stream_CanLoopOverAllElements()
    {
        // Arrange
        var enumerable = new[] {2, 3, 5, 9};

        // Act
        var list = new List<int>();
        var element = enumerable.Stream(list.Add).Last();

        // Assert
        Assert.That(element, Is.EqualTo(9));
        Assert.That(list, Is.EquivalentTo(new[] {2, 3, 5, 9}));
    }
}