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
                grid = grid.InsertColumn(x, '.');
                x++;
            }
        }
        for(int y = 0; y < grid.Width; y++)
        {
            if (grid.GetRow(y).All(c => c == '.'))
            {
                grid = grid.InsertRow(y, '.');
                y++;
            }
        }
        Console.WriteLine(Grid<char>.LayOut(grid));
        yield break;
    }
}
