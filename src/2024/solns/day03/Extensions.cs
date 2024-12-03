using System.Text.RegularExpressions;

namespace d9.aoc._24.day03;
internal static partial class Extensions
{
    private static readonly Regex _mulRegex = GenerateMulRegex();
    public static Regex MulRegex => _mulRegex;
    public static object Evaluate(this string match)
    {
        if (match.TryEvaluateDoOrDont() is bool b)
            return b;
        return match.TryEvaluateMul() ?? throw new ArgumentException($"{match} is not a valid instruction!", nameof(match));
    }
    public static bool? TryEvaluateDoOrDont(this string match)
    {
        if (match is not "do()" or "dont()")
            return null;
        return match == "do()";
    }
    public static int? TryEvaluateMul(this string match)
    {
        if (!MulRegex.IsMatch(match))
            return null;
        List<string> split = match.SplitAndTrim("(", ",", ")");
        return int.Parse(split[1]) * int.Parse(split[2]);
    }
    [GeneratedRegex(@"mul\(\d+,\d+\)")]
    private static partial Regex GenerateMulRegex();
}
