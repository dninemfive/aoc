using System.Numerics;

namespace d9.aoc._24.day11;
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
    public static IEnumerable<BigInteger> Successors(this BigInteger stone)
        => ReplacementRules.ApplyFirst(stone);
}
