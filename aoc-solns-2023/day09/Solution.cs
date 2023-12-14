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
        yield return lines.Select(x => x.ToMany<int>()).Select(x => x.NextValue()).Sum();
    }
    public static IEnumerable<T> Diffs<T>(this IEnumerable<T> numbers)
        where T : INumber<T>
    {
        for(int i = 1; i < numbers.Count(); i++)
        {
            yield return numbers.ElementAt(i) - numbers.ElementAt(i - 1);
        }
    }
    public static T NextValue<T>(this IEnumerable<T> numbers)
        where T : INumber<T>
    {
        Console.WriteLine($"{nameof(NextValue)}<{typeof(T).Name}>({numbers.ListNotation()})");
        List<List<T>> sequences = [numbers.ToList()];
        List<T> cur = numbers.ToList();
        int ct = 0;
        while(cur.Any(x => x != T.Zero))
        {
            cur = cur.Diffs().ToList();
            Console.WriteLine($"{ct++,3}\t{cur.ListNotation()}");
            sequences.Add(cur);
        }
        for (int i = sequences.Count - 1; i >= 0; i--)
            FillIn(sequences, i);
        return sequences.First().First();
    }
    public static void FillIn<T>(this List<List<T>> sequences, int index)
        where T : INumber<T>
    {
        Console.WriteLine($"{nameof(FillIn)}<{typeof(T).Name}>([{sequences.Count}], {index})");
        if(index == sequences.Count - 1)
        {
            sequences[index].Add(T.Zero);
            return;
        }
        sequences[index].Add(sequences[index + 1].Last() + sequences[index].Last());
        Console.WriteLine($"\t{sequences[index].ListNotation()}");
    }
}