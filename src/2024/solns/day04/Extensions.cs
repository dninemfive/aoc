using d9.aoc.core.utils;
using Coordinate    = d9.aoc.core.Point<int>;
using Direction     = d9.aoc.core.Point<int>;
using Directions    = d9.aoc.core.Directions<int>;

namespace d9.aoc._24.day04;
internal static class Extensions
{
    internal static IEnumerable<Direction> HalfClockwise => Directions.Clockwise.Take(4);
    internal static IEnumerable<Direction> MasDirectionsAt(this Grid<char> grid, Coordinate location)
    {
        if (grid.TryGet(location, out char? center) && center != 'A')
            yield break;
        foreach (Direction direction in HalfClockwise)
            if (grid.WordAt(location, direction) is "MAS" or "SAM")
                yield return direction;
    }
    internal static bool IsOrthogonalTo(this Direction a, Direction b)
        => a.Dot(b) == 0;
    internal static int X_MASesAt(this Grid<char> grid, Coordinate location)
    {
        Stack<Direction> directions = new(grid.MasDirectionsAt(location));
        if (directions.Count < 2)
            return 0;
        int ct = 0;
        while(directions.Any())
        {
            Direction cur = directions.Pop();
            if (directions.Any(x => x.IsOrthogonalTo(cur)))
                ct++;
        }
        if(ct > 0)
        {
            Console.WriteLine($"=======\n{location}\n{grid.NeighborhoodOf(location).LayOut()}");
            Console.WriteLine($"-> {ct}");
        }
        return ct;
    }
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
