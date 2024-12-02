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
        foreach (AocSolutionPart part in solution.Execute(group.InputFolder))
            if (part.Result.Label is null)
                Assert.AreEqual(expectedValues[part.Index - 1], part.Result.Value);
    }
}
