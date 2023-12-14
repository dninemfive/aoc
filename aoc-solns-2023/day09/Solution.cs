using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace d9.aoc._23.day09;
public static class Solution
{
    [SolutionToProblem(9)]
    public static IEnumerable<object> Solve(string[] lines)
    {
        yield return lines.Select(x => new Sequence<int>(x.ToMany<int>())).Select(x => x.Next()).Sum();
        Console.WriteLine(new Sequence<int>(lines.First().ToMany<int>()).Pyramid);
    }
    public static IEnumerable<T> Diffs<T>(this IEnumerable<T> numbers)
        where T : INumber<T>
    {
        for(int i = 1; i < numbers.Count(); i++)
            yield return numbers.ElementAt(i) - numbers.ElementAt(i - 1);
    }
}