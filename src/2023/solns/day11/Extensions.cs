using d9.aoc.core;

namespace d9.aoc._23.day11;
internal static class Extensions
{
    internal static IEnumerable<Point<int>> GalaxyLocations(this Grid<char> grid)
        => grid.AllPoints.Where(p => grid[p] == '#');
    internal static IEnumerable<(Point<int> a, Point<int> b)> UniquePairs(this List<Point<int>> Points)
    {
        HashSet<(Point<int> a, Point<int> b)> seenPairs = new();
        bool hasBeenSeen(Point<int> a, Point<int> b) => seenPairs.Contains((a, b));
        for (int i = 0; i < Points.Count; i++)
        {
            for (int j = i + 1; j < Points.Count; j++)
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
    internal static long GalaxyDistance(this (Point<int> a, Point<int> b) pair, int expansionFactor = 2)
    {
        ((int x1, int y1), (int x2, int y2)) = pair;
        int x = x1,
            dX = Math.Sign(x2 - x1),
            y = y1,
            dY = Math.Sign(y2 - y1);
        long distance = 0;
        while (x != x2)
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
