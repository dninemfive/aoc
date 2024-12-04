using d9.utl;
using System.Reflection.Emit;

namespace d9.aoc.core;
public record AocPartialResult(object Value, string? Label = null)
{
    public static implicit operator AocPartialResult((object result, string label) tuple)
        => new(tuple.result, tuple.label);
    public static implicit operator AocPartialResult(string result)
        => new(0b0, result);
    public static implicit operator AocPartialResult(int result)
        => new(result, null);
    public static implicit operator AocPartialResult(long result)
        => new(result, null);
}
public record AocSolutionPart(AocPartialResult Result, int? Index, TimeSpan Elapsed)
{
    public string Label
        => Result.Label ?? $"Part {Index,2}";
    public string ValueString
        => Result.Label is null
            ? Result.Value.PrintNull()
            : "";
    public override string ToString()
        => $"{Label}:\t{ValueString,16}\t{Elapsed,16:c}";
}