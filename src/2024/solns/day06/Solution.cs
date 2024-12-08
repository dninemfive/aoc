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
        MapState initial = MapState.FromInitial(data);
        (HashSet<Point<int>> positions, bool _) = initial.Run();
        yield return positions.Count;
        int ct = 0;
        foreach(Point<int> p in positions)
        {
            if (p == initial.Guard!.Position)
                continue;
            Grid<char> variant = data.CopyWith((p, '#'));
            if (MapState.FromInitial(variant).Run().isCycle)
                ct++;
        }
        yield return ct;
    }
}
