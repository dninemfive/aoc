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
        yield return "preinit";
        yield return reports.Count(x => x.IsStrictlySafe);
        yield return reports.Count(x => x.IsLooselySafe);
    }
}
