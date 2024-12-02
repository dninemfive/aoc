using d9.aoc.core;

namespace d9.aoc._23.day01;

[SolutionToProblem(1)]
public class Solution : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(string[] lines)
    {
        yield return lines.Sum(x => x.CalibrationValueUsing(FindDigit.FromNumber));
        yield return lines.Sum(x => x.CalibrationValueUsing(FindDigit.FromNumber, FindDigit.FromString));
    }
}