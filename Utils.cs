using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d9.aoc._23;
// i should really just use utl...
public static class Utils
{
    public static string ListNotation<T>(this IEnumerable<T> enumerable)
        => enumerable.Any() ? enumerable.Select(x => $"{x}").Aggregate((x, y) => $"{x}, {y}") : "(no elements)";
}
