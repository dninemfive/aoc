using d9.aoc.core;

namespace d9.aoc._23.day03;
[SolutionToProblem(3)]
public class Solution(string[] lines)
    : AocSolution
{
    public readonly Grid<char> Grid = Grid<char>.From(lines);
    [ExpectedResults(520135)]
    public override AocPartResultValue? Part1()
        => Grid.PartNumbers().Sum();
    [ExpectedResults(72514855)]
    public override AocPartResultValue? Part2()
        => Grid.GearRatios().Sum();
}