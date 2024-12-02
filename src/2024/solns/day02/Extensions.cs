namespace d9.aoc._24.day02;
internal static class Extensions
{
    internal static bool IsSafe(this IEnumerable<int> deltas)
        => deltas.MinMax() is ( > 0, <= 3) or ( >= -3, < 0);
    internal static IEnumerable<T> Variation<T>(this IEnumerable<T> enumerable, int i)
    {
        int index = 0;
        foreach (T item in enumerable)
            if (index++ != i)
                yield return item;
    }
    internal static IEnumerable<IEnumerable<T>> Variations<T>(this IEnumerable<T> enumerable)
    {
        for (int i = 0; i < enumerable.Count(); i++)
            yield return enumerable.Variation(i);
    }
}
