using System.Diagnostics;
using System.Reflection;

namespace d9.aoc.core;
public abstract class AocSolution
{
    public string InputFileName => $"{Day:00}.input";
    public int Day => Attribute.Day;
    public static AocSolution? From(Type implementingType, string inputFolder)
        => Activator.CreateInstance(implementingType, inputFolder) as AocSolution;
    public SolutionToProblemAttribute Attribute 
        => GetType().GetCustomAttribute<SolutionToProblemAttribute>()
            ?? throw new Exception($"{GetType().Name} must have a SolutionToProblem attribute to run properly!");
    public IEnumerable<AocSolutionPart> Execute(string inputFolder)
    {
        int partIndex = 1;
        Stopwatch stopwatch = new();
        stopwatch.Start();
        foreach (AocPartialResult result in Solve(File.ReadAllLines(Path.Join(inputFolder, InputFileName))))
        {
            stopwatch.Stop();
            yield return new(result, partIndex, stopwatch.Elapsed);
            if (result.Label is null)
                partIndex++;
            stopwatch.Restart();
        }
    }
    public IEnumerable<string> ResultLines(string inputFolder)
    {
        yield return $"Solution for Problem {Attribute.Day:00}:";
        foreach (AocSolutionPart part in Execute(inputFolder))
            yield return $"\t{part}";
    }
    public abstract IEnumerable<AocPartialResult> Solve(params string[] lines);
}