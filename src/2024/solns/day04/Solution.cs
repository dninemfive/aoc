namespace d9.aoc._24.day04;
[SolutionToProblem(4)]
internal class Solution : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(params string[] lines)
    {
        Grid<char> crossword = Grid<char>.From(lines);
        yield return "preinit";
        yield return crossword.AllPossibleWordStarts()
                              .Count(x => crossword.WordStartsAtLocation("XMAS", x.start, x.direction));
        yield return crossword.AllPoints
                              .Where(x => !crossword.IsOnEdge(x))
                              .Count(x => crossword.HasXMASAt(x));
    }
}
