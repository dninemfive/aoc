using d9.utl;
using System.Numerics;

namespace d9.aoc.core;
public record AocPartResultValue(object? Value, string? Label = null)
{
    public static implicit operator AocPartResultValue((object result, string label) tuple)
        => new(tuple.result, tuple.label);
    public static implicit operator AocPartResultValue(string result)
        => new(null, result);
    public static implicit operator AocPartResultValue(int result)
        => new(result, null);
    public static implicit operator AocPartResultValue(long result)
        => new(result, null);
    public static implicit operator AocPartResultValue(BigInteger result)
        => new(result, null);
    public override string ToString()
    {
        string label = Label is not null ? $"{Label}: " : "";
        return $"{label}{Value.PrintNull()}";
    }
}