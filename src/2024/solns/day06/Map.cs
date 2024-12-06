using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Position      = d9.aoc.core.Point<int>;
using Direction     = d9.aoc.core.Point<int>;
using Directions    = d9.aoc.core.Directions<int>;
namespace d9.aoc._24.day06;
internal class Map(string[] lines)
{
    private Grid<char> _map = Grid<char>.From(lines);
    public Position? Step()
    {
        Position guardPosition = _map.GuardPosition();
        Direction guardDirection = _map[guardPosition].Direction();
        char? next = _map.CellInFrontOf(guardPosition, guardDirection);
        if (next is null)
        {
            return null;
        }
        if(next.IsObstacle())
        {
            return guardPosition + guardDirection.RotateClockwise();
        }
        else
        {
            return guardPosition + guardDirection;
        }
    }
}
internal static class Extensions
{
    public static Position GuardPosition(this Grid<char> grid)
        => grid.AllPoints.Where(x => grid[x].IsGuard()).First();
    public static Direction Direction(this char c)
        => c switch
        {
            '^'         => Directions.Up,
            '>'         => Directions.Right,
            'V' or 'v'  => Directions.Down,
            '<'         => Directions.Left,
            _           => throw new ArgumentException($"'{c}' does not correspond to a direction!")
        };
    public static bool IsGuard(this char c)
        => c is '^' or '>' or 'V' or 'v' or '<';
    public static bool IsObstacle(this char? c)
        => c is '#';
    public static char? CellInFrontOf(this Grid<char> grid, Position cell, Direction direction)
        => grid.TryGet(cell + direction, out char? c) ? c : null;
    public static Direction RotateClockwise(this Direction d)
        => d switch
        {
            ( 0,  1) => ( 1,  0),
            ( 1,  0) => ( 0, -1),
            ( 0, -1) => (-1,  0),
            (-1,  0) => ( 0,  1),
            _ => throw new ArgumentException(nameof(d))
        };
}