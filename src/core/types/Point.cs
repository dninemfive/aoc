using System.Diagnostics.CodeAnalysis;
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
    public static Point<T> operator -(Point<T> p)
        => (-p.X, -p.Y);
    public static Point<T> operator *(Point<T> p, T factor)
        => (p.X * factor, p.Y * factor);
    public static Point<T> operator *(T factor, Point<T> p)
        => p * factor;
    public T Dot(Point<T> b)
    {
        (T a1, T a2) = this;
        (T b1, T b2) = b;
        return a1 * b1 + a2 * b2;
    }
    public Point<T> Abs
        => (T.Abs(X), T.Abs(Y));
    public static bool operator ==(Point<T> a, Point<T> b)
        => a.X == b.X && a.Y == b.Y;
    public static bool operator !=(Point<T> a, Point<T> b)
        => !(a == b);
    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is Point<T> other && this == other;
    public override int GetHashCode()
        => HashCode.Combine(X, Y);
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