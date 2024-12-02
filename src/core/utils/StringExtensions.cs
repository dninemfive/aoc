using System.Globalization;

namespace d9.aoc.core;
public static class StringExtensions
{
    /// <summary>
    /// <see cref="SplitAndTrim(string, string[])">Splits and trims</see> the specified string using
    /// the specified <paramref name="delimiter"/>, skipping <paramref name="skip"/> entries and
    /// parsing each to <typeparamref name="T"/> using the specified <paramref name="formatProvider"/>,
    /// if any.
    /// </summary>
    /// <typeparam name="T">The type to which to parse each split entry.</typeparam>
    /// <param name="s">The string to split and trim.</param>
    /// <param name="delimiter">The delimiter by which to split each string.</param>
    /// <param name="formatProvider">The format provider to use to parse each entry.</param>
    /// <param name="skip">How many items at the beginning of the split results to skip.</param>
    /// <returns>The results of the splitting and parsing described above.</returns>
    public static IEnumerable<T> ToMany<T>(this string s, string delimiter = " ", IFormatProvider? formatProvider = null, int skip = 0)
        where T : IParsable<T>
        => s.SplitAndTrim(delimiter)
            .Skip(skip)
            .Select(x => T.Parse(x.Trim(), formatProvider ?? CultureInfo.InvariantCulture));
    /// <summary>
    /// Splits the string by each delimiter sequentially.
    /// </summary>
    /// <param name="s">The string to split.</param>
    /// <param name="delimiters">The delimiters by which to split <paramref name="s"/>.</param>
    /// <returns>The string split by <paramref name="delimiters"/>, trimming each and removing empty entries.</returns>
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
    public static T Parse<T>(this string s, IFormatProvider? formatProvider = null)
        where T : IParsable<T>
        => T.Parse(s, formatProvider ?? CultureInfo.InvariantCulture);
    public static string ReplaceWith(this string s, Func<char, char> replacer)
    {
        string result = "";
        foreach (char c in s)
            result += replacer(c);
        return result;
    }
    public static string Ancestor(this string path, int index)
    {
        DirectoryInfo result = new DirectoryInfo(path);
        for (int i = 0; i < index; i++)
            result = result.Parent!;
        return result.ToString();
    }
}