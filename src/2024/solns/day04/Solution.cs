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
        yield return AllPossibleWordStarts(crossword).Count(x => WordStartsAtLocation(crossword, "XMAS", x.start, x.direction));
    }
    public bool WordStartsAtLocation(Grid<char> grid, string word, Coordinate start, Direction direction)
    {
        Coordinate cur = start;
        foreach(char c in word)
        {
            if (!grid.HasInBounds(cur) || grid[cur] != c)
                return false;
            cur += direction;
        }
        return true;
    }
    public IEnumerable<(Coordinate start, Direction direction)> AllPossibleWordStarts(Grid<char> grid)
    {
        foreach (Coordinate start in grid.AllPoints)
            foreach (Direction direction in Directions.Clockwise)
                yield return (start, direction);
    }
    public bool HasMas(Grid<char> grid, Coordinate start, Direction direction)
    {
        if (!grid.TryGet(start, out char? c) || c != 'A')
            return false;
        return
    }
}
