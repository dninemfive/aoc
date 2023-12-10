﻿namespace d9.aoc._23.day3;
public static class Solution
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
        foreach (Point p in plan.AllPoints)
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
        foreach (Point p in plan.GearLocations())
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

        while (digitLocations.Any())
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
        for (int xi = left; xi <= right; xi++)
        {
            number += grid[xi, y];
        }
        return (int.Parse(number), left, right);
    }
}