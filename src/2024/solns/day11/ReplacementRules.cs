using System.Numerics;

namespace d9.aoc._24.day11;
public delegate IEnumerable<BigInteger>? ReplacementRule(BigInteger n);
public static class ReplacementRules
{
    public static readonly IEnumerable<ReplacementRule> RulesInOrder = [ZeroToOne, SplitEven, MultiplyBy2024];
    private static readonly Dictionary<BigInteger, IEnumerable<BigInteger>> _cache = new();
    public static IEnumerable<BigInteger> ApplyFirst(this BigInteger n)
    {
        if (_cache.TryGetValue(n, out IEnumerable<BigInteger>? value))
            return value;
        foreach (ReplacementRule rule in RulesInOrder)
            if (rule(n) is IEnumerable<BigInteger> result)
            {
                _cache[n] = result;
                return result;
            }
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
