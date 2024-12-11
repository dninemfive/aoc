using d9.utl;
using System.Numerics;

namespace d9.aoc.core;
public record AocPartialResult(object? Value, string? Label = null)
{
    public static implicit operator AocPartialResult((object result, string label) tuple)
        => new(tuple.result, tuple.label);
    public static implicit operator AocPartialResult(string result)
        => new(null, result);
    public static implicit operator AocPartialResult(int result)
        => new(result, null);
    public static implicit operator AocPartialResult(long result)
        => new(result, null);
    public static implicit operator AocPartialResult(BigInteger result)
        => new(result, null);
    public override string ToString()
    {
        string label = Label is not null ? $"{Label}: " : "";
        return $"{label}{Value.PrintNull()}";
    }
}
public record AocSolutionResult(AocPartialResult Result, string Label, TimeSpan Elapsed)
{
    public object? Value => Result.Value;
    public override string ToString()
        => $"{Label}:\t{Result.Value,16}\t{Elapsed,16:c}";
}