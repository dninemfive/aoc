using d9.aoc.core.meta;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace d9.aoc.core;
#pragma warning disable CS9113 // unread parameter: used by derived classes
public abstract class AocSolution(params string[] lines)
#pragma warning restore CS9113
{
    public delegate AocPartResultValue? Implementation();
    public SolutionToProblemAttribute Attribute
        => GetType().GetCustomAttribute<SolutionToProblemAttribute>()
        ?? throw new Exception($"{GetType().Name} must have a SolutionToProblemAttribute to run properly!");
    public int Day => Attribute.Day;
    public string BaseFileName => $"day{Day:00}";
    public string FileName(bool sample = false, int? index = null)
    {
        string result = BaseFileName;
        if (sample)
            result += ".sample";
        if (index is int i)
            result += $".{i:00}";
        return $"{result}.txt";
    }
    public virtual AocPartResultValue? Part1()
        => null;
    public virtual AocPartResultValue? Part2()
        => null;
    public bool Execute(string name, Implementation implementation, [NotNullWhen(true)] out AocPartResult? result)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        if(implementation() is AocPartResultValue partResult)
        {
            result = new(partResult, name, stopwatch.Elapsed);
            return true;
        }
        result = null;
        return false;
    }
    public IReadOnlyDictionary<int, AocPartImplementation> ImplementedParts
    {
        get
        {
            Dictionary<int, AocPartImplementation> result = new();
            if (GetType().GetMethod("Part1") is MethodInfo mi1 && mi1.IsOverride())
                result[1] = new(this, mi1);
            if (GetType().GetMethod("Part2") is MethodInfo mi2 && mi2.IsOverride())
                result[2] = new(this, mi2);
            return result;
        }
    }
}