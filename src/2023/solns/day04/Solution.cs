using d9.aoc.core;

namespace d9.aoc._23.day04;
[SolutionToProblem(4)]
public class Solution(string[] lines)
    : AocSolution
{
    public readonly ScratchCardCollection ScratchCards = new(lines.Select(x => new ScratchCard(x)));
    [ExpectedResults(25004)]
    public override AocPartResultValue? Part1()
        => ScratchCards.Select(x => x.Value).Sum();
    [ExpectedResults(14427616)]
    public override AocPartResultValue? Part2()
        => ScratchCards.TotalWonCards;
}