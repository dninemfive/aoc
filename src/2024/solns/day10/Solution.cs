using d9.aoc.core.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = d9.aoc.core.Point<int>;

namespace d9.aoc._24.day10;
[SolutionToProblem(10)]
[SampleResults(36)]
internal class Solution : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(params string[] lines)
    {
        Grid<int> map = Grid<char>.From(lines).Map(x => x - '0');
        yield return "preinit";
        yield return map.AllPoints.OrderBy(p => p.Y)
                                  .ThenBy(p => p.X)
                                  .Select(x => map.TrailheadScoreFor(x))
                                  .Sum();
    }
}
public static class Extensions
{
    public static IEnumerable<Point> TrailNeighborsOf(this Grid<int> map, Point<int> position)
        => map.PointsCardinallyAdjacentTo(position).Where(x => map[x] - map[position] == 1);
    public static int TrailheadScoreFor(this Grid<int> map, Point<int> position)
    {
        if (!map.TryGet(position, out int? val) || val != 0)
            return 0;
        List<Point> neighbors = [.. map.TrailNeighborsOf(position)];
        HashSet<Point> visited = [position];
        List<Point> getOptions()
            => neighbors.Where(x => !visited.Contains(x)).ToList();
        List<Point> options = getOptions();
        while(options.Any())
        {
            foreach(Point<int> option in options)
            {
                visited.Add(option);
                foreach (Point neighbor in map.TrailNeighborsOf(option))
                    neighbors.Add(neighbor);
            }
            options = getOptions();
        }
        return visited.Count(x => map[x] == 9);
    }
}