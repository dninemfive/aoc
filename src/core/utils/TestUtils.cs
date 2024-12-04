using d9.aoc.core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d9.aoc.core.test;
public static class TestUtils
{
    public static void Test(this AocSolutionGroup group, int index, params object[] expectedValues)
    {
        AocSolution solution = group[index];
        Assert.IsNotNull(solution);
        foreach ((int i, AocSolutionResult part) in solution.Execute(group.InputFolder).Parts)
            if (part.Result.Label is null)
                Assert.AreEqual(expectedValues[i - 1], part.Value);
    }
    public static void Test(this AocSolutionGroup group)
    {
        foreach(AocSolution solution in group)
        {
            Assert.IsNotNull(solution);
            // flatten annotations so that for each part you have string[] input
        }
    }
    public static void TestFinalResults(this AocSolution solution)
    {
    }
}
