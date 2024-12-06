namespace d9.aoc._24.day05;
[SolutionToProblem(5)]
[SampleResults(143)]
internal class Solution : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(params string[] lines)
    {
        List<Rule> allRules = new();
        List<IEnumerable<int>> allUpdates = new();
        bool rule = true;
        foreach (string line in lines)
        {
            if (line.IsNullOrEmpty())
            {
                rule = false;
                continue;
            }
            if(rule)
            {
                allRules.Add(new Rule(line));
            }
            else
            {
                allUpdates.Add(line.ToMany<int>(","));
            }
        }
        yield return "preinit";
        yield return allUpdates.Count(u => !allRules.Any(r => u.Violates(r)));
    }
}
