using System.Linq;
using System.Numerics;

namespace d9.aoc._24.day11;
[SolutionToProblem(11)]
[SampleResults(55312)]
internal class Solution : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(params string[] lines)
    {
        IEnumerable<long> stones = Blink(lines.First().ToMany<long>(), 25);
        yield return stones.Count();
        // Console.WriteLine(stones.ListNotation(brackets: null, delimiter: " "));
    }
    public static IEnumerable<long> Blink(IEnumerable<long> initial, long times)
    {
        IEnumerable<long> result = initial;
        for(long i = 0; i < times; i++)
            result = result.SelectMany(ReplacementRules<long>.ApplyFirst);
        return result;
    }
}
public delegate IEnumerable<T>? ReplacementRule<T>(T n)
    where T : INumber<T>;
public static class ReplacementRules<T>
    where T : INumber<T>
{
    public static IEnumerable<ReplacementRule<T>> RulesInOrder = [ZeroToOne, SplitEven, MultiplyBy2024];
    public static IEnumerable<T> ApplyFirst(T n)
    {
        foreach (ReplacementRule<T> rule in RulesInOrder)
        {
            if (rule(n) is IEnumerable<T> result)
            {
                if(T.IsNegative(n) || result.Any(T.IsNegative))
                    Console.WriteLine($"{n} -> {result.ListNotation()} ({rule.Method.Name})");
                return result;
            } 
        }
        throw new ArgumentException($"No rule applied to {n}!");
    }
    public static IEnumerable<T>? ZeroToOne(T n)
    {
        if (n != T.Zero)
            return null;
        return [T.One];
    }
    public static IEnumerable<T>? SplitEven(T n)
    {
        string str = n.ToString()!;
        if (str.Length % 2 != 0)
            return null;
        (string left, string right) = str.SplitInHalf();
        return [T.Parse(left, null), T.Parse(right, null)];
    }
    public static IEnumerable<T>? MultiplyBy2024(T n)
    {
        return [n * T.CreateChecked(2024)];
    }
}
public static class Extensions
{
    public static (string left, string right) SplitInHalf(this string s)
    {
        int halfLength = s.Length / 2;
        return (s[..halfLength], s[halfLength..]);
    }
}