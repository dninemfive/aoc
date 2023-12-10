using System.Linq;

namespace d9.aoc._23.day10;
public static class Solution
{
    private static Grid<char> _grid;
    private const char UPDOWN       = '│', 
                       LEFTRIGHT    = '─', 
                       UPRIGHT      = '└',
                       UPLEFT       = '┘', 
                       DOWNLEFT     = '┐', 
                       DOWNRIGHT    = '┌', 
                       START        = '*', 
                       EMPTY        = ' ';
    [SolutionToProblem(10)]
    public static IEnumerable<object> Solve(string[] input)
    {
        _grid = Grid<char>.From(input.Select(x => x.ReplaceBy(BoxDrawingEquivalent)).ToArray());
        File.WriteAllText(Path.Join(Program.INPUT_FOLDER, "readable day10 input.txt"), Grid<char>.LayoutString(_grid));
        yield return Part1();
    }
    public static char BoxDrawingEquivalent(this char c) => c switch
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
    public static IEnumerable<Direction> Directions(this char c)
    {
        if (c is '|' or '-' or 'L' or 'J' or '7' or 'F' or 'S' or '.')
            c = c.BoxDrawingEquivalent();
        if (c is UPDOWN or UPRIGHT or UPLEFT or START)
            yield return Direction.Up;
        if (c is LEFTRIGHT or UPRIGHT or DOWNRIGHT or START)
            yield return Direction.Right;
        if (c is UPDOWN or DOWNLEFT or DOWNRIGHT or START)
            yield return Direction.Down;
        if (c is LEFTRIGHT or UPLEFT or DOWNLEFT or START)
            yield return Direction.Left;
    }
    public static bool PointsIn(this char c, Direction d) 
        => c.Directions().Contains(d);
    public static Point Offset(this Direction d) => d switch {
        Direction.Up => (0, -1),
        Direction.Right => (1, 0),
        Direction.Down => (0, 1),
        Direction.Left => (-1, 0),
        _ => throw new ArgumentOutOfRangeException(nameof(d))
    };
    public static Direction Reverse(this Direction d) => d switch
    {
        Direction.Up => Direction.Down,
        Direction.Right => Direction.Left,
        Direction.Down => Direction.Up,
        Direction.Left => Direction.Right,
        _ => throw new ArgumentOutOfRangeException(nameof(d))
    };
    public static IEnumerable<Point> ConnectedNeighbors(this Point p)
    {
        char c = _grid[p];
        foreach (Direction d in c.Directions())
        {
            Point neighbor = p + d.Offset();
            if (_grid.HasInBounds(neighbor) && _grid[neighbor].PointsIn(d.Reverse()))
            {
                //Console.WriteLine($"{p.Info()} {d,-5} {d.Reverse(),-5} {neighbor.Info()}");
                yield return neighbor;
            }
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
            if (_grid[p] is 'S' or START)
                return p;
        throw new Exception($"Could not find starting point!");
    }
    public static string Info(this Point point)
        => $"{_grid[point]}({point.X,3}, {point.Y,3})";
    public static int Part1()
    {
        int highestDistance = int.MinValue;
        HashSet<Point> visitedPoints = new();
        Queue<(Point point, int distance)> queue = new();
        Grid<char> debugGrid = Grid<char>.Of(' ', _grid.Width, _grid.Height);
        void push(Point p, int distance)
        {
            if (visitedPoints.Contains(p))
                return;
            //Console.WriteLine($"\t{nameof(push)}({p.Info()}, {distance})");
            queue.Enqueue((p, distance));
            visitedPoints.Add(p);
        }
        push(FindStart(), 0);
        while(queue.Any())
        {
            //Console.WriteLine($"[{queue.ListNotation(x => x.point.Info())}]");
            (Point point, int distance) = queue.Dequeue();
            debugGrid = debugGrid.CopyWith((point, _grid[point]));
            //Console.WriteLine($"pop {point} {distance}");
            highestDistance = int.Max(distance, highestDistance);
            foreach (Point neighbor in point.ConnectedNeighbors())
                push(neighbor, distance + 1);
        }
        Console.WriteLine(Grid<char>.LayoutString(debugGrid));
        return highestDistance;
    }
}
