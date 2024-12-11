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
        yield return "calc p1";
        yield return part1.Count();
        Console.Out.Flush();
        IEnumerable<BigInteger> part2 = Blink(part1.Select(x => new BigInteger(x)), 50);
        yield return "calc p2";
        Console.Out.Flush();
        File.WriteAllText("_Day11_asdf1.txt", $"{DateTime.Now:g}");
        yield return BigCount(part2);
        File.WriteAllText("_Day11_asdf2.txt", $"{DateTime.Now:g}");
    }
    public static IEnumerable<T> Blink<T>(IEnumerable<T> initial, int times)
        where T : INumber<T>
    {
        IEnumerable<T> stones = initial;
        string fileName = $"_Day11_debug_progress_{times}.txt";
        File.WriteAllText(fileName, "");
        Stopwatch stopwatch = new();
        for(int i = 0; i < times; i++)
        {
            stopwatch.Restart();
            stones = stones.SelectMany(ReplacementRules<T>.ApplyFirst);
            stopwatch.Stop();
            File.AppendAllText(fileName, $"{i + 1,2}\t{DateTime.Now,16:g}\t{stopwatch.Elapsed:g}\n");
            // stopwatch.Restart();
            // int ct = stones.Count();
            // stopwatch.Stop();
            // File.AppendAllText(fileName, $"{stones.Count(),16}\t({stopwatch.Elapsed})\n");
        }
        File.AppendAllText(fileName, $"Done!");
        return stones;
    }
    public static BigInteger BigCount<T>(IEnumerable<T> enumerable)
        => Count<T, BigInteger>(enumerable);
    public static Z Count<T, Z>(IEnumerable<T> enumerable)
        where Z : INumber<Z>, IModulusOperators<Z,Z,Z>
    {
        Z result = Z.Zero;
        Z modulo = Z.CreateChecked(1000000);
        string fileName = "_Day11_debug_ct.txt";
        File.WriteAllText(fileName, "");
        Stopwatch sw = Stopwatch.StartNew();
        foreach (T _ in enumerable)
        {
            result++;
            if (result % modulo == Z.Zero)
                File.AppendAllText(fileName, $"{sw,16:g} {result,32}");
        }
        return result;
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