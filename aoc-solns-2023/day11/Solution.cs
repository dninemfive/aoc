using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d9.aoc._23.day11;
public static class Solution
{
    [SolutionToProblem(11)]
    public static IEnumerable<object> Solve(string[] lines)
    {
        Grid<char> grid = Grid<char>.From(lines);
        for(int x = 0; x < grid.Width; x++)
        {
            if(grid.GetColumn(x).All(c => c == '.'))
            {
                Console.WriteLine(x);
                grid = grid.InsertColumn(x, '.');
                x++;
            }
        }
        for(int y = 0; y < grid.Height; y++)
        {
            if (grid.GetRow(y).All(c => c == '.'))
            {
                Console.WriteLine(y);
                grid = grid.InsertRow(y, '.');
                y++;
            }
        }
        File.WriteAllText(Path.Join(Program.INPUT_FOLDER, "11.output"), Grid<char>.LayOut(grid));
        yield return grid.GalaxyLocations().ToList().UniquePairs().Select(TaxicabDistance).Sum();
    }
    public static IEnumerable<Point> GalaxyLocations(this Grid<char> grid)
        => grid.AllPoints.Where(p => grid[p] == '#');
    public static IEnumerable<(Point a, Point b)> UniquePairs(this List<Point> points)
    {
        HashSet<(Point a, Point b)> seenPairs = new();
        bool hasBeenSeen(Point a, Point b) => seenPairs.Contains((a, b));
        for(int i = 0; i <  points.Count; i++)
        {
            for(int j = i + 1; j < points.Count; j++)
            {
                (Point a, Point b) = (points[i], points[j]);
                if (hasBeenSeen(a, b))
                    continue;
                yield return (a, b);
                seenPairs.Add((a, b));
                seenPairs.Add((b, a));
            }
        }
    }
    // uses the fact that the length of the shortest taxicab distance between a and b is just the sum of the differences of their axes
    public static int TaxicabDistance(Point a, Point b)
        => Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
    public static int TaxicabDistance(this (Point a, Point b) pair)
        => TaxicabDistance(pair.a, pair.b);
}
