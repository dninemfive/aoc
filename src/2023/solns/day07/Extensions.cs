namespace d9.aoc._23.day07;
internal static class Extensions
{
    public static IEnumerable<int> Rank(this IEnumerable<Hand> hands)
    {
        int rank = hands.Count();
        foreach (Hand hand in hands.OrderDescending())
            yield return rank--;
    }
}
