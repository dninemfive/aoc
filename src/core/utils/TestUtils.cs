using d9.utl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;
using System.Reflection;
using static d9.aoc.core.Constants;

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
    public static void TestAll(this AocSolutionGroup group)
    {
        Console.WriteLine($"Testing solutions for {group.Year}...");
        bool anyFailed = false;
        foreach(AocSolution solution in group)
        {
            Assert.IsNotNull(solution);
            Console.WriteLine($"{TAB}Day {solution.Day,2}:");
            if (solution.GetType().HasCustomAttribute<DisableTestsAttribute>())
            {
                Console.WriteLine($"{TAB}{TAB}Skipping tests...");
                continue;
            }
            foreach (string line in solution.TestResults(group.InputFolder))
            {
                Console.WriteLine($"{TAB}{TAB}{line}");
                if(line.Contains("Failed"))
                    anyFailed = true;
            }
        }
        if (anyFailed)
            Assert.Fail();
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
                yield return $"{(useSampleData ? "Sample" : "Final")}:";
                foreach ((int i, object expected) in expectedResults)
                {
                    string specificFileName = solution.FileName(useSampleData, i);
                    string[]? specificData = TryReadAllLines(inputFolder, specificFileName);
                    string[] data = specificData
                                ?? generalData
                                ?? throw new Exception($"Couldn't find either {generalFileName} or {specificFileName}!");
                    AocSolutionResults actual = solution.Execute(data);
                    Exception? exception = null;
                    try
                    {
                        if (actual[i].Value is BigInteger bi)
                        {
                            if(expected is int z)
                            {
                                Assert.AreEqual(new BigInteger(z), bi);
                            } 
                            else if(expected is long l)
                            {
                                Assert.AreEqual(new BigInteger(l), bi);
                            } 
                            else
                            {
                                Assert.AreEqual(expected, actual[i].Value);
                            }
                        }
                        else
                        {
                            Assert.AreEqual(expected, actual[i].Value);
                        }
                    }
                    catch(Exception e)
                    {
                        exception = e;
                    }
                    if(exception is null)
                    {
                        yield return $"{TAB}Part {i} succeeded!";
                    }
                    else
                    {
                        yield return $"{TAB}Part {i} failed: {exception.Summary()}";
                    }
                }
            }
        }
        else
        {
            yield return $"Couldn't find {typeof(ExpectedResultsAttribute).Name} for day {solution.Day}!";
        }
    }
}
