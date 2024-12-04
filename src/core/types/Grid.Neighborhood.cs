using Point = d9.aoc.core.Point<int>;

namespace d9.aoc.core;
public readonly partial struct Grid<T>
    where T : struct
{
    public static IEnumerable<IEnumerable<Point>> NeighborhoodOffsets(int radius = 1)
    {
        if (radius < 1)
            throw new ArgumentException($"Cannot get a neighborhood with radius {radius}!");
        IEnumerable<Point> generateRow(int y)
        {
            for (int x = -radius; x <= radius; x++)
                yield return (x, y);
        }
        for (int y = -radius; y <= radius; y++)
            yield return generateRow(y);
    }
    public IEnumerable<IEnumerable<Point>> NeighborhoodPointsOf(Point point, int radius = 1)
    {
        Func<Point, bool> contains = Contains;
        IEnumerable<Point> generateRow(IEnumerable<Point> row)
        {
            foreach(Point offset in row)
            {
                Point neighbor = point + offset;
                if (contains(neighbor))
                    yield return neighbor;
            }
        }
        foreach (IEnumerable<Point> row in NeighborhoodOffsets(radius))
            yield return generateRow(row);
    }
    public IEnumerable<T> GetMany(IEnumerable<Point> points)
    {
        foreach (Point p in points)
            yield return this[p];
    }
    public IEnumerable<IEnumerable<T>> NeighborhoodOf(Point point, int radius = 1)
    {
        foreach (IEnumerable<Point> row in NeighborhoodPointsOf(point, radius))
            yield return GetMany(row);
    }
}