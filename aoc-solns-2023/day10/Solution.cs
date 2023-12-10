﻿using System.Linq;

namespace d9.aoc._23.day10;
public static class Solution
{
    private static Grid<char> _grid;
    [SolutionToProblem(10)]
    public static IEnumerable<object> Solve(string[] input)
    {
        _grid = Grid<char>.From(input.Select(x => x.ReplaceBy(BoxDrawingEquivalent)).ToArray());
        File.WriteAllText(Path.Join(Program.INPUT_FOLDER, "readable day10 input.txt"), Grid<char>.LayoutString(_grid));
        yield return Part1();
    }
    public static char BoxDrawingEquivalent(this char c) => c switch
    {
        '|' => '│',
        '-' => '─',
        'L' => '└',
        'J' => '┘',
        '7' => '┐',
        'F' => '┌',
        'S' => '●',
        '.' => ' ',
        _ => throw new ArgumentOutOfRangeException(nameof(c))
    };
    public static IEnumerable<Direction> Directions(this char c)
    {
        if (c is '|' or '-' or 'L' or 'J' or '7' or 'F' or 'S' or '.')
            c = c.BoxDrawingEquivalent();
        if (c is '│' or '└' or '┘' or '●')
            yield return Direction.Up;
        if (c is '─' or '└' or '┌' or '●')
            yield return Direction.Right;
        if (c is '│' or '┐' or '┌' or '●')
            yield return Direction.Down;
        if (c is '─' or '┘' or '┐' or '●')
            yield return Direction.Left;
    }
    public static bool PointsIn(this char c, Direction d) 
        => c.Directions().Contains(d);
    public static Point Offset(this Direction d) => d switch {
        Direction.Up => (0, 1),
        Direction.Right => (1, 0),
        Direction.Down => (0, -1),
        Direction.Left => (-1, 0),
        _ => throw new ArgumentOutOfRangeException(nameof(d))
    };
    public static IEnumerable<Point> ConnectedNeighbors(this Point p)
    {
        char c = _grid[p];
        foreach (Direction d in c.Directions())
        {
            Point neighbor = p + d.Offset();
            if (_grid.HasInBounds(neighbor))
                yield return neighbor;
        }
    }
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    public static Point FindStart()
    {
        foreach (Point p in _grid.AllPoints)
            if (_grid[p] is 'S' or '●')
                return p;
        throw new Exception($"Could not find starting point!");
    }
    public static int Part1()
    {
        int highestDistance = int.MinValue;
        Queue<(Point point, int distance)> queue = new([(FindStart(), 0)]);
        while(queue.Any())
        {
            (Point point, int distance) = queue.Dequeue();
            highestDistance = int.Max(distance, highestDistance);
            foreach(Point neighbor in point.ConnectedNeighbors())
                queue.Enqueue((neighbor, distance + 1));
        }
        return highestDistance;
    }
}
