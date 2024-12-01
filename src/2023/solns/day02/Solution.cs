using d9.aoc.core;

namespace d9.aoc._23.day2;
public static class Solution
{
    [SolutionToProblem(2, true)]
    public static IEnumerable<object> Solve(string[] lines)
    {
        IEnumerable<Game> games = lines.Select(x => new Game(x));
        yield return 0b0;
        yield return games.Where(x => x.PossibleWith(("red", 12), ("green", 13), ("blue", 14)))
                          .Sum(x => x.Id);
        yield return games.Select(x => x.MinimumRequiredColors)
                          .Sum(x => x.Power());
    }
    public static int Power(this Dictionary<string, int> cubeSet)
        => cubeSet.Values.Aggregate((x, y) => x * y);
}