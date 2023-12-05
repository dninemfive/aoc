using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace d9.aoc._23;
public static class Problem3
{
    [SolutionToProblem(3)]
    public static IEnumerable<object> Solve(string[] inputLines)
    {
        Grid<char> grid = MakeGridFrom(inputLines);
        yield return grid.PartNumbers().Sum();
        yield return grid.GearRatios().Sum();
    }
    public static Grid<char> MakeGridFrom(string[] lines)
    {
        int height = lines.Length, width = lines.Select(x => x.Length).Max();
        char[,] input = new char[width, height];
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                input[x, y] = lines[y][x];
        return input;
    }
    public static IEnumerable<int> PartNumbers(this Grid<char> plan)
    {
        string currentNumber = "";
        bool isPartNumber = false;
        foreach(Point p in plan.AllPoints)
        {
            char cur = plan[p];
            if (!cur.IsDigit())
            {
                if (currentNumber.Length > 0 && isPartNumber)
                    yield return int.Parse(currentNumber);
                (currentNumber, isPartNumber) = ("", false);
                continue;
            }
            currentNumber += cur;
            if (p.IsAdjacentToSymbolIn(plan))
                isPartNumber = true;
        } 
    }
    public static IEnumerable<int> GearRatios(this Grid<char> plan)
    {
        foreach(Point p in plan.GearLocations())
        {
            IEnumerable<int> adjacentNumbers = plan.NumbersAdjacentTo(p);
            if (adjacentNumbers.Count() != 2)
                continue;
            yield return adjacentNumbers.Aggregate((x, y) => x * y);
        }
    }
    public static bool IsSymbol(this char c) 
        => !c.IsDigit() && c != '.';
    public static IEnumerable<Point> GearLocations(this Grid<char> grid)
        => grid.AllPoints.Where(p => grid[p] == '*');
    public static bool IsAdjacentToSymbolIn(this Point p, Grid<char> grid)
        => grid.ValuesAdjacentTo(p, includeSelf: true).Any(IsSymbol);
    public static IEnumerable<int> NumbersAdjacentTo(this Grid<char> grid, Point p)
    {
        List<Point> digitLocations = grid.PointsAdjacentTo(p)
                                         .Where(p2 => grid[p2].IsDigit())
                                         .ToList();
        void removePointsBetween(int left, int right, int y)
            => digitLocations.RemoveAll(p2 => p2.Y == y && p2.X >= left && p2.X <= right);

        while(digitLocations.Any())
        {
            Point p2 = digitLocations.First();
            (int value, int left, int right) = grid.EntireNumberIncluding(p2);
            yield return value;
            removePointsBetween(left, right, p2.Y);
        }
    }
#pragma warning disable IDE1006 // Naming Styles: consts should be all-caps (should switch to a better style enforcer tbh)
    private const int LEFT = -1, RIGHT = 1;
#pragma warning restore IDE1006 // Naming Styles
    public static (int value, int left, int right) EntireNumberIncluding(this Grid<char> grid, Point p)
    {
        (int x, int y) = p;
        char c = grid[p];
        if (!c.IsDigit())
            throw new Exception($"The character {c} at point {p} is not a digit!");
        int findEnd(int step)
        {
            int end = x;
            while (grid.HasInBounds((end + step, y)) && grid[end + step, y].IsDigit())
                end += step;
            return end;
        }
        int left = findEnd(LEFT), right = findEnd(RIGHT);
        string number = "";
        for(int xi = left; xi <= right; xi++)
        {
            number += grid[xi, y];
        }
        return (int.Parse(number), left, right);
    }
}
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
public readonly struct Grid<T>(T[,] grid)
{
    private readonly T[,] _grid = grid;
    public T this[int x, int y] => _grid[x, y];
    public T this[Point p] => _grid[p.X, p.Y];
    public static implicit operator T[,](Grid<T> grid) => grid._grid;
    public static implicit operator Grid<T>(T[,] grid) => new(grid);
    public int Width => _grid.GetLength(0);
    public int Height => _grid.GetLength(1);
    public bool HasInBounds(Point point)
    {
        (int x, int y) = point;
        return x >= 0 && x < Width && y >= 0 && y < Height;
    }
    public IEnumerable<Point> PointsAdjacentTo(Point point, bool includeSelf = false)
    {
        for(int xo = -1; xo <= 1; xo++)
            for(int yo = -1; yo <= 1; yo++)
            {
                if (!includeSelf && xo is 0 && yo is 0)
                    continue;
                Point cur = point + (xo, yo);
                if (HasInBounds(cur))
                    yield return cur;
            }
    }
    public IEnumerable<T> ValuesAdjacentTo(Point point, bool includeSelf = false)
    {
        // "lambda methods (&c) don't have access to `this`" for some reason
        Grid<T> _this = this;
        return PointsAdjacentTo(point, includeSelf).Select(x => _this[x]);
    }
    public IEnumerable<Point> AllPoints
    {
        get
        {
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    yield return (x, y);
        }
    }
}