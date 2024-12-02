using d9.aoc.core;
using System.Numerics;

namespace d9.aoc._23.day09;
[SolutionToProblem(9)]
public class Solution : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(string[] lines)
    {
        IEnumerable<Sequence<int>> sequences = lines.Select(x => new Sequence<int>(x.ToMany<int>()));
        yield return "preinit";
        yield return sequences.Select(x => x.Next()).Sum();
        yield return sequences.Select(x => x.Prev()).Sum();
    }
}