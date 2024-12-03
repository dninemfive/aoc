using System.Text.RegularExpressions;

namespace d9.aoc._24.day03;
[SolutionToProblem(3)]
internal class Solution : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(params string[] lines)
    {
        yield return lines.SelectMany(x => MulInstruction.Regex.Matches(x))
                          .Select(x => MulInstruction.FromMatch(x.Value).Value)
                          .Sum();
    }
}
