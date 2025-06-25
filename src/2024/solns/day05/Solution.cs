using System.Data;

namespace d9.aoc._24.day05;
[SolutionToProblem(5)]
internal class Solution : AocSolution
{
    public IReadOnlyList<Rule> Rules;
    public IReadOnlyList<Update> Updates;
    public Solution(params string[] lines)
    {
        (Rules, Updates) = ParseInput(lines);
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
    [ExpectedResults(sample: 143, final: 7074)]
    public override AocPartResultValue Part1()
        => Updates.Where(x => !x.ViolatesAny(Rules))
                  .Select(x => x.MiddleValue)
                  .Sum();
}
