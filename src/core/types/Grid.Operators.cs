using Point = d9.aoc.core.Point<int>;

namespace d9.aoc.core;
public readonly partial struct Grid<T>
    where T : struct
{
    public Grid<T> InsertRow(int newRowIndex, T defaultItem = default)
    {
        T[,] newGrid = new T[Width, Height + 1];
        foreach ((T item, (int x, int y)) in _grid.Enumerate())
        {
            int y_ = y < newRowIndex ? y : y + 1;
            newGrid[x, y_] = item;
        }
        for (int x = 0; x < newGrid.Width(); x++)
            newGrid[x, newRowIndex] = defaultItem;
        return newGrid;
    }
    public Grid<T> InsertColumn(int newColIndex, T defaultItem = default)
    {
        T[,] newGrid = new T[Width + 1, Height];
        foreach ((T item, (int x, int y)) in _grid.Enumerate())
        {
            int x_ = x < newColIndex ? x : x + 1;
            newGrid[x_, y] = item;
        }
        for (int y = 0; y < newGrid.Height(); y++)
            newGrid[newColIndex, y] = defaultItem;
        return newGrid;
    }
    public int CountRowsWhereAll(Func<T, int, int, bool> predicate)
    {
        int ct = 0;
        for (int y = 0; y < Height; y++)
        {
            bool broke = false;
            for (int x = 0; x < Width; x++)
                if (!predicate(_grid[x, y], x, y))
                {
                    broke = true;
                    break;
                }
            if (!broke)
                ct++;
        }
        return ct;
    }
    public Grid<U> Map<U>(Func<T, U> map)
        where U : struct
    {
        U[,] result = new U[Width, Height];
        foreach (Point p in AllPoints)
            result[p.X, p.Y] = map(this[p]);
        return result;
    }
    public Grid<T> Map(IReadOnlyDictionary<T, T> map)
        => Map(x => map.TryGetValue(x, out T val) ? val : x);
}
