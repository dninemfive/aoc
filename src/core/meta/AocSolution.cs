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
    public virtual AocPartResultValue? Part1()
        => null;
    public virtual AocPartResultValue? Part2()
        => null;
}