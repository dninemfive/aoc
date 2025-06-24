using d9.aoc.core;
using d9.utl;
using d9.utl.utils.reflection;
using System.Reflection;

namespace aoc.tests;
public class Tests
{
    [SetUp]
    public void Setup()
    {
        foreach((Assembly assembly, SolutionsForYearAttribute sfy) in AppDomain.CurrentDomain.AssembliesWithAttribute<SolutionsForYearAttribute>())
        {
            foreach ((Type type, SolutionToProblemAttribute stp) in assembly.TypesWithAttribute<SolutionToProblemAttribute>())
            {

            }
        }
    }
    public static string ImplementationName(int part, bool sample)
        => $"Part {part:00}{(sample ? " (sample)" : "")}";
    public static IEnumerable<TestCaseData> TestCasesFor(int part, AocPartImplementation impl)
    {
        TestCaseData result = new(impl);
        if(impl.ExpectedResults is ExpectedResultsAttribute era)
        {
            if (era.Sample is not null)
                yield return result.Returns(era.Sample).SetName(ImplementationName(part, true));
            if (era.Final is not null)
                yield return result.Returns(era.Final).SetName(ImplementationName(part, false));
        }
    }
    public static IEnumerable<TestCaseData> TestCasesFor(int year, AocSolutionInfo soln)
    {
        foreach (AocPartImplementation impl in soln.ImplementedParts)
            foreach (TestCaseData datum in TestCasesFor(impl.Part, impl))
                yield return datum.SetCategory($"Day {soln.Day}");
    }
    public static IEnumerable<TestCaseData> _data;
    [TestCaseSource(nameof(_data))]
    public void TestDay(AocSolution solution)
    {
        AocSolutionResults result = solution.Execute()
    }
}
