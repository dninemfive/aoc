using System.Diagnostics;
using System.Numerics;
namespace d9.aoc._24.day11;
[SolutionToProblem(11)]
[SampleResults(55312)]
[FinalResults(220722)]
internal class Solution : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(params string[] lines)
    {
        IEnumerable<long> part1 = Blink(lines.First().ToMany<long>(), 25);
        yield return part1.Count();
        yield return Blink(part1.Select(x => new BigInteger(x)), 25, 75).Count();
    }
    public static IEnumerable<T> Blink<T>(IEnumerable<T> initial, int times)
        where T : INumber<T>
        => Blink(initial, 0, times);
    public static IEnumerable<T> Blink<T>(IEnumerable<T> initial, int start, int end)
        where T : INumber<T>
    {
        IEnumerable<T> stones = initial;
        string fileName = "_Day11_debug_progress.txt";
        File.Create(fileName);
        Stopwatch stopwatch = new();
        for(int i = start; i < end; i++)
        {
            stopwatch.Start();
            stones = stones.SelectMany(ReplacementRules<T>.ApplyFirst);
            stopwatch.Stop();
            File.AppendAllText(fileName, $"{DateTime.Now,16:g}\t{i,2}\t{stones.Count(),16}\t{stopwatch.Elapsed:g}");
            stopwatch.Reset();
        }
        return stones;
    }
}
public delegate IEnumerable<T>? ReplacementRule<T>(T n)
    where T : INumber<T>;
public static class ReplacementRules<T>
    where T : INumber<T>
{
    public static readonly IEnumerable<ReplacementRule<T>> RulesInOrder = [ZeroToOne, SplitEven, MultiplyBy2024];
    public static IEnumerable<T> ApplyFirst(T n)
    {
        foreach (ReplacementRule<T> rule in RulesInOrder)
            if (rule(n) is IEnumerable<T> result)
                return result;
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