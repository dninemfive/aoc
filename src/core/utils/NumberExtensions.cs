using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace d9.aoc.core;
public static class NumberExtensions
{
    public static bool IsBetween<T>(this T a, T b, T c)
        where T : INumber<T>
        => a > T.Min(b, c) && a < T.Max(b, c);
    public static T LeastCommonMultiple<T>(this IEnumerable<T> enumerable)
        where T : INumber<T>
    {
        if (enumerable.Count() < 2)
            throw new ArgumentException($"Cannot find the least common multiple of {enumerable.Count()} numbers!", nameof(enumerable));
        T result = MathUtils.LeastCommonMultiple(enumerable.First(), enumerable.Second());
        foreach (T item in enumerable.Skip(2))
            result = MathUtils.LeastCommonMultiple(result, item);
        return result;
    }
    public static T MaxOrZero<T>(this IEnumerable<T> enumerable)
        where T : INumber<T>
    {
        if (!enumerable.Any())
            return T.Zero;
        return enumerable.Max() ?? T.Zero;
    }
}
