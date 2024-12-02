using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d9.aoc._24.day02;
[SolutionToProblem(2)]
public class Solution : AocSolution
{
    public override IEnumerable<AocPartialResult> Solve(string[] lines)
    {
        IEnumerable<Report> reports = lines.Select(x => new Report(x));
        yield return reports.Count(x => x.IsStrictlySafe);
        yield return reports.Count(x => x.IsLooselySafe);
        static string s(bool b)
            => b ? "T " : " F";
        foreach (Report report in reports.OrderBy(x => x.IsStrictlySafe).ThenBy(x => x.IsLooselySafe))
            Console.WriteLine($"{s(report.IsStrictlySafe)}{s(report.IsLooselySafe)}"
                            + $"{report,-33} {report.Deltas
                                                    .Order()
                                                    .Select(x => $"{x,2}")
                                                    .ListNotation()}");
    }
}
