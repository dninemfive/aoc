using System.Numerics;

namespace d9.aoc._24.day11;
[SolutionToProblem(11)]
internal class Solution(params string[] lines)
    : AocSolution
{
    public static readonly string DebugFolder = Path.Join("_debug", "day11");
    public readonly Blink Blink = new(lines.First().ToMany<BigInteger>());
    [ExpectedResults(sample: 55312, final: 220722)]
    public override AocPartResultValue? Part1()
        => Blink.GenerateSuccessor(25).Count;
    [ExpectedResults(final: "261952051690787")]
    public override AocPartResultValue? Part2()
        => Blink.GenerateSuccessor(75).Count;
}

