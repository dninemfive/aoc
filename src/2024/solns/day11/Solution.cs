using System.Diagnostics;
using System.Numerics;
using ReplacementInfo = (System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> replacements, System.Numerics.BigInteger increase);
namespace d9.aoc._24.day11;
[SolutionToProblem(11)]
[SampleResults(55312)]
[FinalResults(220722)]
internal class Solution : AocSolution
{
    public static readonly string DebugFolder = Path.Join("_debug", "day11");
    public override IEnumerable<AocPartialResult> Solve(params string[] lines)
    {
        Directory.CreateDirectory(DebugFolder);
        StoneSuccessorCache bit = new();
        IEnumerable<BigInteger> stones = lines.First().ToMany<BigInteger>().Order();
        BigInteger count1 = CountStones(bit, stones, 25) + 7;
        yield return count1;
        yield return CountStones(bit, stones, 75) + 7;
    }
    public static BigInteger CountStones(StoneSuccessorCache bit, IEnumerable<BigInteger> stones, int depth)
    {
        BigInteger result = 0;
        foreach (BigInteger stone in stones)
            result += bit.CountTree(stone, depth);
        return result;
    }
    /*public static (IEnumerable<BigInteger> stones, BigInteger count) Blink(IEnumerable<BigInteger> initial, int times, BigInteger? initialCount = null, int start = 0)
    {
        IEnumerable<BigInteger> stones = initial;
        Stopwatch stopwatch = new();
        BigInteger count = initialCount ?? initial.Count();
        string fileName = $"_Day11_debug_progress_{start}.txt";
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
    public static (LinkedList<BigInteger> stones, BigInteger count) BlinkOnce(IEnumerable<BigInteger> stones, BigInteger? initialCount = null)
    {
        BigInteger count = initialCount ?? 1;
        LinkedList<BigInteger> result = new();
        foreach (BigInteger stone in stones)
        {
            (IEnumerable<BigInteger> replacements, BigInteger i) = ReplacementRules.ApplyFirst(stone);
            count += i;
            foreach (BigInteger replacement in replacements)
                result.AddLast(replacement);
        }
        return (result, count);
    }
    public static IEnumerable<(int index, BigInteger count)> BlinkRepeatedly(BigInteger initialStone, params int[] checkpoints)
    {
        HashSet<int> checkpointSet = checkpoints.ToHashSet();
        int stop = checkpointSet.Max();
        (LinkedList<BigInteger> stones, BigInteger count) = BlinkOnce([initialStone]);
        string fileName = $"_Day11_debug_progress_{initialStone}_{checkpointSet.Order().ListNotation(brackets: null, delimiter: "-")}.txt";
        File.WriteAllText(fileName, "Index\tCalculation Time\tCount\n");
        Stopwatch stopwatch = new();
        for (int i = 0; i < stop; i++)
        {
            stopwatch.Restart();
            if (checkpointSet.Contains(i + 1))
                yield return (i + 1, count);
            (stones, count) = BlinkOnce(stones, count);
            stopwatch.Stop();
            File.AppendAllText(fileName, $"{i + 1,2}\t{stopwatch.Elapsed:g}\t{count,24}\n");
        }
    }*/
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
public delegate IEnumerable<BigInteger>? ReplacementRule(BigInteger n);
public static class ReplacementRules
{
    public static readonly IEnumerable<ReplacementRule> RulesInOrder = [ZeroToOne, SplitEven, MultiplyBy2024];
    private static Dictionary<BigInteger, ReplacementInfo> _cache = new();
    public static IEnumerable<BigInteger> ApplyFirst(this BigInteger n)
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
    {
        return [n * 2024];
    }
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