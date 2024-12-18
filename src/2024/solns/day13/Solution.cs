
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Text.RegularExpressions;

namespace d9.aoc._24.day13;
[SolutionToProblem(13)]
[SampleResults(480L)]
[FinalResults(40069L)]
internal class Solution : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(params string[] lines)
    {
        IEnumerable<ClawMachine<long>> clawMachines1 = lines.Chunk(4)
                                                   //.Select(x => x.Where(y => !y.IsNullOrEmpty()))
                                                     .Select(ClawMachine<long>.FromLines);
        yield return clawMachines1.Select(x => x.CheapestComboCost()).Sum();
        yield break;
        Point<long> offset = (10000000000000L, 10000000000000L);
        IEnumerable<ClawMachine<long>> clawMachines2 = clawMachines1.Select(x => new ClawMachine<long>([x.CheapButton, x.ExpensiveButton], x.Prize + offset));
        yield return clawMachines2.Select(x => x.CheapestComboCost()).Sum();
    }
}