namespace Orion.Mining;

public static class EnumerableExtensions
{
    /// <summary>
    ///     Lazily executes an action over elements of a collection.
    /// </summary>
    /// <param name="enumerable">The collection to iterate over.</param>
    /// <param name="action">The action to apply to the elements of the collection.</param>
    /// <typeparam name="T">Type of the elements.</typeparam>
    /// <returns>The iterated collection. If the action is non-stop, it is the original collection.</returns>
    public static IEnumerable<T> Stream<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
        foreach (var element in enumerable)
        {
            action(element);

            yield return element;
        }
    }
}