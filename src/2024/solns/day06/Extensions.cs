using Direction = d9.aoc.core.Point<int>;
using Position = d9.aoc.core.Point<int>;
namespace d9.aoc._24.day06; internal static class Extensions
{
    public static Position GuardPosition(this Grid<char> grid)
        => grid.AllPoints.Where(x => grid[x].IsGuard()).First();
    public static Guard FindGuard(this Grid<char> grid)
    {
        Position pos = grid.GuardPosition();
        return new(pos, grid[pos]);
    }
    public static Direction Direction(this char c)
    {
        foreach ((Direction k, char v) in Guard.DirectionMap)
        {
            if (v == c)
                return k;
        }
        throw new ArgumentException($"'{c}' does not correspond to a direction!", nameof(c));
    }
    public static bool IsGuard(this char c)
        => c is '^' or '>' or 'v' or '<';
    public static bool IsObstacle(this char c)
        => c is '#';
    public static char? CellInFrontOf(this Grid<char> grid, Position cell, Direction direction)
        => grid.TryGet(cell + direction, out char? c) ? c : null;
    public static char? CellInFrontOf(this Grid<char> grid, Guard guard)
        => grid.CellInFrontOf(guard.Position, guard.Direction);
    public static Direction RotateClockwise(this Direction d)
        => d switch
        {
            (0, 1) => (-1, 0),
            (1, 0) => (0, 1),
            (0, -1) => (1, 0),
            (-1, 0) => (0, -1),
            _ => throw new ArgumentException($"{d} is not a direction i know how to rotate, sorry!", nameof(d))
        };
    public static Guard RotateClockwise(this Guard guard)
        => new(guard.Position, guard.Direction.RotateClockwise());
    public static Grid<char> CopyWith(this Grid<char> grid, Guard g)
        => grid.CopyWith((g.Position, g.Character));
}