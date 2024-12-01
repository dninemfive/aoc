using d9.aoc.core;

namespace d9.aoc._23.day11;
public static class Solution
{
    private static Grid<char> _grid;
#pragma warning disable CS8618 // Initialized in .Solve() and other things are not public
    private static HashSet<int> _emptyRows, _emptyColumns;
#pragma warning restore CS8618
    [SolutionToProblem(11, true)]
    public static IEnumerable<object> Solve(string[] lines)
    {
        _grid = Grid<char>.From(lines);
        (_emptyRows, _emptyColumns) = (EmptyRowIndices.ToHashSet(), EmptyColumnIndices.ToHashSet());
        List<(Point<int> a, Point<int> b)> uniquePairs = _grid.GalaxyLocations()
                                                    .ToList()
                                                    .UniquePairs()
                                                    .ToList();
        yield return 0b0;
        yield return uniquePairs.Select(x => x.GalaxyDistance(2)).Sum();
        yield return uniquePairs.Select(x => x.GalaxyDistance((int)1e6)).Sum();
    }
    private static IEnumerable<Point<int>> GalaxyLocations(this Grid<char> grid)
        => grid.AllPoints.Where(p => grid[p] == '#');
    private static IEnumerable<(Point<int> a, Point<int> b)> UniquePairs(this List<Point<int>> Points)
    {
        HashSet<(Point<int> a, Point<int> b)> seenPairs = new();
        bool hasBeenSeen(Point<int> a, Point<int> b) => seenPairs.Contains((a, b));
        for(int i = 0; i <  Points.Count; i++)
        {
            for(int j = i + 1; j < Points.Count; j++)
            {
                (Point<int> a, Point<int> b) = (Points[i], Points[j]);
                if (hasBeenSeen(a, b))
                    continue;
                yield return (a, b);
                seenPairs.Add((a, b));
                seenPairs.Add((b, a));
            }
        }
    }
    private static IEnumerable<int> EmptyRowIndices
        => _grid.Rows.Where(r => r.row.All(x => x == '.')).Select(r => r.index);
    private static IEnumerable<int> EmptyColumnIndices
        => _grid.Columns.Where(c => c.column.All(x => x == '.')).Select(c => c.index);
    private static long GalaxyDistance(this (Point<int> a, Point<int> b) pair, int expansionFactor = 2)
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
