using System.Numerics;

namespace d9.aoc._24.day11;
[SolutionToProblem(11)]
[SampleResults(55312)]
[FinalResults(220722)]//, 261952051690787)]
internal class Solution : AocSolution
{
    public static readonly string DebugFolder = Path.Join("_debug", "day11");
    public override IEnumerable<AocPartialResult> Solve(params string[] lines)
    {
        Blink blink = new(lines.First().ToMany<BigInteger>());
        blink = blink.GenerateSuccessor(25);
        yield return blink.Count;
        blink = blink.GenerateSuccessor(50);
        yield return blink.Count;
    }
}

