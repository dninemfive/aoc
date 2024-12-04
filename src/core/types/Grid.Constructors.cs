using Point = d9.aoc.core.Point<int>;

namespace d9.aoc.core;
public readonly partial struct Grid<T>(T[,] grid)
    where T : struct
{
    private readonly T[,] _grid = grid;
    public static implicit operator T[,](Grid<T> grid) => grid._grid;
    public static implicit operator Grid<T>(T[,] grid) => new(grid);
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
        char[,] result = new char[width, height];
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                result[x, y] = lines[y][x];
        return result;
    }
}