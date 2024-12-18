using System.Numerics;
using System.Text.RegularExpressions;

namespace d9.aoc._24.day13; internal partial record Prize<T>(T X, T Y)
    where T : INumber<T>
{
    public static readonly Regex PrizeRegex = GeneratePrizeRegex();
    public static Prize<T>? FromLine(string line)
    {
        MatchCollection matches = PrizeRegex.Matches(line);
        if (!matches.Any())
            return null;
        List<string> groups = matches.First()
                                     .Groups
                                     .Values
                                     .Skip(1)
                                     .Select(x => x.Value)
                                     .ToList();
        return new(T.Parse(groups.First(), null), T.Parse(groups.Second(), null));
    }
    public static implicit operator Point<T>(Prize<T> prize)
        => (prize.X, prize.Y);
    [GeneratedRegex(@"Prize: X=(\d+), Y=(\d+)")]
    private static partial Regex GeneratePrizeRegex();
}