using System.Globalization;
using System.Numerics;
namespace d9.aoc._23;
// i should really just use utl...
public static class Utils
{
    public static string ListNotation<T>(this IEnumerable<T> enumerable, Func<T, string>? format = null)
        => enumerable.Any() ? enumerable.Select(x => $"{(format is not null ? format(x) : x)}")
                                        .Aggregate((x, y) => $"{x}, {y}") : "(no elements)";
    public static IEnumerable<T> ToMany<T>(this string s, string delimiter = " ", IFormatProvider? formatProvider = null, int skip = 0)
        where T : IParsable<T>
        => s.SplitAndTrim(delimiter).Skip(skip).Select(x => T.Parse(x.Trim(), formatProvider ?? CultureInfo.InvariantCulture));
    public static List<string> SplitAndTrim(this string s, params string[] delimiters)
    {
        List<string> result = [s];
        foreach (string delimiter in delimiters)
        {
            List<string> newResult = new();
            foreach (string s2 in result)
                newResult.AddRange(s2.Split(delimiter, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
            result = newResult;
        }
        return result;
    }
    public static T Second<T>(this IEnumerable<T> enumerable) => enumerable.ElementAt(1);
    public static Dictionary<K, V> ToDict<T, K, V>(this IEnumerable<T> enumerable, Func<T, K> keys, Func<T, V> values)
        where K : notnull
        => new(enumerable.Select(x => new KeyValuePair<K, V>(keys(x), values(x))));
    public static Dictionary<K, V> ToDictWithKey<K, V>(this IEnumerable<V> enumerable, Func<V, K> keys)
        where K : notnull
        => new(enumerable.Select(x => new KeyValuePair<K, V>(keys(x), x)));
    public static Dictionary<K, V> ToDictWithValue<K, V>(this IEnumerable<K> enumerable, Func<K, V> values)
        where K : notnull
        => new(enumerable.Select(x => new KeyValuePair<K, V>(x, values(x))));
    public static string Repeated<T>(this string s, T ct)
        where T : INumber<T>
    {
        string result = "";
        for (T i = T.Zero; i < ct; i++) result += s;
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
    public static T GreatestCommonDivisor<T>(T a, T b)
        where T : INumber<T>
    {
        static (T large, T small) sort(T a, T b)
            => a > b ? (a, b) : (b, a);
        (T large, T small) = sort(a, b);
        while (small > T.Zero)
            (large, small) = sort(small, large % small);
        return large;
    }
    public static T LeastCommonMultiple<T>(T a, T b)
        where T : INumber<T>
        => T.Abs(a) * (T.Abs(b) / GreatestCommonDivisor(a, b));
    public static T LeastCommonMultiple<T>(this IEnumerable<T> enumerable)
        where T : INumber<T>
    {
        if (enumerable.Count() < 2)
            throw new ArgumentException($"Cannot find the least common multiple of {enumerable.Count()} numbers!", nameof(enumerable));
        T result = LeastCommonMultiple(enumerable.First(), enumerable.Second());
        foreach (T item in enumerable.Skip(2))
            result = LeastCommonMultiple(result, item);
        return result;
    }
    public static T MaxOrZero<T>(this IEnumerable<T> enumerable)
        where T : INumber<T>
    {
        if (!enumerable.Any())
            return T.Zero;
        return enumerable.Max() ?? T.Zero;
    }
    public static string ReplaceBy(this string s, Func<char, char> replacer)
    {
        string result = "";
        foreach (char c in s)
            result += replacer(c);
        return result;
    }
    public static (T lo, T hi) QuadraticFormula<T>(T a, T b, T c)
        where T : INumber<T>, IFloatingPointIeee754<T>
    {
        // for most types, multiplying by 2 is faster than addition
        T two = T.CreateChecked(2), four = two * two;
        T discriminant = T.Sqrt((b * b) - four * a * c), denominator = two * a;
        T minusResult = (-b - discriminant) / denominator, plusResult = (-b + discriminant) / denominator;
        return minusResult < plusResult ? (minusResult, plusResult) : (plusResult, minusResult);
    }
    public static IEnumerable<(T value, NumberPair<int> point)> Enumerate<T>(this T[,] array)
    {
        for(int x = 0; x < array.GetLength(0); x++)
            for (int y = 0; y < array.GetLength(1); y++)
                yield return (array[x, y], (x, y));
    }
    public static int Width<T>(this T[,] array)
        => array.GetLength(0);
    public static int Height<T>(this T[,] array)
        => array.GetLength(1);
    public static bool IsBetween<T>(this T a, T b, T c, bool inclusive = false)
        where T : INumber<T>
    {
        (T hi, T lo) = (T.Max(b, c), T.Min(b, c));
        return (a > lo && a < hi) || (inclusive && (a == lo || a == hi));
    }
}