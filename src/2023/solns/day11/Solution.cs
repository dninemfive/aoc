using d9.aoc.core;

namespace d9.aoc._23.day11;
[SolutionToProblem(11)]
public class Solution : AocSolution
{
    private static Grid<char> _grid;
#pragma warning disable CS8618 // Initialized in .Solve() and other things are not public
    private static HashSet<int> _emptyRows, _emptyColumns;
#pragma warning restore CS8618
    public override IEnumerable<AocPartResultValue> Solve(string[] lines)
    {
        _grid = Grid<char>.From(lines);
        (_emptyRows, _emptyColumns) = (EmptyRowIndices.ToHashSet(), EmptyColumnIndices.ToHashSet());
        List<(Point<int> a, Point<int> b)> uniquePairs = _grid.GalaxyLocations()
                                                    .ToList()
                                                    .UniquePairs()
                                                    .ToList();
        yield return (0b0, "preinit");
        yield return uniquePairs.Select(x => GalaxyDistance(x, 2)).Sum();
        yield return uniquePairs.Select(x => GalaxyDistance(x, (int)1e6)).Sum();
    }
    private static IEnumerable<int> EmptyRowIndices
        => _grid.Rows.Where(r => r.row.All(x => x == '.')).Select(r => r.index);
    private static IEnumerable<int> EmptyColumnIndices
        => _grid.Columns.Where(c => c.column.All(x => x == '.')).Select(c => c.index);
    private static long GalaxyDistance((Point<int> a, Point<int> b) pair, int expansionFactor = 2)
    {
        ((int x1, int y1), (int x2, int y2)) = pair;
        int x = x1, 
            dX = Math.Sign(x2 - x1), 
            y = y1, 
            dY = Math.Sign(y2 - y1);
        long distance = 0;
        while(x != x2)
        {
            distance += _emptyColumns.Contains(x) ? expansionFactor : 1;
            x += dX;
        }
        while (y != y2)
        {
            distance += _emptyRows.Contains(y) ? expansionFactor : 1;
            y += dY;
        }
        return distance;
    }
}
