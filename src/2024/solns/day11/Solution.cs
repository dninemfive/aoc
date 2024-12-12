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
        IEnumerable<BigInteger> part1 = Blink(lines.First().ToMany<BigInteger>(), 25);
        yield return "calc p1";
        yield return part1.Count();
        IEnumerable<BigInteger> part2 = Blink(part1, 50);
        yield return "calc p2";
        yield return BigCount(part2);
    }
    public static IEnumerable<BigInteger> Blink(IEnumerable<BigInteger> initial, int times)
    {
        IEnumerable<BigInteger> stones = initial;
        string fileName = $"_Day11_debug_progress_{times}.txt";
        File.WriteAllText(fileName, "");
        Stopwatch stopwatch = new();
        for(int i = 0; i < times; i++)
        {
            stopwatch.Restart();
            stones = stones.SelectMany(ReplacementRules.ApplyFirst);
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
public delegate IEnumerable<BigInteger>? ReplacementRule(BigInteger n);
public static class ReplacementRules
{
    public static readonly IEnumerable<ReplacementRule> RulesInOrder = [ZeroToOne, SplitEven, MultiplyBy2024];
    public static IEnumerable<BigInteger> ApplyFirst(BigInteger n)
    {
        foreach (ReplacementRule rule in RulesInOrder)
            if (rule(n) is IEnumerable<BigInteger> result)
                return result;
        throw new ArgumentException($"No rule applied to {n}!");
    }
    public static IEnumerable<BigInteger>? ZeroToOne(BigInteger n)
    {
        if (n != 0)
            return null;
        return [1];
    }
    public static IEnumerable<BigInteger>? SplitEven(BigInteger n)
    {
        int digits = n.Digits();
        if (digits.IsOdd())
            return null;
        (BigInteger left, BigInteger right) = n.SplitInHalf(digits);
        return [left, right];
    }
    public static IEnumerable<BigInteger>? MultiplyBy2024(BigInteger n)
        => [n * 2024];
}
public static class Extensions
{
    public static int Digits(this BigInteger n)
        => (int)BigInteger.Log10(n) + 1;
    public static (BigInteger left, BigInteger right) SplitInHalf(this BigInteger n, int? digits = null)
    {
        BigInteger divisor = BigInteger.Pow(10, (digits ?? n.Digits()) / 2);
        return (n / divisor, n % divisor);
    }
    public static (string left, string right) SplitInHalf(this string s)
    {
        int halfLength = s.Length / 2;
        return (s[..halfLength], s[halfLength..]);
    }
}