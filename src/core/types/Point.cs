using System.Numerics;

namespace d9.aoc.core;
public readonly struct Point<T>(T x, T y)
    where T : INumber<T>
{
    public readonly T X = x;
    public readonly T Y = y;
    public Point((T x, T y) tuple) : this(tuple.x, tuple.y) { }
    public static implicit operator Point<T>((T x, T y) tuple)
        => new(tuple.x, tuple.y);
    public static implicit operator (T x, T y)(Point<T> p)
        => (p.X, p.Y);
    public static Point<T> operator +(Point<T> a, Point<T> b)
        => new(a.X + b.X, a.Y + b.Y);
    public static Point<T> operator -(Point<T> a, Point<T> b)
        => new(a.X - b.X, a.Y - b.Y);
    public T ManhattanDistanceFrom(Point<T> other)
    {
        (T dX, T dY) = this - other;
        return T.Abs(dX) + T.Abs(dY);
    }
    public void Deconstruct(out T x, out T y)
    {
        x = X;
        y = Y;
    }
    public override string ToString()
        => $"({X}, {Y})";
}