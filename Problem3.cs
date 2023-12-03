using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d9.aoc._23;
public static class Problem3
{
    [SolutionToProblem(3)]
    public static IEnumerable<object> Solve(string[] inputLines)
    {
        int height = inputLines.Length, width = inputLines.Select(x => x.Length).Max();
        char[,] grid = new char[width, height];
        for(int y = 0; y < height; y++)
            for(int x = 0; x < width; x++)
                grid[x, y] = inputLines[y][x];
        yield return grid.PartNumbers().Sum();
    }
    public static IEnumerable<int> PartNumbers(this char[,] plan)
    {
        string currentNumber = "";
        bool isPartNumber = false;
        foreach((int x, int y) in plan.AllPoints())
        {
            char cur = plan[x, y];
            // Console.WriteLine($"({x,3}, {y,3}): {cur} {(isPartNumber ? 'T' : ' ')} {currentNumber}");
            if (!cur.IsDigit())
            {
                if (currentNumber.Length > 0 && isPartNumber)
                    yield return int.Parse(currentNumber);
                (currentNumber, isPartNumber) = ("", false);
                continue;
            }
            currentNumber += cur;
            if ((x, y).IsAdjacentToSymbolIn(plan))
                isPartNumber = true;
        } 
    }
    public static IEnumerable<int> GearRatios(this char[,] plan)
    {
        foreach((int x, int y) in plan.GearLocations())
        {
            IEnumerable<int> adjacentNumbers = plan.NumbersAdjacentTo((x, y));
            if (adjacentNumbers.Count() != 2)
                continue;
            yield return adjacentNumbers.Aggregate((x, y) => x * y);
        }
    }
    public static bool IsSymbol(this char c) 
        => !c.IsDigit() && c != '.';
    public static bool IsDigit(this char c)
        => c is >= '0' and <= '9';
    public static bool IsAdjacentToSymbolIn(this Point p, char[,] array)
        => array.ValuesAdjacentTo(p, includeSelf: true).Any(IsSymbol);
    public static bool IsAdjacentToSymbolIn(this (int x, int y) p, char[,] array)
        => new Point(p).IsAdjacentToSymbolIn(array);
    public static IEnumerable<int> NumbersAdjacentTo(this char[,] array, Point p)
        // find adjacent points with digits
        // for each digit, go left until there stops being digits then go all the way right as normal
        // if two digits adjacent horizontally take the leftmost one (make sure to account for three adjacent)
        => throw new NotImplementedException();
    public static IEnumerable<Point> GearLocations(this char[,] array)
        => array.AllPointsAndValues()
                .Where(x => x.value == '*')
                .Select(x => x.point);
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
}
public static class ArrayUtils
{
    public static bool IsInBoundsOf<T>(this Point point, T[,] array)
    {
        (int x, int y) = point;
        return x >= 0 && x < array.GetLength(0) && y >= 0 && y < array.GetLength(1);
    }
    public static IEnumerable<Point> PointsAdjacentTo<T>(this T[,] array, Point point, bool includeSelf = false)
    {
        for(int xo = -1; xo <= 1; xo++)
        {
            for(int yo = -1; yo <= 1; yo++)
            {
                if (!includeSelf && xo is 0 && yo is 0)
                    continue;
                Point cur = point + (xo, yo);
                if (cur.IsInBoundsOf(array))
                    yield return cur;
            }
        }
    }
    public static IEnumerable<T> ValuesAdjacentTo<T>(this T[,] array, Point point, bool includeSelf = false)
        => array.PointsAdjacentTo(point, includeSelf).Select(p => array[p.X, p.Y]);
    public static IEnumerable<Point> AllPoints<T>(this T[,] array)
    {
        for (int y = 0; y < array.GetLength(1); y++)
            for (int x = 0; x < array.GetLength(0); x++)
                yield return (x, y);
    }
    public static IEnumerable<(Point point, T value)> AllPointsAndValues<T>(this T[,] array)
        => array.AllPoints().Select(p => (p, array[p.X, p.Y]));
}