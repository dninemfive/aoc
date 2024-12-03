using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace d9.aoc._24.day03;
internal partial class MulInstruction(int left, int right)
{
    public static Regex Regex = MulRegex();
    public int Value => left * right;
    public static MulInstruction FromMatch(string match)
    {
        if (!Regex.IsMatch(match))
            throw new Exception($"{match} does not match the regex for {typeof(MulInstruction).Name!}");
        List<string> split = match.SplitAndTrim("(", ",", ")");
        return new(int.Parse(split[1]), int.Parse(split[2]));
    }

    [GeneratedRegex(@"mul\(\d+,\d+\)")]
    private static partial Regex MulRegex();
}
