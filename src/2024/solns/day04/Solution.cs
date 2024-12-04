using Coordinate = d9.aoc.core.Point<int>;
using Direction  = d9.aoc.core.Point<int>;
using Directions = d9.aoc.core.Directions<int>;

namespace d9.aoc._24.day04;
[SolutionToProblem(4)]
internal class Solution : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(params string[] lines)
    {
        Grid<char> crossword = Grid<char>.From(lines);
        yield return "preinit";
        yield return crossword.AllPossibleWordStarts()
                              .Count(x => crossword.WordStartsAtLocation("XMAS", x.start, x.direction));
        yield return crossword.AllPoints
                              .Where(x => !crossword.IsEdge(x))
                              .Select(x => crossword.X_MASesAt(x))
                              .Sum();
    }
}
