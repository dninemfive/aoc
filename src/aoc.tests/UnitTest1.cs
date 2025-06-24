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
    public static string ImplementationName(int day, int part, bool sample)
        => $"Day {day:00}, part {part:00}{(sample ? " (sample)" : "")}";
    public static IEnumerable<TestCaseData> TestCasesFor(int day, int part, AocPartImplementation impl)
    {
        TestCaseData result = new(impl);
        if(impl.ExpectedResults is ExpectedResultsAttribute era)
        {
            if (era.Sample is not null)
                yield return result.Returns(era.Sample).SetName(ImplementationName(day, part, true));
            if (era.Final is not null)
                yield return result.Returns(era.Final).SetName(ImplementationName(day, part, false));
        }
    }
    public static IEnumerable<TestCaseData> TestCasesFor(int year, AocSolution solution)
    {
        foreach ((int day, AocPartImplementation impl) in solution.ImplementedParts.OrderBy(x => x.Key))
            foreach (TestCaseData datum in TestCasesFor(year, day, impl))
                yield return datum.SetCategory($"{year} Day {solution.Day}");
    }
    public static IEnumerable<TestCaseData> _data;
    [TestCaseSource(nameof(_data))]
    public void TestDay(AocSolution solution)
    {
        AocSolutionResults result = solution.Execute()
    }
}
