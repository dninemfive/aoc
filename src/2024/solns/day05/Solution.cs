using System.Data;

namespace d9.aoc._24.day05;
[SolutionToProblem(5)]
[SampleResults(143)]
internal class Solution : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(params string[] lines)
    {
        (List<Rule> allRules, List<Update> allUpdates) = ParseInput(lines);
        yield return "preinit";
        yield return allUpdates.Where(x => x.ViolatesAny(allRules))
                               .Select(x => x.MiddleValue)
                               .Sum();
    }
    public static (List<Rule> rules, List<Update> updates) ParseInput(string[] lines)
    {
        List<Rule> rules = new();
        List<Update> updates = new();
        bool rule = true;
        foreach (string line in lines)
        {
            if (line.IsNullOrEmpty())
            {
                rule = false;
                continue;
            }
            if (rule)
            {
                rules.Add(new(line));
            }
            else
            {
                updates.Add(new(line));
            }
        }
        return (rules, updates);
    }
}
