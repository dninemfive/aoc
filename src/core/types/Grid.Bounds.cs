using d9.utl;
using Point = d9.aoc.core.Point<int>;

namespace d9.aoc.core;
public readonly partial struct Grid<T>
    where T : struct
{
    public bool Contains(Point p)
        => p.X >= 0 && p.X < Width && p.Y >= 0 && p.Y < Height;
    public bool IsOnEdge(Point point)
        => point.X == 0 || point.X == Width - 1 || point.Y == 0 || point.Y == Height - 1;
    public Point ClampInBounds(Point p)
        => (p.X.Clamp(0, Width - 1), p.Y.Clamp(0, Height - 1));
}