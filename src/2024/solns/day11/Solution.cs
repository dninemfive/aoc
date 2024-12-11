using System.Diagnostics;
using System.Numerics;
using ReplacementInfo = (System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> replacements, System.Numerics.BigInteger increase);
namespace d9.aoc._24.day11;
[SolutionToProblem(11)]
[SampleResults(55312)]
[FinalResults(220722)]
internal class Solution : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(params string[] lines)
    {
        (IEnumerable<BigInteger> stones1, BigInteger count1) = Blink(lines.First().ToMany<BigInteger>(), 25);
        yield return count1;
        (IEnumerable<BigInteger> _, BigInteger count2) = Blink(stones1, 50, count1, 25);
        yield return count2;
    }
    public static (IEnumerable<BigInteger> stones, BigInteger count) Blink(IEnumerable<BigInteger> initial, int times, BigInteger? initialCount = null, int start = 0)
    {
        IEnumerable<BigInteger> stones = initial;
        Stopwatch stopwatch = new();
        BigInteger count = initialCount ?? 0;
        string fileName = $"_Day11_debug_progress_{initialCount}.txt";
        File.WriteAllText(fileName, "Index\tTime\tCalculation Time\tCount\n");
        for (int i = 0; i < times; i++)
        {
            stopwatch.Restart();
            List<BigInteger> newStones = new();
            foreach(BigInteger stone in stones)
            {
                (IEnumerable<BigInteger> replacements, BigInteger increase) = ReplacementRules.ApplyFirst(stone);
                newStones.AddRange(replacements);
                count += increase;
            }
            stones = newStones;
            stopwatch.Stop();
            File.AppendAllText(fileName, $"{i + 1 + start,2}\t{DateTime.Now,16:g}\t{stopwatch.Elapsed:g}\t{count}\n");
        }
        File.AppendAllText(fileName, $"Done!");
        return (stones, count);
    }
    public static BigInteger BigCount<T>(IEnumerable<T> enumerable)
        => Count<T, BigInteger>(enumerable);
    public static Z Count<T, Z>(IEnumerable<T> enumerable)
        where Z : INumber<Z>, IModulusOperators<Z,Z,Z>
    {
        Z result = Z.Zero;
        Z modulo = Z.CreateChecked(10000000);
        string fileName = "_Day11_debug_ct.txt";
        File.WriteAllText(fileName, "");
        Stopwatch sw = Stopwatch.StartNew();
        foreach (T _ in enumerable)
        {
            result++;
            if (result % modulo == Z.Zero)
            {
                sw.Stop();
                File.AppendAllText(fileName, $"{sw,16:g}\t{result,32}\n");
                sw.Restart();
            }
        }
        return result;
    }
}
public delegate ReplacementInfo? ReplacementRule(BigInteger n);
public static class ReplacementRules
{
    public static readonly IEnumerable<ReplacementRule> RulesInOrder = [ZeroToOne, SplitEven, MultiplyBy2024];
    public static ReplacementInfo ApplyFirst(BigInteger n)
    {
        foreach (ReplacementRule rule in RulesInOrder)
            if (rule(n) is ReplacementInfo result)
                return result;
        throw new ArgumentException($"No rule applied to {n}!");
    }
    public static ReplacementInfo? ZeroToOne(BigInteger n)
    {
        if (n != 0)
            return null;
        return ([1], 0);
    }
    public static ReplacementInfo? SplitEven(BigInteger n)
    {
        if (n.Digits().IsOdd())
            return null;
        string str = n.ToString()!;
        (string left, string right) = str.SplitInHalf();
        return ([BigInteger.Parse(left), BigInteger.Parse(right)], 1);
    }
    public static ReplacementInfo? MultiplyBy2024(BigInteger n)
    {
        return ([n * 2024], 0);
    }
}
public static class Extensions
{
    public static int Digits(this BigInteger n)
        => (int)BigInteger.Log10(n) + 1;
    public static (string left, string right) SplitInHalf(this string s)
    {
        int halfLength = s.Length / 2;
        return (s[..halfLength], s[halfLength..]);
    }
}