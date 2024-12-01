namespace d9.aoc._23.day7;
public static class Solution
{
    [SolutionToProblem(7)]
    public static IEnumerable<object> Solve(string[] lines)
    {
        yield return Winnings(lines, jokerMode: false);
        yield return Winnings(lines, jokerMode: true);
    }
    public static IEnumerable<int> Rank(this IEnumerable<Hand> hands)
    {
        int rank = hands.Count();
        foreach (Hand hand in hands.OrderDescending())
            yield return rank--;
    }
    public static int Winnings(IEnumerable<string> lines, bool jokerMode)
    {
        IEnumerable<Hand> hands = lines.Select(x => new Hand(x, jokerMode)).OrderDescending();
        IEnumerable<int> ranks = hands.Rank();
        return hands.Zip(ranks)
                    .Select(x => x.First.Bet * x.Second)
                    .Sum();
    }
}