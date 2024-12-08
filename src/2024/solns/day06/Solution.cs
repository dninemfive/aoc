using d9.aoc.core.utils;

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
        (HashSet<Point<int>> positions, bool _, Grid<char> _) = initial.Run();
        yield return positions.Count;
        yield return CyclesIn(data, data.AllPoints).Count(x => x);
    }
    public IEnumerable<bool> CyclesIn(Grid<char> data, IEnumerable<Point<int>> variantPositions)
    {
        string baseDir = $"_debug/{data.GetHashCode()}",
               cycleDir     = Path.Join(baseDir, "cycle"),
               noCycleDir   = Path.Join(baseDir, "nocycle");
        Directory.CreateDirectory(baseDir);
        Directory.CreateDirectory(cycleDir);
        Directory.CreateDirectory(noCycleDir);
        foreach(Point<int> p in variantPositions)
            if (!data[p].IsGuard() && !data[p].IsObstacle())
            {
                (HashSet<Point<int>> positions, bool isCycle, Grid<char> track) = MapState.FromInitial(data.CopyWith((p, '#'))).Run();
                File.WriteAllText(Path.Join(isCycle ? cycleDir : noCycleDir, $"{p}-{positions.Count}.track"), track.Map(x => x is '.' ? ' ' : x).LayOut());
                yield return isCycle;
            }
    }
}
