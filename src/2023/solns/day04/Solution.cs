using d9.aoc.core;

namespace d9.aoc._23.day4;
public static class Solution
{
    [SolutionToProblem(4, true)]
    public static IEnumerable<object> Solve(string[] inputLines)
    {
        ScratchCardCollection scratchCards = new(inputLines.Select(x => new ScratchCard(x)));
        yield return 0b0;
        yield return scratchCards.Select(x => x.Value).Sum();
        yield return scratchCards.TotalWonCards;
    }
}