namespace Orion.Mining;

public static class EnumerableExtensions
{
    public static IEnumerable<T> Stream<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
        foreach (var element in enumerable)
        {
            action(element);

            yield return element;
        }
    }

    public static void Run<T>(this IEnumerable<T> enumerable)
    {
        foreach (var e in enumerable) Console.WriteLine(e);
    }
}