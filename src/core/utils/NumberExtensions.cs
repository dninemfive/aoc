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
    public static IEnumerable<T> Deltas<T>(this IEnumerable<T> enumerable)
        where T : INumber<T>
    {
        if (enumerable.Count() < 2)
            throw new ArgumentException("Can only calculate deltas of two or more numbers!", nameof(enumerable));
        for (int i = 1; i < enumerable.Count(); i++)
            yield return enumerable.ElementAt(i) - enumerable.ElementAt(i - 1);
    }
    public static (T min, T max) MinMax<T>(this IEnumerable<T> numbers)
        where T : INumber<T>
    {
        if (numbers.Count() < 0)
            throw new ArgumentException("Cannot find minimum and maximum of an empty set!", nameof(numbers));
        T min = numbers.First(), max = min;
        foreach (T t in numbers)
        {
            min = T.Min(min, t);
            max = T.Max(max, t);
        }
        return (min, max);
    }
    public static bool IsMonotonic<T>(this IEnumerable<T> numbers, bool strict = false)
        where T : INumber<T>
    {
        (T min, T max) minMax = numbers.Deltas().MinMax();
        return strict ? minMax is ( > 0, > 0) or ( < 0, < 0)
                      : minMax is ( >= 0, > 0) or ( < 0, <= 0);
    }
}
