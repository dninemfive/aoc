using d9.aoc.core;
using d9.utl;
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
            foreach(Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if(assembly.GetCustomAttribute<SolutionsForYearAttribute>() is SolutionsForYearAttribute sfy)
                {
                    foreach(Type type in assembly.GetTypes())
                    {
                        if(type.GetCustomAttribute<SolutionToProblemAttribute>() is SolutionToProblemAttribute stp)
                        {
                            if(type.GetCustomAttribute<SampleResultsAttribute>() is SampleResultsAttribute sr)
                            {

                            }
                        }
                    }
                }
            }
        }
    }

    [TestCaseSource(nameof(_data))]
    public void Test1()
    {
        Assert.Pass();
    }
}
