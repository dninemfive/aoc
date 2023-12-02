using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc_solns_2023;
public static class Problem1
{
    #region constants
    public static readonly string[] DIGIT_STRINGS =
    [
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
    #endregion constants
    [SolutionToProblem(1)]
    public static IEnumerable<object> Solve(string inputFile)
    {
        IEnumerable<string> lines = File.ReadAllLines(inputFile);
        static int calibrationValue(string line, params DigitFinder[] digitFinders)
            => FirstDigit(line, digitFinders) * 10 + LastDigit(line, digitFinders);
        yield return lines.Sum(x => calibrationValue(x, FindNumericDigit));
        yield return lines.Sum(x => calibrationValue(x, FindNumericDigit, FindStringDigit));
    }
    #region digitFinders
    public delegate (int digit, int index)? DigitFinder(string line, bool first);
    public static (int digit, int index)? FindNumericDigit(string line, bool first)
    {
        for(int i = 0; i < line.Length; i++)
        {
            int index = first ? i : line.Length - i - 1;
            char c = line[index];
            if (c is >= '0' and <= '9')
                return (c - '0', index);
        }
        return null;
    }
    public static (int digit, int index)? FindStringDigit(string line, bool first) 
    {
        Func<string, int> indexOf = first ? line.IndexOf : line.LastIndexOf;
        (int digit, int index)? result = null;
        for(int digit = 0; digit < DIGIT_STRINGS.Length; digit++)
        {
            if (!line.Contains(DIGIT_STRINGS[digit]))
                continue;
            // Console.WriteLine($"{line} contains {DIGIT_STRINGS[digit]}. Tuple: {result?.ToString() ?? "(null)"}");
            int index = indexOf(DIGIT_STRINGS[digit]);
            if(result is null || index < result.Value.index == first)
            {
                result = (digit, index);
            }
        }
        return result;
    }
    #endregion digitFinders
    public static int GetDigit(string line, bool first, params DigitFinder[] digitFinders)
    {
        (int digit, int index)? result = null;
        //Console.WriteLine($"{first}\t{line}");
        foreach(DigitFinder df in digitFinders)
        {
            (int digit, int index)? cur = df(line, first);
            //Console.WriteLine($"\t{df.Method.Name,-32}\t`{cur}`\t`{result}`");
            if (cur is null)
                continue;
            if(result is null || cur.Value.index < result.Value.index == first)
                result = cur;
        }
        return result?.digit ?? throw new Exception($"Failed to find a digit value in string {line}!");
    }
    public static int FirstDigit(string line, params DigitFinder[] digitFinders)
        => GetDigit(line, true, digitFinders);
    public static int LastDigit(string line, params DigitFinder[] digitFinders)
        => GetDigit(line, false, digitFinders);
}
