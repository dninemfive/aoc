using d9.aoc.core;
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
            foreach (string line in solution.TestFinalResults(group.InputFolder))
                Console.WriteLine($"\t{line}");
        }
    }
    private static string[]? TryReadAllLines(string path)
    {
        try
        {
            return File.ReadAllLines(path);
        }
        catch
        {
            return null;
        }
    }
    public static IEnumerable<string> TestExampleResults(this AocSolution solution, string inputFolder)
    {
        string[]? generalFile = TryReadAllLines(solution.FileName(example: true));
        // for each part:
        foreach((int i, AocSolutionResult part) in solution.Execute(inputFolder).Parts)
        {

        }
        //  try to get specific file
        //      test on specific ?? general ?? throw exception
    }
    public static IEnumerable<string> TestFinalResults(this AocSolution solution, string inputFolder)
    {
        if(solution.GetType().GetCustomAttribute<FinalResultsAttribute>() is FinalResultsAttribute attr)
        {
            yield return $"Testing solution for day {solution.Day}...";
            foreach ((int i, AocSolutionResult part) in solution.Execute(inputFolder).Parts)
            {
                Assert.AreEqual(attr.ExpectedResults[i - 1], part.Value);
                yield return $"\tPart {i} succeeded!";
            }
        }
        else
        {
            yield return $"Couldn't find {typeof(FinalResultsAttribute).Name} for day {solution.Day}!";
        }
    }
}
