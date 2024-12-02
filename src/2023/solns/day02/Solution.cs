using d9.aoc.core;

namespace d9.aoc._23.day02;
[SolutionToProblem(2)]
public class Solution : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(string[] lines)
    {
        IEnumerable<Game> games = lines.Select(x => new Game(x));
        yield return "preinit";
        yield return games.Where(x => x.PossibleWith(("red", 12), ("green", 13), ("blue", 14)))
                          .Sum(x => x.Id);
        yield return games.Select(x => x.MinimumRequiredColors)
                          .Sum(x => x.Power());
    }
}