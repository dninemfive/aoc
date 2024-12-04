using System.Numerics;

namespace d9.aoc.core;
public static class Directions<T>
    where T : INumber<T>
{
    public static readonly Point<T>   Up     = ( T.Zero,  T.One),
                                      Right  = ( T.One,   T.Zero),
                                      Down   = ( T.Zero, -T.One),
                                      Left   = (-T.One,   T.Zero);
    public static IEnumerable<Point<T>> Clockwise
    {
        get
        {
            yield return Up;
            yield return Up + Right;
            yield return Right;
            yield return Down + Right;
            yield return Down;
            yield return Down + Left;
            yield return Left;
            yield return Up + Left;
        }
    }
    public static IEnumerable<Point<T>> Counterclockwise => Clockwise.Reverse();
}
public static class DirectionExtensions
{
    public static Point<T> Opposite<T>(this Point<T> point)
        where T : INumber<T>
        => -point;
}
