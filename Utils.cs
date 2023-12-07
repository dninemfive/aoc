using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace d9.aoc._23;
// i should really just use utl...
public static class Utils
{
    public static string ListNotation<T>(this IEnumerable<T> enumerable)
        => enumerable.Any() ? enumerable.Select(x => $"{x}").Aggregate((x, y) => $"{x}, {y}") : "(no elements)";
    public static IEnumerable<T> ToMany<T>(this string s, string delimiter = " ", IFormatProvider? formatProvider = null, int skip = 0)
        where T : IParsable<T>
        => s.SplitAndTrim(delimiter).Skip(skip).Select(x => T.Parse(x.Trim(), formatProvider ?? CultureInfo.InvariantCulture));
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
    public static T Second<T>(this IEnumerable<T> enumerable) => enumerable.ElementAt(1);
    public static Dictionary<K, V> ToDict<T, K, V>(this IEnumerable<T> enumerable, Func<T, K> keySelector, Func<T, V> valueSelector)
        where K : notnull
        => new(enumerable.Select(x => new KeyValuePair<K, V>(keySelector(x), valueSelector(x))));
    public static Dictionary<K, V> ToDictWithKey<K, V>(this IEnumerable<V> enumerable, Func<V, K> keySelector)
        where K : notnull
        => new(enumerable.Select(x => new KeyValuePair<K, V>(keySelector(x), x)));
    public static Dictionary<K, V> ToDictWithValue<K, V>(this IEnumerable<K> enumerable, Func<K, V> valueSelector)
        where K : notnull
        => new(enumerable.Select(x => new KeyValuePair<K, V>(x, valueSelector(x))));
    public static string Repeated<T>(this string s, T ct)
        where T : INumber<T>
    {
        string result = "";
        for(T i = T.Zero; i < ct; i++) result += s;
        return result;
    }
    /// <summary>
    /// Checks whether a character value represents a digit when printed.
    /// </summary>
    /// <param name="c">The character whose value to check.</param>
    /// <returns><see langword="true"/> if <paramref name="c"/> is between 
    /// <c>(<see langword="char"/>)'0'</c> and <c>(<see langword="char"/>)'9'</c>, inclusive,
    /// or <see langword="false"/> otherwise.
    /// </returns>
    public static bool IsDigit(this char c) => c is >= '0' and <= '9';
    /// <summary>
    /// Given a digit character (i.e. <c>(<see langword="char"/>)'0'</c> through 
    /// <c>(<see langword="char"/>)'9'</c>), returns the integer value 
    /// that character represents.<br/><b>Throws an exception</b> otherwise.
    /// </summary>
    /// <param name="c">The character whose value to get. 
    /// <b>Must</b> be between <c>(<see langword="char"/>)'0'</c> and 
    /// <c>(<see langword="char"/>)'9'</c>.</param>
    /// <returns>The integer value corresponding to the character, e.g. 
    /// <c>(<see langword="int"/>)0</c> for <c>(<see langword="char"/>)'0'</c>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="c"/> is not a digit.</exception>
    public static int IntValue(this char c) => c switch
    {
        >= '0' and <= '9' => c - '0',
        _ => throw new ArgumentOutOfRangeException(nameof(c), "Only digit characters have integer values!")
    };
    public static string Merge(this IEnumerable<string> strings)
        => strings.Aggregate((x, y) => $"{x}{y}");
    public static T Parse<T>(this string s, IFormatProvider? formatProvider = null)
        where T : IParsable<T>
    => T.Parse(s, formatProvider ?? CultureInfo.InvariantCulture);
}
