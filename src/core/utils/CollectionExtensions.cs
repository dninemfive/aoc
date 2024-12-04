namespace d9.aoc.core;
public static class CollectionExtensions
{
    public static T Second<T>(this IEnumerable<T> enumerable) => enumerable.ElementAt(1);
    public static T Second<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        => enumerable.Where(predicate)
                     .ElementAt(1);
    public static Dictionary<K, V> ToDict<T, K, V>(this IEnumerable<T> enumerable, Func<T, K> keys, Func<T, V> values)
        where K : notnull
        => new(enumerable.Select(x => new KeyValuePair<K, V>(keys(x), values(x))));
    public static Dictionary<K, V> ToDictWithKey<K, V>(this IEnumerable<V> enumerable, Func<V, K> keys)
        where K : notnull
        => new(enumerable.Select(x => new KeyValuePair<K, V>(keys(x), x)));
    public static Dictionary<K, V> ToDictWithValue<K, V>(this IEnumerable<K> enumerable, Func<K, V> values)
        where K : notnull
        => new(enumerable.Select(x => new KeyValuePair<K, V>(x, values(x))));
    public static IEnumerable<T> SkipNull<T>(this IEnumerable<T?> enumerable)
    {
        foreach (T? item in enumerable)
            if (item is not null)
                yield return item;
    }
}
