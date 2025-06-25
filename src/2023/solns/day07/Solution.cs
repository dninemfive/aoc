using d9.aoc.core;

namespace d9.aoc._23.day07;
[SolutionToProblem(7)]
public class Solution(string[] lines) 
    : AocSolution
{
    [ExpectedResults(248453531)]
    public override AocPartResultValue? Part1()
        => Winnings(lines, jokerMode: false);

    [ExpectedResults(248781813)]
    public override AocPartResultValue? Part2()
        => Winnings(lines, jokerMode: true);

    public static int Winnings(IEnumerable<string> lines, bool jokerMode)
    {
        IEnumerable<Hand> hands = lines.Select(x => new Hand(x, jokerMode)).OrderDescending();
        IEnumerable<int> ranks = hands.Rank();
        return hands.Zip(ranks)
                    .Select(x => x.First.Bet * x.Second)
                    .Sum();
    }
}