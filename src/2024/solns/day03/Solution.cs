using System.Text.RegularExpressions;

namespace d9.aoc._24.day03;
[SolutionToProblem(3)]
internal partial class Solution(params string[] lines)
    : AocSolution
{
    public readonly IEnumerable<Union<bool, int>> Instructions
        = lines.SelectMany(x => _anyInstruction.Matches(x))
               .Select(x => x.Value.Evaluate());

    [ExpectedResults(sample: 161, final: 179834255)]
    public override AocPartResultValue? Part1()
        => Instructions.Select(x => x.As<int>()).Sum()!;

    [ExpectedResults(sample: 48, final: 80570939)]
    public override AocPartResultValue? Part2()
    {
        bool active = true;
        int sum = 0;
        foreach (Union<bool, int> instruction in Instructions)
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

    private static readonly Regex _anyInstruction = AnyInstruction();
    // til about the non-capturing modifier: https://stackoverflow.com/a/632248
    [GeneratedRegex(@"(?:mul\(\d+,\d+\)|do\(\)|don't\(\))")]
    private static partial Regex AnyInstruction();
}
