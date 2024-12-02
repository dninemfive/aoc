using System.Diagnostics;
using System.Reflection;

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
    public IEnumerable<AocSolutionPart> Execute(string inputFolder)
    {
        int partIndex = 1;
        Stopwatch stopwatch = new();
        stopwatch.Start();
        foreach (AocPartialResult result in Solve(File.ReadAllLines(Path.Join(inputFolder, FileName))))
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
        yield return $"Solution for Problem {Day:00}:";
        foreach (AocSolutionPart part in Execute(inputFolder))
            yield return $"\t{part}";
    }
    public abstract IEnumerable<AocPartialResult> Solve(params string[] lines);
}