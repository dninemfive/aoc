namespace d9.aoc._24.day06;
[SolutionToProblem(6)]
[SampleResults(41, 6)]
[FinalResults(5453)]
internal class Solution : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(params string[] lines)
    {
        Grid<char> data = Grid<char>.From(lines);
        yield return "preinit";
        Map map = new(data);
        GuardReport report1 = map.Run();
        yield return report1.TouchedPositions.Count;
        int ct = 0;
        foreach(Point<int> p in  report1.TouchedPositions)
        {
            if (p == data.GuardPosition())
                continue;
            Grid<char> variant = data.CopyWith((p, '#'));
            if (new Map(variant).Run().IsCycle)
                ct++;
        }
        yield return ct;
    }
}
