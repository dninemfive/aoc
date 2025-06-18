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
        foreach(Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            if(assembly.GetCustomAttribute<SolutionsForYearAttribute>() is SolutionsForYearAttribute attr)
            {

            }
        }
    }
    public IEnumerable<TestCaseData> AllTests
    {
        get
        {
            foreach((Assembly assembly, SolutionsForYearAttribute sfy) in AppDomain.CurrentDomain.AssembliesWithAttribute<SolutionsForYearAttribute>())
            {
                foreach((Type type, SolutionToProblemAttribute stp) in assembly.TypesWithAttribute<SolutionToProblemAttribute>())
                {
                    if(type.GetCustomAttribute<SampleResultsAttribute>() is SampleResultsAttribute sr)
                    {
                        yield return MakeTestCaseFor(sfy.Year, stp.Day, sr)
                    }
                }
            }
        }
    }
    /*
        => new TestCaseData(fileName).Returns(expected)
                                     .SetCategory($"Year {year}")
                                     .SetName($"Day {problem,2} Problem {}");
    */
    public IEnumerable<(AocSolution soln, IEnumerable<(TestCaseData data)>)>
    public TestCaseData MakeTestCaseFor(AocSolution solution)
        => new TestCaseData(solution.FileName)

    [TestCaseSource(nameof(_data))]
    public void Test1()
    {
        Assert.Pass();
    }
}
