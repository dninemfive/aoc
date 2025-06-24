using d9.aoc.core;

namespace d9.aoc._23.day03;
[SolutionToProblem(3)]
public class Solution : AocSolution
{
    public override IEnumerable<AocPartResultValue> Solve(string[] inputLines)
    {
        Grid<char> grid = Grid<char>.From(inputLines);
        yield return (0b0, "preinit");
        yield return grid.PartNumbers().Sum();
        yield return grid.GearRatios().Sum();
    }    
}