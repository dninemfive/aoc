using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace d9.aoc.core;
public class AocPartImplementation(int part, MethodInfo mi)
{
    public delegate AocPartResult? Delegate(AocSolution soln);
    public int Part => part;
    public ExpectedResultsAttribute? ExpectedResults = mi.GetCustomAttribute<ExpectedResultsAttribute>();
    public Func<AocSolution, AocPartResultValue?> Function 
        => (parent) => mi.Invoke(parent, null) as AocPartResultValue;
    public AocPartResult? Execute(AocSolution parent)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        AocPartResultValue? result = Function(parent);
        stopwatch.Stop();
        return result is not null
             ? new($"Part {Part}", result, stopwatch.Elapsed) 
             : null;
    }
    public void Deconstruct(out int part, out Delegate execute)
    {
        part = Part;
        execute = Execute;
    }
}
