namespace d9.aoc._23.day01;

internal delegate (int digit, int index)? DigitFinder(string line, bool first);
internal static class Extensions
{
    internal static int CalibrationValueUsing(this string line, params DigitFinder[] digitFinders)
        => FirstDigit(line, digitFinders) * 10 + LastDigit(line, digitFinders);
    internal static int GetDigit(this string line, bool first, params DigitFinder[] digitFinders)
    {
        (int digit, int index)? result = null;
        foreach (DigitFinder digitFinder in digitFinders)
        {
            (int digit, int index)? cur = digitFinder(line, first);
            if (cur is null)
                continue;
            if (result is null || cur.Value.index < result.Value.index == first)
                result = cur;
        }
        return result?.digit ?? throw new Exception($"Failed to find a digit value in string {line}!");
    }
    internal static int FirstDigit(this string line, params DigitFinder[] digitFinders)
        => GetDigit(line, true, digitFinders);
    internal static int LastDigit(this string line, params DigitFinder[] digitFinders)
        => GetDigit(line, false, digitFinders);
}
