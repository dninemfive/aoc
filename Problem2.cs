using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aoc_solns_2023;
public class Problem2 : Problem1
{
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
    protected override int GetDigit(string line, bool first)
    {
        for(int i = 0; i < DIGIT_STRINGS.Length; i++)
        {
            line = line.Replace(DIGIT_STRINGS[i], $"{i}");
        }
        return base.GetDigit(line, first);
    }
}
