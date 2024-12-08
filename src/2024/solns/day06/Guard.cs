using Direction = d9.aoc.core.Point<int>;
using Directions = d9.aoc.core.Directions<int>;
using Position = d9.aoc.core.Point<int>;
namespace d9.aoc._24.day06;
internal record Guard(Position Position, Direction Direction)
{
    internal static readonly Dictionary<Direction, char> DirectionMap = new()
    {
        { Directions.Down,    '^' },
        { Directions.Right, '>' },
        { Directions.Up,  'v' },
        { Directions.Left,  '<' }
    };
    internal Guard(Position position, char c) : this(position, c.Direction()) { }
    public char Character
        => DirectionMap[Direction];
    public override string ToString()
        => $"{Character} {Position}";
    public Position Ahead
        => Position + Direction;
}