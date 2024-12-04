using System.Diagnostics.CodeAnalysis;
using Point = d9.aoc.core.Point<int>;

namespace d9.aoc.core;
public readonly partial struct Grid<T>
    where T : struct
{
    public T this[int x, int y] => _grid[x, y];
    public T this[Point p] => _grid[p.X, p.Y];
    public IEnumerable<Point> PointsAdjacentTo(Point point, bool includeSelf = false)
    {
        for (int xo = -1; xo <= 1; xo++)
            for (int yo = -1; yo <= 1; yo++)
            {
                if (!includeSelf && xo is 0 && yo is 0)
                    continue;
                Point cur = point + (xo, yo);
                if (Contains(cur))
                    yield return cur;
            }
    }
    public IEnumerable<T> ValuesAdjacentTo(Point point, bool includeSelf = false)
    {
        // "lambda methods (&c) don't have access to `this`" for some reason
        Grid<T> _this = this;
        return PointsAdjacentTo(point, includeSelf).Select(x => _this[x]);
    }
    public IEnumerable<T> GetColumn(int columnIndex)
    {
        for (int y = 0; y < Height; y++)
            yield return _grid[columnIndex, y];
    }
    public IEnumerable<T> GetRow(int rowIndex)
    {
        for (int x = 0; x < Width; x++)
            yield return _grid[x, rowIndex];
    }
    public bool TryGet(Point coordinate, [NotNullWhen(true)] out T? result)
    {
        bool inbounds = Contains(coordinate);
        result = inbounds ? this[coordinate] : default;
        return inbounds;
    }
}