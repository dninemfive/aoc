using System.Numerics;

namespace d9.aoc.core;
public record AocPartResultValue(object Value)
{
    public static implicit operator AocPartResultValue(int result)
        => new(result);
    public static implicit operator AocPartResultValue(long result)
        => new(result);
    public static implicit operator AocPartResultValue(BigInteger result)
        => new(result);
}