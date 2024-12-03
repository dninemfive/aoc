using System.Text.RegularExpressions;

namespace d9.aoc._24.day03;
[SolutionToProblem(3)]
internal partial class Solution : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(params string[] lines)
    {
        IEnumerable<Union<bool, int>> instructions = lines.SelectMany(x => _anyInstruction.Matches(x))
                                                          .Select(x => x.Value.Evaluate());
        yield return "preinit";
        yield return instructions.Select(x => x.As<int>())
                                 .Where(x => x is not null)
                                 .Sum()!;
        bool active = true;
        int sum = 0;
        foreach(Union<bool, int> instruction in instructions)
        {
            Console.WriteLine(instruction);
            if (instruction.As<bool>() is bool b)
            {
                active = b;
            }
            else if(instruction.As<int>() is int i)
            {
                if (active)
                    sum += i;
            }
        }
        yield return sum;
    }
    private static Regex _anyInstruction = AnyInstruction();
    // til about the non-capturing modifier: https://stackoverflow.com/a/632248
    [GeneratedRegex(@"(?:mul\(\d+,\d+\)|do\(\)|dont\(\))")]
    private static partial Regex AnyInstruction();
}
