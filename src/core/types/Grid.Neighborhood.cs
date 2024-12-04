using Point = d9.aoc.core.Point<int>;

namespace d9.aoc.core;
public readonly partial struct Grid<T>
    where T : struct
{
    public Grid<T> SubgridWithin(Point a, Point b)
    {
        (int ax, int ay) = ClampInBounds(a);
        (int bx, int by) = ClampInBounds(b);
        int x1 = Math.Min(ax, bx), x2 = Math.Max(ax, bx),
            y1 = Math.Min(ay, by), y2 = Math.Max(ay, by);
        T[,] result = new T[x2 - x1 + 1, y2 - y1 + 1];
        for (int x = x1; x <= x2; x++)
            for (int y = y1; y <= y2; y++)
                result[x - x1, y - y1] = this[x, y];
        return result;
    }
    public Grid<T> NeighborhoodOf(Point p, int radius = 1)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(radius, 1, nameof(radius));
        return SubgridWithin((p.X - radius, p.Y - radius), (p.X + radius, p.Y + radius));
    }
}