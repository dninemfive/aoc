using d9.aoc.core;

namespace d9.aoc._23.day1;

[SolutionToProblem(1)]
public class Solution(string inputPath) : AocSolution(inputPath)
{
    public override IEnumerable<object> Solve(string[] lines)
    {
        yield return lines.Sum(x => x.CalibrationValueUsing(FindDigit.FromNumber));
        yield return lines.Sum(x => x.CalibrationValueUsing(FindDigit.FromNumber, FindDigit.FromString));
    }
}