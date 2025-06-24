using d9.utl;
using System.Diagnostics;
using System.Reflection;

namespace d9.aoc.core;
public record AocSolutionInfo(Type ImplementingType)
{
    private Assembly Assembly => ImplementingType.Assembly;
    private SolutionsForYearAttribute AssemblyAttr
        => Assembly.GetCustomAttribute<SolutionsForYearAttribute>()
        ?? throw new Exception($"Assembly implementing type {ImplementingType.Name} ({Assembly.GetName()}) needs a SolutionsForYearAttribute to function!");
    private SolutionToProblemAttribute DayAttr 
        => ImplementingType.GetCustomAttribute<SolutionToProblemAttribute>() 
        ?? throw new Exception($"Type {ImplementingType.Name} needs a SolutionToProblemAttribute to function!");
    public int Year => AssemblyAttr.Year;
    public int Day => DayAttr.Day;
    public string FileName(bool sample = false, int? part = null)
    {
        string resultBase = $"{Path.GetDirectoryName(Assembly.Location)!.Ancestor(4)}/{Year}/input/day{Day:00}";
        if (sample)
            resultBase += ".sample";
        if (part is int i)
        {
            string specificResult = $"{resultBase}.{i:00}.txt";
            if (File.Exists(specificResult))
                return specificResult;
        }
        return $"{resultBase}.txt";
    }
    public AocSolution Instantiate(out TimeSpan initTime, bool sample = false, int? part = null)
        => Instantiate(FileName(sample, part), out initTime);
    public AocSolution Instantiate(string filePath, out TimeSpan initTime)
    {
        string[] lines = File.ReadAllLines(filePath);
        Stopwatch sw = Stopwatch.StartNew();
        Console.WriteLine(ImplementingType.GetConstructors().ListNotation());
        AocSolution instance = (AocSolution)Activator.CreateInstance(ImplementingType, args: [lines])!;
        sw.Stop();
        initTime = sw.Elapsed;
        return instance;
    }
    public IEnumerable<AocPartImplementation> ImplementedParts
    {
        get
        {
            if (ImplementingType.GetMethod("Part1") is MethodInfo mi1 && mi1.IsOverride())
                yield return new(1, mi1);
            if (ImplementingType.GetMethod("Part2") is MethodInfo mi2 && mi2.IsOverride())
                yield return new(2, mi2);
        }
    }
    public override string ToString()
        => $"{Year}-{Day:00} {ImplementedParts.Select(x => x.Part).ListNotation(brackets: ("(", ")"))}";
}