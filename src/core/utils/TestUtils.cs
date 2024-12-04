using d9.aoc.core;
using d9.aoc.core.meta;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        Console.WriteLine($"Testing solutions for {group.Year}...");
        foreach(AocSolution solution in group)
        {
            Assert.IsNotNull(solution);
            foreach (string line in solution.TestResults(group.InputFolder))
                Console.WriteLine($"\t{line}");
        }
    }
    private static string[]? TryReadAllLines(params string[] path)
    {
        try
        {
            return File.ReadAllLines(Path.Join(path));
        }
        catch
        {
            return null;
        }
    }
    public static IEnumerable<string> TestResults(this AocSolution solution, string inputFolder)
    {
        IEnumerable<ExpectedResultsAttribute> attrs = solution.GetType().GetCustomAttributes<ExpectedResultsAttribute>();
        if(attrs.Any())
        {
            foreach(ExpectedResultsAttribute expectedResults in attrs)
            {
                bool useSampleData = expectedResults.UseSampleData;
                string generalFileName = solution.FileName(useSampleData);
                string[]? generalData = TryReadAllLines(inputFolder, solution.FileName(useSampleData));
                yield return $"Testing solution for day {solution.Day} on {(useSampleData ? "sample" : "final")} data...";
                foreach ((int i, object expected) in expectedResults)
                {
                    string specificFileName = solution.FileName(useSampleData, i);
                    string[]? specificData = TryReadAllLines(inputFolder, specificFileName);
                    string[] data = specificData
                                ?? generalData
                                ?? throw new Exception($"Couldn't find either {generalFileName} or {specificFileName}!");
                    AocSolutionResults actual = solution.Execute(data);
                    Assert.AreEqual(expected, actual[i]);
                    yield return $"\tPart {i} succeeded!";
                }
            }
        }
        else
        {
            yield return $"Couldn't find {typeof(ExpectedResultsAttribute).Name} for day {solution.Day}!";
        }
    }
}
