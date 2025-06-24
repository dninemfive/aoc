using d9.aoc.core;
using d9.aoc.core.utils;

namespace d9.aoc._23.day10;
[SolutionToProblem(10)]
public class Solution : AocSolution
{
    private static Grid<char> _grid;
#pragma warning disable IDE1006 // Naming Styles
    private const char UPDOWN       = '│', 
                       LEFTRIGHT    = '─', 
                       UPRIGHT      = '└',
                       UPLEFT       = '┘', 
                       DOWNLEFT     = '┐', 
                       DOWNRIGHT    = '┌', 
                       START        = '█', 
                       EMPTY        = ' ';
#pragma warning restore IDE1006 // Naming Styles
    public override IEnumerable<AocPartResultValue> Solve(string[] input)
    {
        _grid = Grid<char>.From(input.Select(x => x.ReplaceWith(BoxDrawingEquivalent)).ToArray());
        yield return (0b0, "preinit");
        yield return Part1();
    }
    public static char BoxDrawingEquivalent(char c) => c switch
    {
        '|' => UPDOWN,
        '-' => LEFTRIGHT,
        'L' => UPRIGHT,
        'J' => UPLEFT,
        '7' => DOWNLEFT,
        'F' => DOWNRIGHT,
        'S' => START,
        '.' => EMPTY,
        _ => throw new ArgumentOutOfRangeException(nameof(c))
    };
    public static IEnumerable<Direction> Directions(char c)
    {
        if (c is '|' or '-' or 'L' or 'J' or '7' or 'F' or 'S' or '.')
            c = BoxDrawingEquivalent(c);
        if (c is UPDOWN or UPRIGHT or UPLEFT or START)
            yield return Direction.Up;
        if (c is LEFTRIGHT or UPRIGHT or DOWNRIGHT or START)
            yield return Direction.Right;
        if (c is UPDOWN or DOWNLEFT or DOWNRIGHT or START)
            yield return Direction.Down;
        if (c is LEFTRIGHT or UPLEFT or DOWNLEFT or START)
            yield return Direction.Left;
    }
    public static bool PointsIn(char c, Direction d) 
        => Directions(c).Contains(d);
    public static Point<int> Offset(Direction d) => d switch {
        Direction.Up => (0, -1),
        Direction.Right => (1, 0),
        Direction.Down => (0, 1),
        Direction.Left => (-1, 0),
        _ => throw new ArgumentOutOfRangeException(nameof(d))
    };
    public static Direction Reverse(Direction d) => d switch
    {
        Direction.Up => Direction.Down,
        Direction.Right => Direction.Left,
        Direction.Down => Direction.Up,
        Direction.Left => Direction.Right,
        _ => throw new ArgumentOutOfRangeException(nameof(d))
    };
    public static IEnumerable<Point<int>> ConnectedNeighbors(Point<int> p)
    {
        char c = _grid[p];
        foreach (Direction d in Directions(c))
        {
            Point<int> neighbor = p + Offset(d);
            if (_grid.Contains(neighbor) && PointsIn(_grid[neighbor], Reverse(d)))
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
    public static Point<int> FindStart()
    {
        foreach (Point<int> p in _grid.AllPoints)
            if (_grid[p] is 'S' or START)
                return p;
        throw new Exception($"Could not find starting NumberPair<int>!");
    }
    public static string Info(Point<int> point)
        => $"{_grid[point]}({point.X,3}, {point.Y,3})";
    public static int Part1()
    {
        int highestDistance = int.MinValue;
        HashSet<Point<int>> visitedPoints = new();
        Queue<(Point<int> point, int distance)> queue = new();
        Grid<char> debugGrid = Grid<char>.Of(' ', _grid.Width, _grid.Height);
        void push(Point<int> p, int distance)
        {
            if (visitedPoints.Contains(p))
                return;
            queue.Enqueue((p, distance));
            visitedPoints.Add(p);
        }
        push(FindStart(), 0);
        while(queue.Any())
        {
            (Point<int> point, int distance) = queue.Dequeue();
            debugGrid = debugGrid.CopyWith((point, _grid[point]));
            highestDistance = int.Max(distance, highestDistance);
            foreach (Point<int> neighbor in ConnectedNeighbors(point))
                push(neighbor, distance + 1);
        }
        File.WriteAllText("loop visualization.txt", debugGrid.LayOut());
        return highestDistance;
    }
}
