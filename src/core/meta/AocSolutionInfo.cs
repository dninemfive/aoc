using System.Diagnostics;
using System.Reflection;

namespace d9.aoc.core;
public record AocSolutionInfo(int Year, Type ImplementingType)
{
    public AocSolution Instantiate(string filePath, out TimeSpan initTime)
    {
        string[] lines = File.ReadAllLines(filePath);
        Stopwatch sw = Stopwatch.StartNew();
        AocSolution instance = (AocSolution)Activator.CreateInstance(ImplementingType, lines)!;
        sw.Stop();
        initTime = sw.Elapsed;
        return instance;
    }
    public IEnumerable<AocPartImplementation> ImplementedParts
    {
        get
        {
            if (GetType().GetMethod("Part1") is MethodInfo mi1 && mi1.IsOverride())
                yield return new(1, mi1);
            if (GetType().GetMethod("Part2") is MethodInfo mi2 && mi2.IsOverride())
                yield return new(2, mi2);
        }
    }
}