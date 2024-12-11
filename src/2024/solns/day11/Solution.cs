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
        (IEnumerable<long> part1, long count1) = Blink(lines.First().ToMany<long>(), 25);
        yield return count1;
        (IEnumerable<BigInteger> part2, BigInteger count2) = Blink(part1.Select(x => new BigInteger(x)), 50, 25);
        yield return count2;
    }
    public static (IEnumerable<T> stones, T count) Blink<T>(IEnumerable<T> initial, int times, int start = 0)
        where T : INumber<T>
    {
        IEnumerable<T> stones = initial;
        string fileName = $"_Day11_debug_progress_{times}.txt";
        File.WriteAllText(fileName, "Index\tTime\tCalculation Time\tCount\tLargest Stone\n");
        Stopwatch stopwatch = new();
        T count = T.Zero;
        for(int i = 0; i < times; i++)
        {
            stopwatch.Restart();
            List<T> newStones = new();
            T largestStone = T.Zero;
            foreach(T stone in stones)
            {
                bool increment = false;
                foreach(T newStone in ReplacementRules<T>.ApplyFirst(stone))
                {
                    largestStone = T.Max(newStone, largestStone);
                    if (increment)
                        count++;
                    newStones.Add(newStone);
                    increment = true;
                }
            }
            stones = newStones;
            stopwatch.Stop();
            File.AppendAllText(fileName, $"{i + 1 + start,2}\t{DateTime.Now,16:g}\t{stopwatch.Elapsed:g}\t{count}\t{largestStone}\n");
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
        if (T.IsEvenInteger(n.Digits()))
            return null;
        string str = n.ToString()!;
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
    public static T Digits<T>(this T n)
        where T : INumber<T>, ILogarithmicFunctions<T>
        => T.Log10(n + T.One);
    public static (string left, string right) SplitInHalf(this string s)
    {
        int halfLength = s.Length / 2;
        return (s[..halfLength], s[halfLength..]);
    }
}