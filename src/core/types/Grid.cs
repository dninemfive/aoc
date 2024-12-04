using System.Diagnostics.CodeAnalysis;
using Point = d9.aoc.core.Point<int>;

namespace d9.aoc.core;
public readonly partial struct Grid<T>(T[,] grid)
    where T : struct
{
    private readonly T[,] _grid = grid;
    public T this[int x, int y] => _grid[x, y];
    public T this[Point p] => _grid[p.X, p.Y];
    public static implicit operator T[,](Grid<T> grid) => grid._grid;
    public static implicit operator Grid<T>(T[,] grid) => new(grid);
    public int Width => _grid.GetLength(0);
    public int Height => _grid.GetLength(1);
    public bool HasInBounds(Point point)
    {
        (int x, int y) = point;
        return x >= 0 && x < Width && y >= 0 && y < Height;
    }
    public IEnumerable<Point> PointsAdjacentTo(Point point, bool includeSelf = false)
    {
        for (int xo = -1; xo <= 1; xo++)
            for (int yo = -1; yo <= 1; yo++)
            {
                if (!includeSelf && xo is 0 && yo is 0)
                    continue;
                Point cur = point + (xo, yo);
                if (HasInBounds(cur))
                    yield return cur;
            }
    }
    public IEnumerable<T> ValuesAdjacentTo(Point point, bool includeSelf = false)
    {
        // "lambda methods (&c) don't have access to `this`" for some reason
        Grid<T> _this = this;
        return PointsAdjacentTo(point, includeSelf).Select(x => _this[x]);
    }
    public IEnumerable<Point> AllPoints
    {
        get
        {
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    yield return (x, y);
        }
    }
    public Grid<T> CopyWith(params (Point point, T item)[] changes)
    {
        T[,] newGrid = (T[,])_grid.Clone();
        foreach(((int x, int y), T item) in changes)
            newGrid[x, y] = item;
        return new(newGrid);
    }
    public static Grid<T> Of(T item, int width, int height)
    {
        T[,] grid = new T[width, height];
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                grid[x, y] = item;
        return grid;
    }
    public static Grid<char> From(string[] lines)
    {
        int height = lines.Length, width = lines.Select(x => x.Length).Max();
        char[,] input = new char[width, height];
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                input[x, y] = lines[y][x];
        return input;
    }
    public static string LayOut(Grid<char> grid)
    {
        string result = "";
        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
                result += grid[x, y];
            result += "\n";
        }
        return result[..^1];
    }
    public Grid<T> InsertRow(int newRowIndex, T defaultItem = default)
    {
        T[,] newGrid = new T[Width, Height + 1];
        foreach((T item, (int x, int y)) in _grid.Enumerate())
        {
            int y_ = y < newRowIndex ? y : y + 1;
            newGrid[x, y_] = item;
        }
        for(int x = 0; x < newGrid.Width(); x++)
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
    public IEnumerable<T> GetColumn(int columnIndex)
    {
        for (int y = 0; y < Height; y++)
            yield return _grid[columnIndex, y];
    }
    public IEnumerable<(T[] column, int index)> Columns
    {
        get
        {
            for (int i = 0; i < Height; i++)
                yield return (GetColumn(i).ToArray(), i);
        }
    }
    public int CountColumnsWhereAll(Func<T, int, int, bool> predicate)
    {
        int ct = 0;
        for (int x = 0; x < Width; x++)
        {
            bool broke = false;
            for (int y = 0; y < Height; y++)
                if (!predicate(_grid[x, y], x, y))
                {
                    broke = true;
                    break;
                }
            if (!broke) ct++;
        }
        return ct;
    }
    public IEnumerable<T> GetRow(int rowIndex)
    {
        for (int x = 0; x < Width; x++)
            yield return _grid[x, rowIndex];
    }
    public IEnumerable<(T[] row, int index)> Rows
    {
        get
        {
            for (int i = 0; i < Width; i++)
                yield return (GetRow(i).ToArray(), i);
        }
    }
    public int CountRowsWhereAll(Func<T, int, int, bool> predicate)
    {
        int ct = 0;
        for(int y = 0; y < Height; y++)
        {
            bool broke = false;
            for (int x = 0; x < Width; x++)
                if (!predicate(_grid[x, y], x, y))
                {
                    broke = true;
                    break;
                }
            if(!broke) ct++;
        }
        return ct;
    }
    public bool TryGet(Point coordinate, [NotNullWhen(true)] out T? result)
    {
        bool inbounds = HasInBounds(coordinate);
        result = inbounds ? this[coordinate] : default;
        return inbounds;
    }
    public bool IsEdge(Point point)
        => point.X == 0 || point.X == Width - 1 || point.Y == 0 || point.Y == Height - 1;
    public static IEnumerable<IEnumerable<Point>> NeighborhoodOffsets(int radius = 1)
    {
        if (radius < 1)
            throw new ArgumentException($"Cannot get a neighborhood with radius {radius}!");
        IEnumerable<Point> generateRow(int y)
        {
            for (int x = -radius; x <= radius; x++)
                yield return (x, y);
        }
        for (int y = -radius; y <= radius; y++)
            yield return generateRow(y);
    }
    public IEnumerable<IEnumerable<Point>> NeighborhoodPointsOf(Point point, int radius = 1)
    {
        Func<Point, bool> inBounds = HasInBounds;
        IEnumerable<Point> generateRow(IEnumerable<Point> row)
        {
            foreach(Point offset in row)
            {
                Point neighbor = point + offset;
                if (inBounds(neighbor))
                    yield return neighbor;
            }
        }
        foreach (IEnumerable<Point> row in NeighborhoodOffsets(radius))
            yield return generateRow(row);
    }
    public IEnumerable<T> GetMany(IEnumerable<Point> points)
    {
        foreach (Point p in points)
            yield return this[p];
    }
    public IEnumerable<IEnumerable<T>> NeighborhoodOf(Point point, int radius = 1)
    {
        foreach (IEnumerable<Point> row in NeighborhoodPointsOf(point, radius))
            yield return GetMany(row);
    }
}