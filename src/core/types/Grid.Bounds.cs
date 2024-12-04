using System.Diagnostics.CodeAnalysis;
using Point = d9.aoc.core.Point<int>;

namespace d9.aoc.core;
public readonly partial struct Grid<T>
    where T : struct
{
    public bool Contains(Point p)
        => p.X >= 0 && p.X < Width && p.Y >= 0 && p.Y < Height;
    public bool IsOnEdge(Point point)
        => point.X == 0 || point.X == Width - 1 || point.Y == 0 || point.Y == Height - 1;
}