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
    public static int EmptyRowsBetween(int aY, int bY)
        => _grid.CountRowsWhereAll((t, _, y) => t == '.' && y.IsBetween(aY, bY));
    public static int EmptyColumnsBetween(int aX, int bX)
        => _grid.CountRowsWhereAll((t, x, _) => t == '.' && x.IsBetween(aX, bX));
    public static long GalaxyDistance(this (Point<int> a, Point<int> b) pair, int expansionFactor = 2)
    {
        (Point<int> a, Point<int> b) = pair;
        // int emptySpacesBetweenPoints = EmptyRowsBetween(a.Y, b.Y) + EmptyColumnsBetween(a.X, b.X);
        return a.ManhattanDistanceFrom(b); // + emptySpacesBetweenPoints * (expansionFactor - 1);
    }
}
