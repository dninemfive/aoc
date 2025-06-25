using d9.aoc.core;
using d9.utl.utils.reflection;
using System.Reflection;

namespace d9.aoc.test;
public class TestHelper
{
    private readonly List<AocSolutionInfo> _infos;
    public IEnumerable<AocSolutionInfo> Infos => _infos;
    private readonly List<TestCaseData> _cases;
    public IEnumerable<TestCaseData> Cases => _cases;
    public TestHelper()
    {
        object[] __ = [new _23.Program(), new _24.Program()];
        _infos = [];
        foreach ((Assembly assembly, SolutionsForYearAttribute _) in AppDomain.CurrentDomain.AssembliesWithAttribute<SolutionsForYearAttribute>())
            foreach ((Type type, SolutionToProblemAttribute _) in assembly.TypesWithAttribute<SolutionToProblemAttribute>())
                _infos.Add(new(type));
        _infos = [.. _infos.OrderBy(x => x.Year).ThenBy(x => x.Day)];
        _cases = [];
        foreach (AocSolutionInfo info in _infos)
            foreach (TestCaseData datum in TestCasesFor(info))
                _cases.Add(datum);
    }
    private static IEnumerable<TestCaseData> TestCasesFor(AocSolutionInfo info, AocPartImplementation impl)
    {
        Console.WriteLine($"\t\t\tTestCasesFor({info}, {impl})");
        if (impl.ExpectedResults is ExpectedResultsAttribute era)
        {
            Console.WriteLine($"\t\t\t\t{era}");
            if (era.Sample is not null)
                yield return new TestCaseData(info, impl, true).Returns(era.Sample);
            if (era.Final is not null)
                yield return new TestCaseData(info, impl, false).Returns(era.Final);
        }
    }
    private static IEnumerable<TestCaseData> TestCasesFor(AocSolutionInfo soln)
    {
        Console.WriteLine($"\t\t{soln}");
        foreach (AocPartImplementation impl in soln.ImplementedParts)
            foreach (TestCaseData datum in TestCasesFor(soln, impl))
                yield return datum.SetName($"Year {soln.Year} Day {soln.Day:00}");
    }
}