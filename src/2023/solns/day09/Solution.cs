using d9.aoc.core;
using d9.utl;

namespace d9.aoc._23.day09;
[SolutionToProblem(9)]
public class Solution(string[] lines)
    : AocSolution
{
    public readonly IEnumerable<Sequence<int>> Sequences = lines.Select(x => new Sequence<int>(x.ToMany<int>()));

    [ExpectedResults(1993300041)]
    public override AocPartResultValue? Part1()
        => Sequences.Select(x => x.Next()).Sum();

    [ExpectedResults(1038)]
    public override AocPartResultValue? Part2()
        => Sequences.Select(x => x.Prev()).Sum();
}