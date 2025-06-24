using d9.aoc.core;

namespace d9.aoc._23.day11;
[SolutionToProblem(11)]
public class Solution
    : AocSolution
{
    public readonly Grid<char> Grid;
    public readonly IEnumerable<int> RowIndices, ColumnIndices;
    public readonly HashSet<int> EmptyRows, EmptyColumns;
    public readonly IReadOnlyList<(Point<int> a, Point<int> b)> UniquePairs;
        
    public Solution(string[] lines)
    {
        Grid = Grid<char>.From(lines);
        RowIndices = Grid.Rows.Where(r => r.row.All(x => x == '.')).Select(r => r.index);
        EmptyRows = [.. RowIndices];
        ColumnIndices = Grid.Columns.Where(c => c.column.All(x => x == '.')).Select(c => c.index);
        EmptyColumns = [.. ColumnIndices];
        UniquePairs = [.. Grid.GalaxyLocations()
              .ToList()
              .UniquePairs()];
    }

    [ExpectedResults(9565386L)]
    public override AocPartResultValue? Part1()
        => UniquePairs.Select(x => GalaxyDistance(x, 2)).Sum();

    [ExpectedResults(857986849428L)]
    public override AocPartResultValue? Part2()
        => UniquePairs.Select(x => GalaxyDistance(x, (int)1e6)).Sum();

    private long GalaxyDistance((Point<int> a, Point<int> b) pair, int expansionFactor = 2)
    {
        ((int x1, int y1), (int x2, int y2)) = pair;
        int x = x1, 
            dX = Math.Sign(x2 - x1), 
            y = y1, 
            dY = Math.Sign(y2 - y1);
        long distance = 0;
        while(x != x2)
        {
            distance += EmptyColumns.Contains(x) ? expansionFactor : 1;
            x += dX;
        }
        while (y != y2)
        {
            distance += EmptyRows.Contains(y) ? expansionFactor : 1;
            y += dY;
        }
        return distance;
    }
}
