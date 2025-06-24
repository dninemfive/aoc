using d9.aoc.core;

namespace d9.aoc._23.day01;

[SolutionToProblem(1)]
public class Solution(string[] lines) 
    : AocSolution
{
    [ExpectedResults(55090)]
    public override AocPartResultValue? Part1()
        => lines.Sum(x => x.CalibrationValueUsing(FindDigit.FromNumber));
    [ExpectedResults(54845)]
    public override AocPartResultValue? Part2()
        => lines.Sum(x => x.CalibrationValueUsing(FindDigit.FromNumber, FindDigit.FromString));
}