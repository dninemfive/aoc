using System.Numerics;
using System.Reflection;
using static d9.aoc.core.Constants;

namespace d9.aoc.core.test;
public static class TestUtils
{
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
                            if (expected is int z)
                            {
                                Assert.AreEqual(new BigInteger(z), bi);
                            }
                            else if (expected is long l)
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
