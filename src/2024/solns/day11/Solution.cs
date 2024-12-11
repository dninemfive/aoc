using System.Numerics;
using SolutionInt = System.Numerics.BigInteger;

namespace d9.aoc._24.day11;
[SolutionToProblem(11)]
[SampleResults(55312)]
[FinalResults(220722)]
internal class Solution : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(params string[] lines)
    {
        foreach((int i, int ct) in Blink(lines.First().ToMany<SolutionInt>(), 75))
        {
            if (i is 24 or 74)
                yield return ct;
        }
    }
    public static IEnumerable<(int index, int count)> Blink(IEnumerable<SolutionInt> initial, int times)
    {
        IEnumerable<SolutionInt> stones = initial;
        for(int i = 0; i < times; i++)
        {
            stones = stones.SelectMany(ReplacementRules<SolutionInt>.ApplyFirst);
            File.WriteAllText($"{i}.txt", stones.ListNotation());
            yield return (i, stones.Count());
        }
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