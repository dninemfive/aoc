using d9.aoc.core;

namespace d9.aoc._23.day07;
[SolutionToProblem(7)]
public class Solution : AocSolution
{
    public override IEnumerable<AocPartResultValue> Solve(string[] lines)
    {
        yield return Winnings(lines, jokerMode: false);
        yield return Winnings(lines, jokerMode: true);
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