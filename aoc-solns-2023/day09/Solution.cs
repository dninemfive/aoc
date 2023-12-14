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
        int sum = 0;
        foreach(Sequence<int> seq in lines.Select(x => new Sequence<int>(x.ToMany<int>())))
        {
            int next = seq.Next();
            sum += next;
            Console.WriteLine($"{next,8}\t{sum,16}");
        }
        yield return sum;
    }
    public static IEnumerable<T> Diffs<T>(this IEnumerable<T> numbers)
        where T : INumber<T>
    {
        for(int i = 1; i < numbers.Count(); i++)
            yield return numbers.ElementAt(i) - numbers.ElementAt(i - 1);
    }
}