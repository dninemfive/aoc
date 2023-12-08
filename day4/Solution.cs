namespace d9.aoc._23.day4;
public static class Solution
{
    [SolutionToProblem(4)]
    public static IEnumerable<object> Solve(string[] inputLines)
    {
        ScratchCardCollection scratchCards = new(inputLines.Select(x => new ScratchCard(x)));
        yield return scratchCards.Select(x => x.Value).Sum();
        yield return scratchCards.TotalWonCards;
    }
}