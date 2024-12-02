using d9.aoc.core;
using System.Reflection;

namespace d9.aoc._23.tests;
public static class Utils
{
    public static void AssertSolution(AocSolution solution, params object[] expected)
    {
        Assert.IsNotNull(solution);
        object[] actual = solution.Solve(InputForProblem(solution.Day)).ToArray();
        int offset = stpa.HasStartupMarker ? 1 : 0;
        for (int i = 0; i < expected.Length; i++)
            Assert.AreEqual(expected[i], actual[i + offset]);
    }
    public static string[] InputForProblem(int problemIndex)
        => File.ReadAllLines(Path.Join(Program.INPUT_FOLDER, $"{problemIndex}.input"));
}
