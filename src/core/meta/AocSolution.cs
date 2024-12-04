using System.Diagnostics;
using System.Reflection;
using static d9.aoc.core.meta.Constants;

namespace d9.aoc.core;
public abstract class AocSolution
{
    public AocSolution() { }
    public string FileName => $"day{Day:00}.txt";
    public int Day => Attribute.Day;
    public static AocSolution? Instantiate(Type implementingType)
        => Activator.CreateInstance(implementingType) as AocSolution;
    public SolutionToProblemAttribute Attribute 
        => GetType().GetCustomAttribute<SolutionToProblemAttribute>()
            ?? throw new Exception($"{GetType().Name} must have a SolutionToProblem attribute to run properly!");
    public AocSolutionResults Execute(string[] lines)
    {
        int partIndex = 1;
        Stopwatch stopwatch = new();
        AocSolutionResults results = new();
        stopwatch.Start();
        foreach (AocPartialResult result in Solve(lines))
        {
            stopwatch.Stop();
            results.Add(result, stopwatch.Elapsed, result.Label is null ? partIndex++ : null);
            stopwatch.Restart();
        }
        return results;
    }
    public AocSolutionResults Execute(string inputFolder)
        => Execute(File.ReadAllLines(Path.Join(inputFolder, FileName)));
    public IEnumerable<string> ResultLines(string inputFolder)
    {
        yield return $"Day {Day,2}:";
        foreach (AocSolutionResult part in Execute(inputFolder))
            yield return $"{TAB}{part}";
    }
    public abstract IEnumerable<AocPartialResult> Solve(params string[] lines);
}