using Coordinate    = d9.aoc.core.Point<int>;
using Direction     = d9.aoc.core.Point<int>;
using Directions    = d9.aoc.core.Directions<int>;

namespace d9.aoc._24.day04;
internal static class Extensions
{
    internal static bool HasXMASAt(this Grid<char> grid, Coordinate location)
        => (grid.WordAt(location, (1, 1)) is "MAS" or "SAM") && (grid.WordAt(location, (1, -1)) is "MAS" or "SAM");
    internal static bool WordStartsAtLocation(this Grid<char> grid, string word, Coordinate start, Direction direction)
    {
        Coordinate cur = start;
        foreach (char c in word)
        {
            if (!grid.Contains(cur) || grid[cur] != c)
                return false;
            cur += direction;
        }
        return true;
    }
    internal static IEnumerable<(Coordinate start, Direction direction)> AllPossibleWordStarts(this Grid<char> grid)
    {
        foreach (Coordinate start in grid.AllPoints)
            foreach (Direction direction in Directions.Clockwise)
                yield return (start, direction);
    }
    internal static string? WordAt(this Grid<char> grid, Coordinate location, Direction direction)
    {
        if (grid.TryGet(location + direction.Opposite(), out char? a)
            && grid.TryGet(location, out char? b)
            && grid.TryGet(location + direction, out char? c))
            return $"{a}{b}{c}";
        return null;
    }
}
