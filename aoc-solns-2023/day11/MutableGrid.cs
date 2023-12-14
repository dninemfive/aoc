namespace d9.aoc._23.day11;
public class MutableGrid<T>(int width, int height)
{
    private T[,] _grid = new T[width, height];
    public int Width => _grid.GetLength(0);
    public int Height => _grid.GetLength(1);
    public MutableGrid(T[,] sourceGrid) : this(sourceGrid.GetLength(0), sourceGrid.GetLength(1))
    {
        foreach ((T value, Point p) in sourceGrid.Enumerate())
            this[p] = value;
    }
    public T this[Point p]
    {
        get => _grid[p.X, p.Y];
        set => _grid[p.X, p.Y] = value;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="row">The index of the new row. This will be <em>before</em> the row currently occupying this index.</param>
    /// <param name="value"></param>
    /// <returns></returns>
    public void InsertRow(int row = 0)
    {
        T[,] oldGrid = _grid;
        _grid = new T[Width, Height + 1];
        for(int x = 0; x < Width; x++) 
            for(int y = 0; y < Height; y++)
            {
                if (y == row) continue;
                int y_ = y <= row ? y : y + 1;
                _grid[x, y] = oldGrid[x, y_];
            }
    }
}
