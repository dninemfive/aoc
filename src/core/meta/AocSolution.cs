using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace d9.aoc.core;
#pragma warning disable CS9113 // unread parameter: used by derived classes
// todo: split into:
// - AocSolution: base class for solution implementations
// - AocSolutionInfo: metadata holder & instantiator for AocSolution
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
}