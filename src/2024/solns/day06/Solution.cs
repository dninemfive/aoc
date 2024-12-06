namespace d9.aoc._24.day06;
[SolutionToProblem(6)]
[SampleResults(41)]
[FinalResults(5453)]
internal class Solution : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(params string[] lines)
    {
        Grid<char> data = Grid<char>.From(lines);
        yield return "preinit";
        Map map = new(data);
        map.Run();
        yield return map.TouchedPositionCount;
    }
}
