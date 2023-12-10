namespace d9.aoc._23;
public readonly struct Point(int x, int y)
{
    public readonly int X = x;
    public readonly int Y = y;
    public Point((int x, int y) tuple) : this(tuple.x, tuple.y) { }
    public static implicit operator Point((int x, int y) tuple)
        => new(tuple.x, tuple.y);
    public static implicit operator (int x, int y)(Point p)
        => (p.X, p.Y);
    public static Point operator +(Point a, Point b)
        => new(a.X + b.X, a.Y + b.Y);
    public void Deconstruct(out int x, out int y)
    {
        x = X;
        y = Y;
    }
    public override string ToString()
        => $"({X}, {Y})";
}