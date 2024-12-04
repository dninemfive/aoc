using d9.utl;
using Point = d9.aoc.core.Point<int>;

namespace d9.aoc.core.utils;
public static class GridExtensions
{
    public static IEnumerable<string> ToStrings(this Grid<char> grid)
        => grid.Rows.Select(x => x.row.Join());
    public static Grid<char> ToCharsBy<T>(this Grid<T> grid, Func<T, char> map)
        where T : struct
        => grid.Map(map);
    public static Grid<char> ToCharsWith<T>(this Grid<T> grid, IReadOnlyDictionary<T, char> dict)
        where T : struct
        => grid.Map(x => dict[x]);
}
