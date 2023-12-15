using d9.aoc._23.day1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace d9.aoc._23.tests;
public static class Utils
{
    public static void AssertSolution(Func<string[], IEnumerable<object>> method, params object[] expected)
    {
        (_, SolutionToProblemAttribute? stpa) = method.GetMethodInfo().MethodAndAttribute();
        Assert.IsNotNull(stpa);
        object[] actual = method(InputForProblem(stpa.Index)).ToArray();
        int offset = stpa.HasStartupMarker ? 1 : 0;
        for (int i = 0; i < expected.Length; i++)
            Assert.AreEqual(expected[i], actual[i + offset]);
    }
    public static string[] InputForProblem(int problemIndex)
        => File.ReadAllLines(Path.Join(Program.INPUT_FOLDER, $"{problemIndex}.input"));
}
