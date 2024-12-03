using System.Text.RegularExpressions;

namespace d9.aoc._24.day03;
[SolutionToProblem(3)]
internal partial class Solution : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(params string[] lines)
    {
        IEnumerable<object> matches = lines.SelectMany<object>(x => x.Evaluate());
        yield return "preinit";
        yield return matches.SelectMany(x => MulInstruction.Regex.Matches(x))
                            .Select(x => MulInstruction.FromMatch(x.Value).Value)
                            .Sum();
        IEnumerable<Match> matches = lines.SelectMany(x => AnyInstruction().Matches(x));
        Console.WriteLine(matches.ListNotation());
    }
    // til about the non-capturing modifier: https://stackoverflow.com/a/632248
    [GeneratedRegex(@"(?:mul\(\d+,\d+\)|do\(\)|dont\(\))")]
    private static partial Regex AnyInstruction();
}
