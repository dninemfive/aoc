namespace d9.aoc.core;
public record AocPartResult(string Label, AocPartResultValue Result, TimeSpan Elapsed)
{
    public object? Value => Result.Value;
    public override string ToString()
        => $"{Label}:\t{Result.Value,16}\t{Elapsed,16:c}";
}