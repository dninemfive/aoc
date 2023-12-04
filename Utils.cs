using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d9.aoc._23;
// i should really just use utl...
public static class Utils
{
    public static string ListNotation<T>(this IEnumerable<T> enumerable)
        => enumerable.Any() ? enumerable.Select(x => $"{x}").Aggregate((x, y) => $"{x}, {y}") : "(no elements)";
    public static IEnumerable<int> ToInts(this string s, string delimiter = " ")
        => s.SplitAndTrim(delimiter).Select(x => int.Parse(x.Trim()));
    public static List<string> SplitAndTrim(this string s, params string[] delimiters)
    {
        List<string> result = [s];
        foreach(string delimiter in delimiters)
        {
            List<string> newResult = new();
            foreach (string s2 in result)
                newResult.AddRange(s2.Split(delimiter, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
            result = newResult;
        }
        return result;
    }
    public static Dictionary<K, V> ToDictWithKey<K, V>(this IEnumerable<V> enumerable, Func<V, K> keySelector)
        where K : notnull
        => new(enumerable.Select(x => new KeyValuePair<K, V>(keySelector(x), x)));
}
