using d9.aoc.core;

namespace d9.aoc._23.day02;
[SolutionToProblem(2)]
public class Solution(string[] lines) 
    : AocSolution
{
    public readonly IEnumerable<Game> Games = lines.Select(x => new Game(x));
    [ExpectedResults(2061)]
    public override AocPartResultValue? Part1()
        => Games.Where(x => x.PossibleWith(("red", 12), ("green", 13), ("blue", 14)))
                .Sum(x => x.Id);
    [ExpectedResults(72596)]
    public override AocPartResultValue? Part2()
        => Games.Select(x => x.MinimumRequiredColors)
                .Sum(x => x.Power());
}