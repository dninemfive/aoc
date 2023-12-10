using d9.aoc._23.shared;

namespace d9.aoc._23.day1;
public static class FindDigit
{
    public static readonly string[] DIGIT_STRINGS = [
        "zero",
        "one",
        "two",
        "three",
        "four",
        "five",
        "six",
        "seven",
        "eight",
        "nine"
    ];
    public static (int digit, int index)? FromNumber(string line, bool first)
    {
        for (int i = 0; i < line.Length; i++)
        {
            int index = first ? i : line.Length - i - 1;
            char c = line[index];
            if (c.IsDigit())
                return (c.IntValue(), index);
        }
        return null;
    }
    public static (int digit, int index)? FromString(string line, bool first)
    {
        Func<string, int> indexOf = first ? line.IndexOf : line.LastIndexOf;
        (int digit, int index)? result = null;
        for (int digit = 0; digit < DIGIT_STRINGS.Length; digit++)
        {
            if (!line.Contains(DIGIT_STRINGS[digit]))
                continue;
            int index = indexOf(DIGIT_STRINGS[digit]);
            if (result is null || index < result.Value.index == first)
                result = (digit, index);
        }
        return result;
    }
}
