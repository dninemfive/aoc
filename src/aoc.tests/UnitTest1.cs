using d9.aoc.core;
using d9.utl;
using d9.utl.utils.reflection;
using System.Reflection;

namespace aoc.tests;
public class Tests
{
    public record TestElement(int Year, int Day, AocSolution Solution, ExpectedResultsAttribute Expected);
    [SetUp]
    public void Setup()
    {
        foreach(Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            if(assembly.GetCustomAttribute<SolutionsForYearAttribute>() is SolutionsForYearAttribute attr)
            {

            }
        }
    }
    public static IEnumerable<TestCaseData> TestCasesFor(int year, AocSolution solution, ExpectedResultsAttribute expectedResults)
    {
        TestCaseData result = new TestCaseData(solution).Returns(expectedResults)
                                                        .SetCategory(year.ToString())
                                                        .SetName($"Day {solution.Day} {(expectedResults.UseSampleData ? "(sample)" : "")}");
        result.SetProperty("fileName", "fileName");
    }
    public static IEnumerable<TestCaseData> _data;
    [TestCaseSource(nameof(_data))]
    public void TestDay(AocSolution solution)
    {
        AocSolutionResults result = solution.Execute()
    }
}
