namespace d9.aoc._24.day02;
[SolutionToProblem(2)]
public class Solution(params string[] lines) 
    : AocSolution
{
    public readonly IEnumerable<Report> Reports = lines.Select(x => new Report(x));
    [ExpectedResults(final: 257)]
    public override AocPartResultValue Part1()
        => Reports.Count(x => x.IsStrictlySafe);
    [ExpectedResults(final: 328)]
    public override AocPartResultValue Part2()
        => Reports.Count(x => x.IsLooselySafe);
}
