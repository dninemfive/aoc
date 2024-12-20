﻿using System.Text.RegularExpressions;

namespace d9.aoc._24.day03;
[SolutionToProblem(3)]
[SampleResults(161, 48)]
[FinalResults(179834255, 80570939)]
internal partial class Solution : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(params string[] lines)
    {
        IEnumerable<Union<bool, int>> instructions = lines.SelectMany(x => _anyInstruction.Matches(x))
                                                          .Select(x => x.Value.Evaluate());
        yield return "preinit";
        yield return instructions.Select(x => x.As<int>())
                                 .Sum()!;
        yield return SumOnlyWhileActive(instructions);
    }
    public int SumOnlyWhileActive(IEnumerable<Union<bool, int>> instructions)
    {
        bool active = true;
        int sum = 0;
        foreach (Union<bool, int> instruction in instructions)
        {
            if (instruction.IsT1(out bool b))
            {
                active = b;
            }
            else if (instruction.IsT2(out int i))
            {
                if (active)
                    sum += i;
            }
        }
        return sum;
    }
    private static Regex _anyInstruction = AnyInstruction();
    // til about the non-capturing modifier: https://stackoverflow.com/a/632248
    [GeneratedRegex(@"(?:mul\(\d+,\d+\)|do\(\)|don't\(\))")]
    private static partial Regex AnyInstruction();
}
