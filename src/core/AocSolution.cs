using System.Diagnostics;
using System.Reflection;

namespace d9.aoc.core;
public abstract class AocSolution(string inputFolder)
{
    public readonly string InputFolder = inputFolder;
    public string InputFile => Path.Join(InputFolder, $"{Day:00}.input");
    public int Day => Attribute.Day;
    public int StartingIndex => Attribute.StartingIndex;
    public SolutionToProblemAttribute Attribute 
        => GetType().GetCustomAttribute<SolutionToProblemAttribute>()
            ?? throw new Exception($"{GetType().Name} must have a SolutionToProblem attribute to run properly!");
    public void Execute()
    {
        Console.WriteLine($"Solution for Problem {Attribute.Day}:");
        int partIndex = StartingIndex;
        Stopwatch stopwatch = new();
        stopwatch.Start();
        foreach (object part in Solve(File.ReadAllLines(InputFile)))
        {
            stopwatch.Stop();
            if (partIndex < 1)
            {
                Console.WriteLine($"\tpreinit:\t{"",15}\t{$"{stopwatch.Elapsed:c}",16}");
                partIndex++;
            }
            else
            {
                Console.WriteLine($"\tPart {partIndex++}:\t{part,16}\t{$"{stopwatch.Elapsed:c}",16}");
            }
            stopwatch.Restart();
        }
    }
    public abstract IEnumerable<AocSolutionPart> Solve(params string[] lines);
    public static AocSolution? From(Type implementingType, string inputFolder)
        => Activator.CreateInstance(implementingType, inputFolder) as AocSolution;
}