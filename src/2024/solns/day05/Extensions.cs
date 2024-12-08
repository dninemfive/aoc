namespace d9.aoc._24.day05;
internal static class Extensions
{
    public static int? IndexOf<T>(this IEnumerable<T> enumerable, T value)
    {
        for (int i = 0; i < enumerable.Count(); i++)
            if (value?.Equals(enumerable.ElementAt(i)) ?? false)
                return i;
        return null;
    }
    public static bool Violates(this IEnumerable<int> enumerable, Rule rule)
    {
        (int first, int second) = rule;
        if (!enumerable.Contains(first) || !enumerable.Contains(second))
            return false;
        return enumerable.IndexOf(first) < enumerable.IndexOf(second);
    }
}
