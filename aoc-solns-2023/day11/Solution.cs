namespace d9.aoc._23.day11;
public static class Solution
{
    private static Grid<char> _grid;
    [SolutionToProblem(11)]
    public static IEnumerable<object> Solve(string[] lines)
    {
        _grid = Grid<char>.From(lines);
        yield return _grid.GalaxyLocations().ToList().UniquePairs().Select(GalaxyDistance).Sum();
    }
    public static IEnumerable<Point<int>> GalaxyLocations(this Grid<char> grid)
        => grid.AllPoints.Where(p => grid[p] == '#');
    public static IEnumerable<(Point<int> a, Point<int> b)> UniquePairs(this List<Point<int>> Points)
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
    public static IEnumerable<int> EmptyRowIndices
        => _grid.Rows.Where(x => x.row.All(y => y == '.')).Select(x => x.index);
    public static IEnumerable<int> EmptyColumnIndices
        => _grid.Columns.Where(x => x.column.All(y => y == '.')).Select(x => x.index);
    public static int GalaxyDistance(this (Point<int> a, Point<int> b) pair, int expansionFactor = 2)
    {
        (Point<int> a, Point<int> b) = pair;
        int emptySpacesBetweenPoints = EmptyRowIndices.Where(i => i.IsBetween(a.Y, b.Y)).Count()
                                     + EmptyColumnIndices.Where(i => i.IsBetween(a.X, b.X)).Count();
        return a.ManhattanDistanceFrom(b) + emptySpacesBetweenPoints * expansionFactor;
    }
}
