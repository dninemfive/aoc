using System.Reflection;

namespace d9.aoc.core;
public class AocPartImplementation(MethodInfo info)
{
    public ExpectedResultsAttribute? ExpectedResults = info.GetCustomAttribute<ExpectedResultsAttribute>();
    public Func<AocSolution, AocPartResultValue?> Function 
        => (parent) => info.Invoke(parent, null) as AocPartResultValue;
}
