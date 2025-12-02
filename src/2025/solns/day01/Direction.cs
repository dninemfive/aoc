using System.Numerics;

namespace d9.aoc._25.day01;
internal enum Direction
{
    Left = 'L',
    Right = 'R'
}
internal static class DirectionExtensions
{
    public static Direction ToDirection(this char c)
        => c switch
        {
            'L' => Direction.Left,
            'R' => Direction.Right,
            _ => throw new NotImplementedException()
        };
    public static T Sign<T>(this Direction direction)
        where T : INumber<T>
        => direction switch
        {
            Direction.Left => T.CreateChecked(-1),
            Direction.Right => T.One,
            _ => throw new NotImplementedException()
        };
}