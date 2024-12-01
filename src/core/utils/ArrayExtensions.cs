namespace d9.aoc.core;
public static class ArrayExtensions
{
    public static IEnumerable<(T value, Point<int> point)> Enumerate<T>(this T[,] array)
    {
        for (int x = 0; x < array.GetLength(0); x++)
            for (int y = 0; y < array.GetLength(1); y++)
                yield return (array[x, y], (x, y));
    }
    public static int Width<T>(this T[,] array)
        => array.GetLength(0);
    public static int Height<T>(this T[,] array)
        => array.GetLength(1);
}
