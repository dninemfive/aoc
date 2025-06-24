using d9.utl;
using System.Numerics;

namespace d9.aoc.core;
public record AocPartResult(AocPartResultValue Result, string Label, TimeSpan Elapsed)
{
    public object? Value => Result.Value;
    public override string ToString()
        => $"{Label}:\t{Result.Value,16}\t{Elapsed,16:c}";
}