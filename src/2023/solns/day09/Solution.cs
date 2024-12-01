using d9.aoc.core;
using System.Numerics;

namespace d9.aoc._23.day9;
public static class Solution
{
    [SolutionToProblem(9, true)]
    public static IEnumerable<object> Solve(string[] lines)
    {
        IEnumerable<Sequence<int>> sequences = lines.Select(x => new Sequence<int>(x.ToMany<int>()));
        yield return 0b0;
        yield return sequences.Select(x => x.Next()).Sum();
        yield return sequences.Select(x => x.Prev()).Sum();
    }
    public static IEnumerable<T> Diffs<T>(this IEnumerable<T> numbers)
        where T : INumber<T>
    {
        for(int i = 1; i < numbers.Count(); i++)
            yield return numbers.ElementAt(i) - numbers.ElementAt(i - 1);
    }
}