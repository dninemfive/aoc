namespace d9.aoc._24.day02;
internal static class Extensions
{
    internal static bool IsSafe(this IEnumerable<int> deltas)
        => deltas.MinMax() is ( > 0, <= 3) or ( >= -3, < 0);
}
