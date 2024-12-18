using System.Numerics;
using System.Text.RegularExpressions;

namespace d9.aoc._24.day13;
internal partial record Button<T>(Point<T> Offset, string Name)
    where T : INumber<T>
{
    public static readonly Regex ButtonRegex = GenerateButtonRegex();
    public T Cost => Name switch
    {
        "A" => T.CreateChecked(3),
        "B" => T.One,
        _ => throw new Exception($"{Name} is not a valid button name!")
    };
    public static Button<T>? FromLine(string line)
    {
        MatchCollection matches = ButtonRegex.Matches(line);
        if (!matches.Any())
            return null;
        List<string> groups = matches.First()
                                     .Groups
                                     .Values
                                     .Skip(1)
                                     .Select(x => x.Value)
                                     .ToList();
        return new((T.Parse(groups[1], null), T.Parse(groups[2], null)), groups[0]);
    }
    public override string ToString()
        => $"Button {Name} (${Cost}, {Offset})";
    public static implicit operator Point<T>(Button<T> button)
        => button.Offset;
    [GeneratedRegex(@"Button (.): X\+(\d+), Y\+(\d+)")]
    private static partial Regex GenerateButtonRegex();
}