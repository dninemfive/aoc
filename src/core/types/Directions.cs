namespace d9.aoc.core;
public static class Directions
{
    public static readonly Point<int> Up     = ( 0,  1),
                                      Right  = ( 1,  0),
                                      Down   = ( 0, -1),
                                      Left   = (-1,  0);
    public static IEnumerable<Point<int>> Clockwise
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
    public static IEnumerable<Point<int>> Counterclockwise => Clockwise.Reverse();
}
