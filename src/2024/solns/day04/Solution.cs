namespace d9.aoc._24.day04;
[SolutionToProblem(4)]
internal class Solution(params string[] lines)
    : AocSolution
{
    public readonly Grid<char> Crossword = Grid<char>.From(lines);
    [ExpectedResults(sample: 18, final: 2569)]
    public override AocPartialResult? Part1()
        => Crossword.AllPossibleWordStarts()
                    .Count(x => Crossword.WordStartsAtLocation("XMAS", x.start, x.direction));
    [ExpectedResults(sample: 9, final: 1998)]
    public override AocPartialResult? Part2()
        => Crossword.AllPoints
                    .Where(x => !Crossword.IsOnEdge(x))
                    .Count(x => Crossword.HasXMASAt(x));
}
