using Point = d9.aoc.core.Point<int>;

namespace d9.aoc.core;
public readonly partial struct Grid<T>
    where T : struct
{
    public int Height
        => _grid.Height();
    public int Width
        => _grid.Width();
    public IEnumerable<Point> AllPoints
    {
        get
        {
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    yield return (x, y);
        }
    }
    public IEnumerable<(T[] column, int index)> Columns
    {
        get
        {
            for (int i = 0; i < Height; i++)
                yield return (GetColumn(i).ToArray(), i);
        }
    }
    public IEnumerable<(T[] row, int index)> Rows
    {
        get
        {
            for (int i = 0; i < Width; i++)
                yield return (GetRow(i).ToArray(), i);
        }
    }
}