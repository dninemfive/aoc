using System.Numerics;

namespace d9.aoc._23.day09;
internal static class Extensions
{
    public static IEnumerable<T> Diffs<T>(this IEnumerable<T> numbers)
        where T : INumber<T>
    {
        for (int i = 1; i < numbers.Count(); i++)
            yield return numbers.ElementAt(i) - numbers.ElementAt(i - 1);
    }
}
