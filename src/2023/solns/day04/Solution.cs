using d9.aoc.core;

namespace d9.aoc._23.day04;
[SolutionToProblem(4)]
public class Solution : AocSolution
{
    public override IEnumerable<AocPartResultValue> Solve(string[] inputLines)
    {
        ScratchCardCollection scratchCards = new(inputLines.Select(x => new ScratchCard(x)));
        yield return "preinit";
        yield return scratchCards.Select(x => x.Value).Sum();
        yield return scratchCards.TotalWonCards;
    }
}