using d9.aoc.core;

namespace d9.aoc._23.day1;
public static class Solution
{
    public delegate (int digit, int index)? DigitFinder(string line, bool first);
    [SolutionToProblem(1)]
    public static IEnumerable<object> Solve(string[] lines)
    {
        yield return lines.Sum(x => x.CalibrationValueUsing(FindDigit.FromNumber));
        yield return lines.Sum(x => x.CalibrationValueUsing(FindDigit.FromNumber, FindDigit.FromString));
    }
    public static int CalibrationValueUsing(this string line, params DigitFinder[] digitFinders)
        => FirstDigit(line, digitFinders) * 10 + LastDigit(line, digitFinders);
    public static int GetDigit(this string line, bool first, params DigitFinder[] digitFinders)
    {
        (int digit, int index)? result = null;
        foreach(DigitFinder digitFinder in digitFinders)
        {
            (int digit, int index)? cur = digitFinder(line, first);
            if (cur is null)
                continue;
            if(result is null || cur.Value.index < result.Value.index == first)
                result = cur;
        }
        return result?.digit ?? throw new Exception($"Failed to find a digit value in string {line}!");
    }
    public static int FirstDigit(this string line, params DigitFinder[] digitFinders)
        => GetDigit(line, true, digitFinders);
    public static int LastDigit(this string line, params DigitFinder[] digitFinders)
        => GetDigit(line, false, digitFinders);
}